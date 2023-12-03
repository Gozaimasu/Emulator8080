namespace ConsoleEmulator;

internal struct State8080
{
    public byte A { get; set; }
    public byte B { get; set; }
    public byte C { get; set; }
    public byte D { get; set; }
    public byte E { get; set; }
    public byte H { get; set; }
    public byte L { get; set; }
    public ushort SP { get; set; }
    public ushort PC { get; set; }
    public byte[] Memory { get; set; }
    public ConditionCodes CC { get; set; }
    public byte IntEnable { get; set; }
}
