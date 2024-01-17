using Emulator;

namespace SpaceInvaders;

public class ShiftOffsetDevice : IOutputDevice
{
    private readonly object _lock = new();
    private byte _shiftOffset;

    public ShiftOffsetDevice()
    {
        _shiftOffset = 0;
    }

    public byte GetShiftOffset()
    {
        lock (_lock)
        {
            return _shiftOffset;
        }
    }

    public void WriteByte(byte data)
    {
        lock (_lock)
        {
            _shiftOffset = (byte)(data & 0x07);
        }
    }
}
