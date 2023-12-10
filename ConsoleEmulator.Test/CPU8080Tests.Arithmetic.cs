namespace ConsoleEmulator.Test;

public partial class CPU8080Tests
{
    [Theory]
    [MemberData(nameof(DisassembleTestData.GetADDData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenADD_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Disassemble8080Op_WhenADI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xC6, 0x01], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

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
    public void Disassemble8080Op_WhenACI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xCE, 0x01], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

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
    public void Disassemble8080Op_WhenSUI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xD6, 0x01], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

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
    public void Disassemble8080Op_WhenSBI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xDE, 0x01], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

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
    [MemberData(nameof(DisassembleTestData.GetDCRData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenDCR_ShouldSucceed(byte[] data, string expectedOutput)
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
    [MemberData(nameof(EmulateTestData.GetDCRData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDCRB_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP, byte initialZ, byte initialS, byte initialP)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x05], 0);
        sut.State.B = initialValue;
        sut.State.CC = new ConditionCodes()
        {
            Z = initialZ,
            S = initialS,
            P = initialP
        };

        // Act
        sut.Step();

        // Assert
        Assert.Equal(expectedValue, sut.State.B);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(expectedZ, sut.State.CC.Z);
        Assert.Equal(expectedS, sut.State.CC.S);
        Assert.Equal(expectedP, sut.State.CC.P);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetDCRData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDCRC_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP, byte initialZ, byte initialS, byte initialP)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x0D], 0);
        sut.State.C = initialValue;
        sut.State.CC = new ConditionCodes()
        {
            Z = initialZ,
            S = initialS,
            P = initialP
        };

        // Act
        sut.Step();

        // Assert
        Assert.Equal(expectedValue, sut.State.C);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(expectedZ, sut.State.CC.Z);
        Assert.Equal(expectedS, sut.State.CC.S);
        Assert.Equal(expectedP, sut.State.CC.P);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetDCRData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDCRD_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP, byte initialZ, byte initialS, byte initialP)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x15], 0);
        sut.State.D = initialValue;
        sut.State.CC = new ConditionCodes()
        {
            Z = initialZ,
            S = initialS,
            P = initialP
        };

        // Act
        sut.Step();

        // Assert
        Assert.Equal(expectedValue, sut.State.D);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(expectedZ, sut.State.CC.Z);
        Assert.Equal(expectedS, sut.State.CC.S);
        Assert.Equal(expectedP, sut.State.CC.P);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetDCRData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDCRE_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP, byte initialZ, byte initialS, byte initialP)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x1D], 0);
        sut.State.E = initialValue;
        sut.State.CC = new ConditionCodes()
        {
            Z = initialZ,
            S = initialS,
            P = initialP
        };

        // Act
        sut.Step();

        // Assert
        Assert.Equal(expectedValue, sut.State.E);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(expectedZ, sut.State.CC.Z);
        Assert.Equal(expectedS, sut.State.CC.S);
        Assert.Equal(expectedP, sut.State.CC.P);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetDCRData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDCRH_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP, byte initialZ, byte initialS, byte initialP)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x25], 0);
        sut.State.H = initialValue;
        sut.State.CC = new ConditionCodes()
        {
            Z = initialZ,
            S = initialS,
            P = initialP
        };

        // Act
        sut.Step();

        // Assert
        Assert.Equal(expectedValue, sut.State.H);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(expectedZ, sut.State.CC.Z);
        Assert.Equal(expectedS, sut.State.CC.S);
        Assert.Equal(expectedP, sut.State.CC.P);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetDCRData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDCRL_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP, byte initialZ, byte initialS, byte initialP)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x2D], 0);
        sut.State.L = initialValue;
        sut.State.CC = new ConditionCodes()
        {
            Z = initialZ,
            S = initialS,
            P = initialP
        };

        // Act
        sut.Step();

        // Assert
        Assert.Equal(expectedValue, sut.State.L);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(expectedZ, sut.State.CC.Z);
        Assert.Equal(expectedS, sut.State.CC.S);
        Assert.Equal(expectedP, sut.State.CC.P);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetDCRData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDCRA_ShouldSucceed(byte initialValue, byte expectedValue, byte expectedZ, byte expectedS, byte expectedP, byte initialZ, byte initialS, byte initialP)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x3D], 0);
        sut.State.A = initialValue;
        sut.State.CC = new ConditionCodes()
        {
            Z = initialZ,
            S = initialS,
            P = initialP
        };

        // Act
        sut.Step();

        // Assert
        Assert.Equal(expectedValue, sut.State.A);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(expectedZ, sut.State.CC.Z);
        Assert.Equal(expectedS, sut.State.CC.S);
        Assert.Equal(expectedP, sut.State.CC.P);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetINXData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenINX_ShouldSucceed(byte[] data, string expectedOutput)
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
    [MemberData(nameof(EmulateTestData.GetINXData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenINXB_ShouldSucceed(ushort initialValue, ushort expectedValue)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x03], 0);
        sut.State.B = (byte)((initialValue >> 8) & 0xFF);
        sut.State.C = (byte)(initialValue & 0xFF);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal((byte)(expectedValue & 0xFF), sut.State.C);
        Assert.Equal((byte)((expectedValue >> 8) & 0xFF), sut.State.B);
        Assert.Equal(1, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetINXData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenINXD_ShouldSucceed(ushort initialValue, ushort expectedValue)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x13], 0);
        sut.State.D = (byte)((initialValue >> 8) & 0xFF);
        sut.State.E = (byte)(initialValue & 0xFF);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal((byte)(expectedValue & 0xFF), sut.State.E);
        Assert.Equal((byte)((expectedValue >> 8) & 0xFF), sut.State.D);
        Assert.Equal(1, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetINXData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenINXH_ShouldSucceed(ushort initialValue, ushort expectedValue)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x23], 0);
        sut.State.H = (byte)((initialValue >> 8) & 0xFF);
        sut.State.L = (byte)(initialValue & 0xFF);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal((byte)(expectedValue & 0xFF), sut.State.L);
        Assert.Equal((byte)((expectedValue >> 8) & 0xFF), sut.State.H);
        Assert.Equal(1, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetINXData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenINXSP_ShouldSucceed(ushort initialValue, ushort expectedValue)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x03], 0);
        sut.State.SP = initialValue;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, sut.State.SP);
        Assert.Equal(1, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetDCXData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenDCX_ShouldSucceed(byte[] data, string expectedOutput)
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
    [MemberData(nameof(DisassembleTestData.GetDADData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenDAD_ShouldSucceed(byte[] data, string expectedOutput)
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
    [MemberData(nameof(EmulateTestData.GetDADData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenDAD_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, ushort initialSP, byte expectedH, byte expectedL, byte initialCY, byte expectedCY)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init(data, 0);
        sut.State.B = initialB;
        sut.State.C = initialC;
        sut.State.D = initialD;
        sut.State.E = initialE;
        sut.State.H = initialH;
        sut.State.L = initialL;
        sut.State.SP = initialSP;
        sut.State.CC = new ConditionCodes()
        {
            CY = initialCY
        };

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(expectedH, sut.State.H);
        Assert.Equal(expectedL, sut.State.L);
        Assert.Equal(expectedCY, sut.State.CC.CY);
    }

    [Fact]
    public void Disassemble8080Op_WhenDAA_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x27], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tDAA{Environment.NewLine}", debugOutput.Output);
    }
}
