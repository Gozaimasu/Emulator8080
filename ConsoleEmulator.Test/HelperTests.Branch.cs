namespace ConsoleEmulator.Test;

public partial class HelperTests
{
    [Fact]
    public void Disassemble8080Op_WhenJMP_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xC3, 0x01, 0x02], 0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal($"0000\tJMP\t$0201{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Emulate8080Op_WhenJMP_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0xC3, 0x01, 0x02],
            PC = 0
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x0201, state.PC);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetJConditionData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenJCondition_ShouldSucceed(byte[] data, string expectedOutput)
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op(data, 0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal(expectedOutput, debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetJConditionData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenJCondition_ShouldSucceed(byte[] data, byte initialZ, byte initialCY, byte initialP, byte initialS, ushort expectedPC)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = data,
            PC = 0,
            CC = new ConditionCodes
            {
                Z = initialZ,
                CY = initialCY,
                P = initialP,
                S = initialS
            }
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedPC, state.PC);
    }

    [Fact]
    public void Disassemble8080Op_WhenCALL_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xCD, 0x01, 0x02], 0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal($"0000\tCALL\t$0201{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Emulate8080Op_WhenCALL_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0xCD, 0x01, 0x02, 0x02, 0x01],
            PC = 0x00,
            SP = 5
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(3, state.SP);
        Assert.Equal(0x0201, state.PC);
        Assert.Equal(0x01, state.Memory[3]);
        Assert.Equal(0x00, state.Memory[4]);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetCConditionData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenCCondition_ShouldSucceed(byte[] data, string expectedOutput)
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op(data, 0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal(expectedOutput, debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenRET_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xC9], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tRET{Environment.NewLine}", debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetRConditionData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenRCondition_ShouldSucceed(byte[] data, string expectedOutput)
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op(data, 0);

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
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op(data, 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal(expectedOutput, debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenPCHL_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xE9], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tPCHL{Environment.NewLine}", debugOutput.Output);
    }
}
