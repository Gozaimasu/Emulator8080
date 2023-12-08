namespace ConsoleEmulator.Test;

public partial class HelperTests
{
    [Theory]
    [MemberData(nameof(DisassembleTestData.GetPUSHData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenPUSH_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Emulate8080Op_WhenPUSHB_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0xC5, 0x00, 0x00],
            B = 0x02,
            C = 0x01,
            PC = 0,
            SP = 3
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(1, state.PC);
        Assert.Equal(0x02, state.Memory[2]);
        Assert.Equal(0x01, state.Memory[1]);
        Assert.Equal(1, state.SP);
    }

    [Fact]
    public void Emulate8080Op_WhenPUSHD_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0xD5, 0x00, 0x00],
            D = 0x02,
            E = 0x01,
            PC = 0,
            SP = 3
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(1, state.PC);
        Assert.Equal(0x02, state.Memory[2]);
        Assert.Equal(0x01, state.Memory[1]);
        Assert.Equal(1, state.SP);
    }

    [Fact]
    public void Emulate8080Op_WhenPUSHH_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0xE5, 0x00, 0x00],
            H = 0x02,
            L = 0x01,
            PC = 0,
            SP = 3
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(1, state.PC);
        Assert.Equal(0x02, state.Memory[2]);
        Assert.Equal(0x01, state.Memory[1]);
        Assert.Equal(1, state.SP);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetPOPData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenPOP_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Disassemble8080Op_WhenXTHL_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xE3], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tXTHL{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenSPHL_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xF9], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tSPHL{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenIN_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xDB, 0x01], 0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal($"0000\tIN\t#$01{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenOUT_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xD3, 0x01], 0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal($"0000\tOUT\t#$01{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenEI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xFB], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tEI{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenDI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xF3], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tDI{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenHLT_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0x76], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tHLT{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenNOP_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0x00], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tNOP{Environment.NewLine}", debugOutput.Output);
    }

}
