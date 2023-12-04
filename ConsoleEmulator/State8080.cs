namespace ConsoleEmulator;

internal struct State8080
{
    private readonly byte[] _registers = new byte[8];

    public byte A
    {
        get => _registers.AsSpan()[7];
        set => _registers.AsSpan()[7] = value;
    }
    public byte B
    {
        get => _registers.AsSpan()[0];
        set => _registers.AsSpan()[0] = value;
    }
    public byte C
    {
        get => _registers.AsSpan()[1];
        set => _registers.AsSpan()[1] = value;
    }
    public byte D
    {
        get => _registers.AsSpan()[2];
        set => _registers.AsSpan()[2] = value;
    }
    public byte E
    {
        get => _registers.AsSpan()[3];
        set => _registers.AsSpan()[3] = value;
    }
    public byte H
    {
        get => _registers.AsSpan()[4];
        set => _registers.AsSpan()[4] = value;
    }
    public byte L
    {
        get => _registers.AsSpan()[5];
        set => _registers.AsSpan()[5] = value;
    }
    public ushort SP { get; set; }
    public ushort PC { get; set; }
    public byte[] Memory { get; set; } = [];
    public ConditionCodes CC { get; set; }
    public byte IntEnable { get; set; }

    public State8080()
    {

    }

    public byte GetRegister(int offset)
    {
        return _registers.AsSpan()[offset];
    }

    public void SetRegister(int offset, byte value)
    {
        _registers.AsSpan()[offset] = value;
    }
}
