using Emulator;

namespace SpaceInvaders;

public class ShiftDevice : IOutputDevice, IInputDevice
{
    private readonly object _lock = new();
    private readonly ShiftOffsetDevice _shiftOffsetDevice;
    private ushort _shiftReg;

    public ShiftDevice(ShiftOffsetDevice shiftOffsetDevice)
    {
        _shiftReg = 0;
        _shiftOffsetDevice = shiftOffsetDevice;
    }

    public byte ReadByte()
    {
        lock (_lock)
        {
            byte shiftOffset = _shiftOffsetDevice.GetShiftOffset();
            return (byte)((_shiftReg >> (8 - shiftOffset)) & 0xff);
        }
    }

    public void WriteByte(byte data)
    {
        lock (_lock)
        {
            byte lsb = (byte)((_shiftReg >> 8) & 0xff);
            _shiftReg = (ushort)(((uint)data << 8 | lsb) & 0xffff);
        }
    }
}
