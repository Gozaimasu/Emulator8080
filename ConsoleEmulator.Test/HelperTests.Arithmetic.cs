﻿namespace ConsoleEmulator.Test;

public partial class HelperTests
{
    [Theory]
    [MemberData(nameof(DisassembleTestData.GetADDData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenADD_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Disassemble8080Op_WhenADI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xC6, 0x01], 0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal($"0000\tADI\t#$01{Environment.NewLine}", debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetADCData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenADC_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Disassemble8080Op_WhenACI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xCE, 0x01], 0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal($"0000\tACI\t#$01{Environment.NewLine}", debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetSUBData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenSUB_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Disassemble8080Op_WhenSUI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xD6, 0x01], 0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal($"0000\tSUI\t#$01{Environment.NewLine}", debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetSBBData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenSBB_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Disassemble8080Op_WhenSBI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xDE, 0x01], 0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal($"0000\tSBI\t#$01{Environment.NewLine}", debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetINRData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenINR_ShouldSucceed(byte[] data, string expectedOutput)
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
    [MemberData(nameof(DisassembleTestData.GetDCRData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenDCR_ShouldSucceed(byte[] data, string expectedOutput)
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
    [MemberData(nameof(EmulateTestData.GetDCRData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDCRB_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x05],
            PC = 0,
            B = initialValue,
            CC = new ConditionCodes()
        };

        // Act
        Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(expectedValue, state.B);
        Assert.Equal(1, state.PC);
        Assert.Equal(expectedZ, state.CC.Z);
        Assert.Equal(expectedS, state.CC.S);
        Assert.Equal(expectedP, state.CC.P);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetDCRData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDCRC_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x0D],
            PC = 0,
            C = initialValue,
            CC = new ConditionCodes()
        };

        // Act
        Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(expectedValue, state.C);
        Assert.Equal(1, state.PC);
        Assert.Equal(expectedZ, state.CC.Z);
        Assert.Equal(expectedS, state.CC.S);
        Assert.Equal(expectedP, state.CC.P);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetDCRData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDCRD_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x15],
            PC = 0,
            D = initialValue,
            CC = new ConditionCodes()
        };

        // Act
        Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(expectedValue, state.D);
        Assert.Equal(1, state.PC);
        Assert.Equal(expectedZ, state.CC.Z);
        Assert.Equal(expectedS, state.CC.S);
        Assert.Equal(expectedP, state.CC.P);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetDCRData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDCRE_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x1D],
            PC = 0,
            E = initialValue,
            CC = new ConditionCodes()
        };

        // Act
        Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(expectedValue, state.E);
        Assert.Equal(1, state.PC);
        Assert.Equal(expectedZ, state.CC.Z);
        Assert.Equal(expectedS, state.CC.S);
        Assert.Equal(expectedP, state.CC.P);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetDCRData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDCRH_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x25],
            PC = 0,
            H = initialValue,
            CC = new ConditionCodes()
        };

        // Act
        Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(expectedValue, state.H);
        Assert.Equal(1, state.PC);
        Assert.Equal(expectedZ, state.CC.Z);
        Assert.Equal(expectedS, state.CC.S);
        Assert.Equal(expectedP, state.CC.P);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetDCRData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDCRL_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x2D],
            PC = 0,
            L = initialValue,
            CC = new ConditionCodes()
        };

        // Act
        Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(expectedValue, state.L);
        Assert.Equal(1, state.PC);
        Assert.Equal(expectedZ, state.CC.Z);
        Assert.Equal(expectedS, state.CC.S);
        Assert.Equal(expectedP, state.CC.P);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetDCRData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDCRA_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x3D],
            PC = 0,
            A = initialValue,
            CC = new ConditionCodes()
        };

        // Act
        Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(expectedValue, state.A);
        Assert.Equal(1, state.PC);
        Assert.Equal(expectedZ, state.CC.Z);
        Assert.Equal(expectedS, state.CC.S);
        Assert.Equal(expectedP, state.CC.P);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetINXData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenINX_ShouldSucceed(byte[] data, string expectedOutput)
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
    [MemberData(nameof(DisassembleTestData.GetDCXData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenDCX_ShouldSucceed(byte[] data, string expectedOutput)
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
    [MemberData(nameof(DisassembleTestData.GetDADData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenDAD_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Disassemble8080Op_WhenDAA_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0x27], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tDAA{Environment.NewLine}", debugOutput.Output);
    }
}
