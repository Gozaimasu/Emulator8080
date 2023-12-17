using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleEmulator.Test;

public partial class CPU8080Tests
{
    [Theory]
    [MemberData(nameof(DisassembleTestData.GetPUSHData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenPUSH_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Emulate8080Op_WhenPUSHB_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0xC5, 0x00, 0x00], 0);
        sut.State.B = 0x02;
        sut.State.C = 0x01;
        sut.State.SP = 3;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(0x02, sut.Memory[2]);
        Assert.Equal(0x01, sut.Memory[1]);
        Assert.Equal(1, sut.State.SP);
    }

    [Fact]
    public void Emulate8080Op_WhenPUSHD_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0xD5, 0x00, 0x00], 0);
        sut.State.D = 0x02;
        sut.State.E = 0x01;
        sut.State.SP = 3;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(0x02, sut.Memory[2]);
        Assert.Equal(0x01, sut.Memory[1]);
        Assert.Equal(1, sut.State.SP);
    }

    [Fact]
    public void Emulate8080Op_WhenPUSHH_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0xE5, 0x00, 0x00], 0);
        sut.State.H = 0x02;
        sut.State.L = 0x01;
        sut.State.SP = 3;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(0x02, sut.Memory[2]);
        Assert.Equal(0x01, sut.Memory[1]);
        Assert.Equal(1, sut.State.SP);
    }

    [Fact]
    public void Emulate8080Op_WhenPUSHPSW_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0xF5, 0x00, 0x00], 0);
        sut.State.A = 0xFF;
        sut.State.SP = 3;
        sut.State.CC = new ConditionCodes
        {
            Z = 0x01,
            S = 0x01,
            P = 0x01,
            CY = 0x01,
            AC = 0x01,
            PAD = 0x01
        };

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(0xD7, sut.Memory[1]);
        Assert.Equal(0xFF, sut.Memory[2]);
        Assert.Equal(1, sut.State.SP);
        Assert.Equal(3, sut.Cycles);
        Assert.Equal(11, sut.States);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetPOPData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenPOP_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Emulate8080Op_WhenPOPB_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0xC1, 0x0A, 0x0B], 0);
        sut.State.B = 0x00;
        sut.State.C = 0x00;
        sut.State.SP = 1;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(0x0A, sut.State.C);
        Assert.Equal(0x0B, sut.State.B);
        Assert.Equal(3, sut.State.SP);
    }

    [Fact]
    public void Emulate8080Op_WhenPOPD_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0xD1, 0x0A, 0x0B], 0);
        sut.State.D = 0x00;
        sut.State.E = 0x00;
        sut.State.SP = 1;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(0x0A, sut.State.E);
        Assert.Equal(0x0B, sut.State.D);
        Assert.Equal(3, sut.State.SP);
    }

    [Fact]
    public void Emulate8080Op_WhenPOPH_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0xE1, 0x0A, 0x0B], 0);
        sut.State.H = 0x00;
        sut.State.L = 0x00;
        sut.State.SP = 1;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(0x0A, sut.State.L);
        Assert.Equal(0x0B, sut.State.H);
        Assert.Equal(3, sut.State.SP);
    }

    [Fact]
    public void Disassemble8080Op_WhenXTHL_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xE3], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tXTHL{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenSPHL_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xF9], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tSPHL{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenIN_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xDB, 0x01], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal($"0000\tIN\t#$01{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenOUT_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xD3, 0x01], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal($"0000\tOUT\t#$01{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Emulate8080Op_WhenOUT_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0xD3, 0x01], 0);
        sut.State.A = 0x02;
        sut.Output = delegate (byte port, byte data) { Assert.Equal(0x01, port); Assert.Equal(0x02, data); };

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(2, sut.State.PC);
    }

    [Fact]
    public void Disassemble8080Op_WhenEI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xFB], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tEI{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenDI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xF3], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tDI{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenHLT_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x76], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tHLT{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenNOP_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x00], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tNOP{Environment.NewLine}", debugOutput.Output);
    }

}
