namespace ConsoleEmulator.Test;

public partial class HelperTests
{
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
    [MemberData(nameof(EmulateTestData.GetMOVBData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVB_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, state.B);
        Assert.Equal(1, state.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetMOVCData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVC_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, state.C);
        Assert.Equal(1, state.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetMOVDData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVD_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, state.D);
        Assert.Equal(1, state.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetMOVEData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVE_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, state.E);
        Assert.Equal(1, state.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetMOVHData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVH_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, state.H);
        Assert.Equal(1, state.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetMOVLData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVL_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, state.L);
        Assert.Equal(1, state.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetMOVMData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVM_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, state.Memory[(state.H << 8) + state.L]);
        Assert.Equal(1, state.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetMOVAData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVA_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, state.A);
        Assert.Equal(1, state.PC);
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

    [Fact]
    public void Emulate8080Op_WhenMVIB_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x06, 0x01],
            PC = 0
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, state.B);
        Assert.Equal(2, state.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenMVIC_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x0E, 0x01],
            PC = 0
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, state.C);
        Assert.Equal(2, state.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenMVID_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x16, 0x01],
            PC = 0
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, state.D);
        Assert.Equal(2, state.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenMVIE_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x1E, 0x01],
            PC = 0
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, state.E);
        Assert.Equal(2, state.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenMVIH_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x26, 0x01],
            PC = 0
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, state.H);
        Assert.Equal(2, state.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenMVIL_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x2E, 0x01],
            PC = 0
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, state.L);
        Assert.Equal(2, state.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenMVIA_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x3E, 0x01],
            PC = 0
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, state.A);
        Assert.Equal(2, state.PC);
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
    public void Emulate8080Op_WhenLXIB_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x01, 0x01, 0x02],
            PC = 0
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, state.C);
        Assert.Equal(0x02, state.B);
        Assert.Equal(3, state.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenLXID_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x11, 0x01, 0x02],
            PC = 0
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, state.E);
        Assert.Equal(0x02, state.D);
        Assert.Equal(3, state.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenLXIH_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x21, 0x01, 0x02],
            PC = 0
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, state.L);
        Assert.Equal(0x02, state.H);
        Assert.Equal(3, state.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenLXISP_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0x31, 0x01, 0x02],
            PC = 0
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x0201, state.SP);
        Assert.Equal(3, state.PC);
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
        Assert.Equal($"0000\tLDA\t$0201{Environment.NewLine}", debugOutput.Output);
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
        Assert.Equal($"0000\tSTA\t$0201{Environment.NewLine}", debugOutput.Output);
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
        Assert.Equal($"0000\tLHLD\t$0201{Environment.NewLine}", debugOutput.Output);
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
        Assert.Equal($"0000\tSHLD\t$0201{Environment.NewLine}", debugOutput.Output);
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
    [MemberData(nameof(EmulateTestData.GetLDAXBData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenLDAXB_ShouldSucceed(byte[] data, byte memoryLocationLow, byte memoryLocationHigh, byte expectedValue)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = data,
            PC = 0,
            C = memoryLocationLow,
            B = memoryLocationHigh
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, state.A);
        Assert.Equal(1, state.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetLDAXDData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenLDAXD_ShouldSucceed(byte[] data, byte memoryLocationLow, byte memoryLocationHigh, byte expectedValue)
    {
        // Arrange
        State8080 state = new()
        {
            Memory = data,
            PC = 0,
            E = memoryLocationLow,
            D = memoryLocationHigh
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, state.A);
        Assert.Equal(1, state.PC);
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
        Assert.Equal($"0000\tXCHG{Environment.NewLine}", debugOutput.Output);
    }
}