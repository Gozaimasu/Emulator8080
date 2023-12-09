namespace ConsoleEmulator.Test;

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
    public void Emulate8080Op_WhenDCRB_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP, byte initialZ, byte initialS, byte initialP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x05],
            PC = 0,
            B = initialValue,
            CC = new ConditionCodes()
            {
                Z = initialZ,
                S = initialS,
                P = initialP
            }
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
    public void Emulate8080Op_WhenDCRC_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP, byte initialZ, byte initialS, byte initialP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x0D],
            PC = 0,
            C = initialValue,
            CC = new ConditionCodes()
            {
                Z = initialZ,
                S = initialS,
                P = initialP
            }
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
    public void Emulate8080Op_WhenDCRD_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP, byte initialZ, byte initialS, byte initialP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x15],
            PC = 0,
            D = initialValue,
            CC = new ConditionCodes()
            {
                Z = initialZ,
                S = initialS,
                P = initialP
            }
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
    public void Emulate8080Op_WhenDCRE_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP, byte initialZ, byte initialS, byte initialP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x1D],
            PC = 0,
            E = initialValue,
            CC = new ConditionCodes()
            {
                Z = initialZ,
                S = initialS,
                P = initialP
            }
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
    public void Emulate8080Op_WhenDCRH_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP, byte initialZ, byte initialS, byte initialP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x25],
            PC = 0,
            H = initialValue,
            CC = new ConditionCodes()
            {
                Z = initialZ,
                S = initialS,
                P = initialP
            }
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
    public void Emulate8080Op_WhenDCRL_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP, byte initialZ, byte initialS, byte initialP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x2D],
            PC = 0,
            L = initialValue,
            CC = new ConditionCodes()
            {
                Z = initialZ,
                S = initialS,
                P = initialP
            }
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
    public void Emulate8080Op_WhenDCRA_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP, byte initialZ, byte initialS, byte initialP)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x3D],
            PC = 0,
            A = initialValue,
            CC = new ConditionCodes()
            {
                Z = initialZ,
                S = initialS,
                P = initialP
            }
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
    [MemberData(nameof(EmulateTestData.GetINXData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenINXB_ShouldSucceed(ushort initialValue, ushort expectedValue)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x03],
            PC = 0,
            B = (byte)((initialValue >> 8) & 0xFF),
            C = (byte)(initialValue & 0xFF),
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal((byte)(expectedValue & 0xFF), state.C);
        Assert.Equal((byte)((expectedValue >> 8) & 0xFF), state.B);
        Assert.Equal(1, state.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetINXData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenINXD_ShouldSucceed(ushort initialValue, ushort expectedValue)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x13],
            PC = 0,
            D = (byte)((initialValue >> 8) & 0xFF),
            E = (byte)(initialValue & 0xFF),
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal((byte)(expectedValue & 0xFF), state.E);
        Assert.Equal((byte)((expectedValue >> 8) & 0xFF), state.D);
        Assert.Equal(1, state.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetINXData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenINXH_ShouldSucceed(ushort initialValue, ushort expectedValue)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x23],
            PC = 0,
            H = (byte)((initialValue >> 8) & 0xFF),
            L = (byte)(initialValue & 0xFF),
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal((byte)(expectedValue & 0xFF), state.L);
        Assert.Equal((byte)((expectedValue >> 8) & 0xFF), state.H);
        Assert.Equal(1, state.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetINXData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenINXSP_ShouldSucceed(ushort initialValue, ushort expectedValue)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x33],
            PC = 0,
            SP = initialValue
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, state.SP);
        Assert.Equal(1, state.PC);
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

    [Theory]
    [MemberData(nameof(EmulateTestData.GetDADData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDADB_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte initialSP, byte expectedH, byte expectedL, byte initialCY, byte expectedCY)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = data,
            PC = 0,
            B = initialB,
            C = initialC,
            D = initialD,
            E = initialE,
            H = initialH,
            L = initialL,
            A = initialA,
            SP = initialSP,
            CC = new ConditionCodes()
            {
                CY = initialCY
            }
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(1, state.PC);
        Assert.Equal(expectedH, state.H);
        Assert.Equal(expectedL, state.L);
        Assert.Equal(expectedCY, state.CC.CY);
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
