using Emulator;

namespace SpaceInvaders;

public class ArcadeInput : IInputDevice
{
    private bool _coinInserted = false;
    private bool _start = false;
    private bool _left = false;
    private bool _right = false;
    private bool _shot = false;

    public byte ReadByte()
    {
        byte result = 0x08;
        if (_coinInserted)
        {
            result |= 0x01;
            _coinInserted = false;
        }
        if (_start)
        {
            result |= 0x04;
        }
        if (_shot)
        {
            result |= 0x10;
        }
        if (_left)
        {
            result |= 0x20;
        }
        if (_right)
        {
            result |= 0x40;
        }
        return result;
    }

    public void KeyDown(Keys keyData)
    {
        if (keyData == Keys.Enter)
        {
            _start = true;
        }
        if (keyData == Keys.Left)
        {
            _left = true;
        }
        if (keyData == Keys.Right)
        {
            _right = true;
        }
        if (keyData == Keys.Up)
        {
            _shot = true;
        }
    }

    public void KeyUp(Keys keyData)
    {
        if (keyData == Keys.Enter)
        {
            _start = false;
        }
        if (keyData == Keys.Left)
        {
            _left = false;
        }
        if (keyData == Keys.Right)
        {
            _right = false;
        }
        if (keyData == Keys.Up)
        {
            _shot = false;
        }
    }

    public void OnCoinInserted ()
    {
        _coinInserted = true;
    }
}
