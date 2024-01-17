using Emulator;
using System.Media;

namespace SpaceInvaders;

public class SoundDevice : IOutputDevice
{
    private readonly byte _port;
    private readonly SoundPlayer?[] _sounds = new SoundPlayer?[8];
    private byte _lastData;

    public SoundDevice(byte port)
    {
        _port = port;
        if (port == 0x03)
        {
            _sounds[0] = new(Properties.Resources.ufo_highpitch);
            _sounds[0]!.Load();
            _sounds[1] = new(Properties.Resources.shoot);
            _sounds[1]!.Load();
            _sounds[2] = new(Properties.Resources.explosion);
            _sounds[2]!.Load();
            _sounds[3] = new(Properties.Resources.invaderkilled);
            _sounds[3]!.Load();
            return;
        }
        if (port == 0x05)
        {
            _sounds[0] = new(Properties.Resources.fastinvader1);
            _sounds[0]!.Load();
            _sounds[1] = new(Properties.Resources.fastinvader2);
            _sounds[1]!.Load();
            _sounds[2] = new(Properties.Resources.fastinvader3);
            _sounds[2]!.Load();
            _sounds[3] = new(Properties.Resources.fastinvader4);
            _sounds[3]!.Load();
            _sounds[4] = new(Properties.Resources.ufo_lowpitch);
            _sounds[4]!.Load();
            return;
        }
    }

    public void WriteByte(byte data)
    {
        if (data == _lastData)
        {
            return;
        }

        byte test = 0x01;
        for (int bitnumber = 0; bitnumber < 8; bitnumber++)
        {
            SoundPlayer? soundPlayer = _sounds[bitnumber];
            // Start sound if transition from 0 to 1
            if ((byte)(data & test) == test && (byte)(_lastData & test) != test)
            {
                if ((_port == 0x03) && (bitnumber == 0))
                {
                    // Repeat sound for sound 1 of port 3
                    soundPlayer?.PlayLooping();
                }
                else
                {
                    soundPlayer?.Play();
                }
            }
            // Stop sound if transition from 1 to 0
            else if ((byte)(data & test) != test && (byte)(_lastData & test) == test)
            {
                if ((_port == 0x03) && (bitnumber == 0))
                {
                    soundPlayer?.Stop();
                }
            }
            test = (byte)(test << 1);
        }

        _lastData = data;
    }
}
