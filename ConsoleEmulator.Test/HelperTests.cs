using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleEmulator.Test;

public class HelperTests
{
    [Theory]
    [InlineData(2, 1, 0, 0, 0)]
    [InlineData(1, 0, 1, 0, 1)]
    [InlineData(0, 0xFF, 0, 1, 0)]
    [InlineData(128, 127, 0, 0, 0)]
    public void Emulate8080Op_WhenDCRB_ShouldSucceed(byte initialB, byte expectedB, byte expectedZ, byte expectedS, byte expectedP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x05],
            PC = 0,
            B = initialB,
            CC = new ConditionCodes()
        };

        // Act
        Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(expectedB, state.B);
        Assert.Equal(1, state.PC);
        Assert.Equal(expectedZ, state.CC.Z);
        Assert.Equal(expectedS, state.CC.S);
        Assert.Equal(expectedP, state.CC.P);
    }

    [Fact]
    public void Emulate8080Op_WhenNOP_ShouldDoNothing()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x00],
            PC = 0,
            A = 1,
            B = 1,
            C = 1,
            D = 1,
            E = 1,
            H = 1,
            L = 1,
            SP = 1,
            IntEnable = 1,
            CC = new ConditionCodes()
            {
                AC = 1,
                CY = 1,
                P = 1,
                PAD = 1,
                S = 1,
                Z = 1
            }
        };

        // Act
        Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(1, state.A);
        Assert.Equal(1, state.B);
        Assert.Equal(1, state.C);
        Assert.Equal(1, state.D);
        Assert.Equal(1, state.E);
        Assert.Equal(1, state.H);
        Assert.Equal(1, state.L);
        Assert.Equal(1, state.SP);
        Assert.Equal(1, state.IntEnable);
        Assert.Equal(1, state.CC.AC);
        Assert.Equal(1, state.CC.CY);
        Assert.Equal(1, state.CC.P);
        Assert.Equal(1, state.CC.PAD);
        Assert.Equal(1, state.CC.S);
        Assert.Equal(1, state.CC.Z);
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
        Assert.Equal("0000\tNOP\r\n", debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetMOVData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenMOV_ShouldSucceed(byte[] data, string expectedOutput)
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
    [MemberData(nameof(DisassembleTestData.GetMVIData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenMVI_ShouldSucceed(byte[] data, string expectedOutput)
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op(data, 0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal(expectedOutput, debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetLXIData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenLXI_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Disassemble8080Op_WhenLDA_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0x3A, 0x01, 0x02], 0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal("0000\tLDA\t$0201\r\n", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenSTA_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0x32, 0x01, 0x02], 0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal("0000\tSTA\t$0201\r\n", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenLHLD_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0x2A, 0x01, 0x02], 0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal("0000\tLHLD\t$0201\r\n", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenSHLD_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0x22, 0x01, 0x02], 0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal("0000\tSHLD\t$0201\r\n", debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetLDAXData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenLDAX_ShouldSucceed(byte[] data, string expectedOutput)
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
    [MemberData(nameof(DisassembleTestData.GetSTAXData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenSTAX_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Disassemble8080Op_WhenXCHG_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xEB], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal("0000\tXCHG\r\n", debugOutput.Output);
    }
}