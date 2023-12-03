using System.Runtime.InteropServices;

namespace ConsoleEmulator;

[StructLayout(LayoutKind.Explicit, Size = 8)]
internal struct ConditionCodes
{
    [FieldOffset(0)]
    public byte Z;
    [FieldOffset(1)]
    public byte S;
    [FieldOffset(2)]
    public byte P;
    [FieldOffset(3)]
    public byte CY;
    [FieldOffset(4)]
    public byte AC;
    [FieldOffset(5)]
    public byte PAD;
}
