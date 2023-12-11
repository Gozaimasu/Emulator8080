using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleEmulator.Test;

public partial class CPU8080Tests
{
    [Theory]
    [MemberData(nameof(DisassembleTestData.GetMOVData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenMOV_ShouldSucceed(byte[] data, string expectedOutput)
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
    [MemberData(nameof(EmulateTestData.GetMOVBData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVB_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        sut.State.A = initialA;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, sut.State.B);
        Assert.Equal(1, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetMOVCData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVC_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        sut.State.A = initialA;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, sut.State.C);
        Assert.Equal(1, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetMOVDData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVD_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        sut.State.A = initialA;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, sut.State.D);
        Assert.Equal(1, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetMOVEData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVE_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        sut.State.A = initialA;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, sut.State.E);
        Assert.Equal(1, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetMOVHData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVH_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        sut.State.A = initialA;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, sut.State.H);
        Assert.Equal(1, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetMOVLData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVL_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        sut.State.A = initialA;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, sut.State.L);
        Assert.Equal(1, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetMOVMData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVM_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        sut.State.A = initialA;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, sut.Memory[(sut.State.H << 8) + sut.State.L]);
        Assert.Equal(1, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetMOVAData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenMOVA_ShouldSucceed(byte[] data, byte initialB, byte initialC, byte initialD, byte initialE, byte initialH, byte initialL, byte initialA, byte expectedValue)
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
        sut.State.A = initialA;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, sut.State.A);
        Assert.Equal(1, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetMVIData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenMVI_ShouldSucceed(byte[] data, string expectedOutput)
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init(data, 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal(expectedOutput, debugOutput.Output);
    }

    [Fact]
    public void Emulate8080Op_WhenMVIB_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x06, 0x01], 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, sut.State.B);
        Assert.Equal(2, sut.State.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenMVIC_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x0E, 0x01], 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, sut.State.C);
        Assert.Equal(2, sut.State.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenMVID_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x16, 0x01], 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, sut.State.D);
        Assert.Equal(2, sut.State.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenMVIE_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x1E, 0x01], 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, sut.State.E);
        Assert.Equal(2, sut.State.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenMVIH_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x26, 0x01], 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, sut.State.H);
        Assert.Equal(2, sut.State.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenMVIL_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x2E, 0x01], 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, sut.State.L);
        Assert.Equal(2, sut.State.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenMVIM_ShouldSucceed()
    {
        byte[] data = new byte[259];
        data[0] = 0x36;
        data[1] = 0x01;
        data[258] = 0x00;
        // Arrange
        CPU8080 sut = new();
        sut.Init(data, 0);
        sut.State.H = 1;
        sut.State.L = 2;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, sut.Memory[258]);
        Assert.Equal(2, sut.State.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenMVIA_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x3E, 0x01], 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, sut.State.A);
        Assert.Equal(2, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetLXIData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenLXI_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Emulate8080Op_WhenLXIB_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x01, 0x01, 0x02], 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, sut.State.C);
        Assert.Equal(0x02, sut.State.B);
        Assert.Equal(3, sut.State.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenLXID_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x11, 0x01, 0x02], 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, sut.State.E);
        Assert.Equal(0x02, sut.State.D);
        Assert.Equal(3, sut.State.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenLXIH_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x21, 0x01, 0x02], 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, sut.State.L);
        Assert.Equal(0x02, sut.State.H);
        Assert.Equal(3, sut.State.PC);
    }

    [Fact]
    public void Emulate8080Op_WhenLXISP_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x31, 0x01, 0x02], 0);

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x0201, sut.State.SP);
        Assert.Equal(3, sut.State.PC);
    }

    [Fact]
    public void Disassemble8080Op_WhenLDA_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x3A, 0x01, 0x02], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal($"0000\tLDA\t$0201{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenSTA_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x32, 0x01, 0x02], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal($"0000\tSTA\t$0201{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenLHLD_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x2A, 0x01, 0x02], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(3, read);
        Assert.Equal($"0000\tLHLD\t$0201{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenSHLD_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0x22, 0x01, 0x02], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

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
    [MemberData(nameof(EmulateTestData.GetLDAXBData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenLDAXB_ShouldSucceed(byte[] data, byte memoryLocationLow, byte memoryLocationHigh, byte expectedValue)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init(data, 0);
        sut.State.C = memoryLocationLow;
        sut.State.B = memoryLocationHigh;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, sut.State.A);
        Assert.Equal(1, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(EmulateTestData.GetLDAXDData), MemberType = typeof(EmulateTestData))]
    public void Emulate8080Op_WhenLDAXD_ShouldSucceed(byte[] data, byte memoryLocationLow, byte memoryLocationHigh, byte expectedValue)
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init(data, 0);
        sut.State.E = memoryLocationLow;
        sut.State.D = memoryLocationHigh;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(expectedValue, sut.State.A);
        Assert.Equal(1, sut.State.PC);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetSTAXData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenSTAX_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Disassemble8080Op_WhenXCHG_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        CPU8080.DebugOutput = debugOutput;
        CPU8080 sut = new();
        sut.Init([0xEB], 0);

        // Act
        int read = sut.Disassemble8080Op(0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tXCHG{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Emulate8080Op_WhenXCHG_ShouldSucceed()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0xEB], 0);
        sut.State.D = 0x01;
        sut.State.E = 0x02;
        sut.State.H = 0x03;
        sut.State.L = 0x04;

        // Act
        int done = sut.Step();

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0x01, sut.State.H);
        Assert.Equal(0x02, sut.State.L);
        Assert.Equal(0x03, sut.State.D);
        Assert.Equal(0x04, sut.State.E);
        Assert.Equal(1, sut.State.PC);
    }
}