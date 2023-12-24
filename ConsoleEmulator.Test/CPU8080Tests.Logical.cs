namespace ConsoleEmulator.Test;

public partial class CPU8080Tests
{
    [Theory]
    [MemberData(nameof(DisassembleTestData.GetANAData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenANA_ShouldSucceed(byte[] data, string expectedOutput)
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
    [MemberData(nameof(EmulateTestData.GetANARData), MemberType = typeof(EmulateTestData))]
    public void Step_WhenANAR_ShouldSucceed(byte[] data, byte initialA, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte expectedA)
    {
        // Arrange
        CPU8080 sut = new();
        sut.State.A = initialA;
        sut.State.B = initialB;
        sut.State.C = initialC;
        sut.State.D = initialD;
        sut.State.E = initialE;
        sut.State.H = initialH;
        sut.State.L = initialL;
        sut.State.CC = new()
        {
            CY = 0x01
        };
        sut.Init(data, 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedA, sut.State.A);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(0, sut.State.CC.CY);
        Assert.Equal(1, sut.Cycles);
        Assert.Equal(4, sut.States);
    }

    [Fact]
    public void Step_WhenANAM_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.State.A = 0xAF;
        sut.State.H = 0x01;
        sut.State.L = 0x02;
        sut.State.CC = new()
        {
            CY = 0x01
        };
        byte[] data = new byte[259];
        data[0] = 0xA6;
        data[258] = 0x55;
        sut.Init(data, 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x05, sut.State.A);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(0, sut.State.CC.CY);
        Assert.Equal(2, sut.Cycles);
        Assert.Equal(7, sut.States);
    }

    [Fact]
    public void Disassemble8080Op_WhenANI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xE6, 0x01], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal($"0000\tANI\t#$01{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Emulate8080Op_WhenANI_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0xE6, 0xA5], 0);
        sut.State.A = 0xF0;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0xA0, sut.State.A);
        Assert.Equal(2, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetXRAData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenXRA_ShouldSucceed(byte[] data, string expectedOutput)
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
    [MemberData(nameof(EmulateTestData.GetXRARData), MemberType = typeof(EmulateTestData))]
    public void Step_WhenXRAR_ShouldSucceed(byte[] data, byte initialA, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte expectedA)
    {
        // Arrange
        CPU8080 sut = new();
        sut.State.A = initialA;
        sut.State.B = initialB;
        sut.State.C = initialC;
        sut.State.D = initialD;
        sut.State.E = initialE;
        sut.State.H = initialH;
        sut.State.L = initialL;
        sut.State.CC = new()
        {
            CY = 0x01,
            AC = 0x01
        };
        sut.Init(data, 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedA, sut.State.A);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(0, sut.State.CC.CY);
        Assert.Equal(0, sut.State.CC.AC);
        Assert.Equal(1, sut.Cycles);
        Assert.Equal(4, sut.States);
    }

    [Fact]
    public void Step_WhenXRAM_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.State.A = 0xAF;
        sut.State.H = 0x01;
        sut.State.L = 0x02;
        sut.State.CC = new()
        {
            CY = 0x01,
            AC = 0x01
        };
        byte[] data = new byte[259];
        data[0] = 0xAE;
        data[258] = 0x55;
        sut.Init(data, 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0xFA, sut.State.A);
        Assert.Equal(1, sut.State.PC);
        Assert.Equal(0, sut.State.CC.CY);
        Assert.Equal(0, sut.State.CC.AC);
        Assert.Equal(2, sut.Cycles);
        Assert.Equal(7, sut.States);
    }

    [Fact]
    public void Disassemble8080Op_WhenXRI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xEE, 0x01], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal($"0000\tXRI\t#$01{Environment.NewLine}", debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetORAData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenORA_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Disassemble8080Op_WhenORI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xF6, 0x01], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal($"0000\tORI\t#$01{Environment.NewLine}", debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetCMPData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenCMP_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Disassemble8080Op_WhenCPI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xFE, 0x01], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal($"0000\tCPI\t#$01{Environment.NewLine}", debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetCPIData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenCPI_ShouldSucceed(byte[] data, byte registerA, byte initialZ, byte initialS, byte initialP, byte initialCY, byte expectedZ, byte expectedS, byte expectedP, byte expectedCY)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init(data, 0);
        sut.State.A = registerA;
        sut.State.CC = new ConditionCodes()
        {
            Z = initialZ,
            S = initialS,
            P = initialP,
            CY = initialCY
        };

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(registerA, sut.State.A);
        Assert.Equal(2, sut.State.PC);
        Assert.Equal(expectedZ, sut.State.CC.Z);
        Assert.Equal(expectedS, sut.State.CC.S);
        Assert.Equal(expectedP, sut.State.CC.P);
        Assert.Equal(expectedCY, sut.State.CC.CY);
    }

    [Fact]
    public void Disassemble8080Op_WhenRLC_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x07], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tRLC{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenRRC_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x0F], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tRRC{Environment.NewLine}", debugOutput.Output);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetRRCData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenRRC_ShouldSucceed(byte initialA, byte initialCY, byte expectedA, byte expectedCY)
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x0F], 0);
        sut.State.A = initialA;
        sut.State.CC = new()
        {
            CY = initialCY
        };

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedA, sut.State.A);
        Assert.Equal(expectedCY, sut.State.CC.CY);
        Assert.Equal(1, sut.Cycles);
        Assert.Equal(4, sut.States);
    }

    [Fact]
    public void Disassemble8080Op_WhenRAL_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x17], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tRAL{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenRAR_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x1F], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tRAR{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenCMA_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x2F], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tCMA{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenCMC_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x3F], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tCMC{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenSTC_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x37], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tSTC{Environment.NewLine}", debugOutput.Output);
    }
}
