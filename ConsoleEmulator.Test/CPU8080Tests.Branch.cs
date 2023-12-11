namespace ConsoleEmulator.Test;

public partial class CPU8080Tests
{
    [Fact]
    public void Disassemble8080Op_WhenJMP_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xC3, 0x01, 0x02], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal($"0000\tJMP\t$0201{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Emulate8080Op_WhenJMP_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0xC3, 0x01, 0x02], 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x0201, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetJConditionData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenJCondition_ShouldSucceed(byte[] data, string expectedOutput)
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init(data, 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal(expectedOutput, debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetJConditionData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenJCondition_ShouldSucceed(byte[] data, byte initialZ, byte initialCY, byte initialP, byte initialS, ushort expectedPC)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init(data, 0);
        sut.State.CC = new ConditionCodes
        {
            Z = initialZ,
            CY = initialCY,
            P = initialP,
            S = initialS
        };

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedPC, sut.State.PC);
    }

    [Fact]
    public void Disassemble8080Op_WhenCALL_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xCD, 0x01, 0x02], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal($"0000\tCALL\t$0201{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Emulate8080Op_WhenCALL_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0xCD, 0x01, 0x02, 0x02, 0x01], 0);
        sut.State.SP = 5;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(3, sut.State.SP);
        Assert.Equal(0x0201, sut.State.PC);
        Assert.Equal(0x01, sut.Memory[3]);
        Assert.Equal(0x00, sut.Memory[4]);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetCConditionData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenCCondition_ShouldSucceed(byte[] data, string expectedOutput)
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init(data, 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal(expectedOutput, debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenRET_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xC9], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tRET{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Emulate8080Op_WhenRET_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0xC9, 0x02, 0x01], 0);
        sut.State.SP = 1;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x0102, sut.State.PC);
        Assert.Equal(3, sut.State.SP);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetRConditionData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenRCondition_ShouldSucceed(byte[] data, string expectedOutput)
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init(data, 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal(expectedOutput, debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetRSTData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenRST_ShouldSucceed(byte[] data, string expectedOutput)
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init(data, 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal(expectedOutput, debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenPCHL_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xE9], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tPCHL{Environment.NewLine}", debugOutput.Output);
    }
}
