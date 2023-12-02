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
        Assert.Equal("0000\tADI\t#$01\r\n", debugOutput.Output);
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
        Assert.Equal("0000\tACI\t#$01\r\n", debugOutput.Output);
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
        Assert.Equal("0000\tSUI\t#$01\r\n", debugOutput.Output);
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
        Assert.Equal("0000\tSBI\t#$01\r\n", debugOutput.Output);
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
        Assert.Equal("0000\tDAA\r\n", debugOutput.Output);
    }
}
