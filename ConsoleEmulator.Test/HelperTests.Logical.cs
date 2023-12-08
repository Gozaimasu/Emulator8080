namespace ConsoleEmulator.Test;

public partial class HelperTests
{
    [Theory]
    [MemberData(nameof(DisassembleTestData.GetANAData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenANA_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Disassemble8080Op_WhenANI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xE6, 0x01], 0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal($"0000\tANI\t#$01{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Emulate8080Op_WhenANI_ShouldSucceed()
    {
        // Arrange
        State8080 state = new()
        {
            Memory = [0xE6, 0xA5],
            PC = 0,
            A = 0xF0
        };

        // Act
        int done = Helper.Emulate8080Op(ref state);

        // Assert
        Assert.Equal(0, done);
        Assert.Equal(0xA0, state.A);
        Assert.Equal(2, state.PC);
    }

    [Theory]
    [MemberData(nameof(DisassembleTestData.GetXRAData), MemberType = typeof(DisassembleTestData))]
    public void Disassemble8080Op_WhenXRA_ShouldSucceed(byte[] data, string expectedOutput)
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
    public void Disassemble8080Op_WhenXRI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xEE, 0x01], 0);

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
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op(data, 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal(expectedOutput, debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenORI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xF6, 0x01], 0);

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
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op(data, 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal(expectedOutput, debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenCPI_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0xFE, 0x01], 0);

        // Assert
        Assert.Equal(2, read);
        Assert.Equal($"0000\tCPI\t#$01{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenRLC_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0x07], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tRLC{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenRRC_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0x0F], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tRRC{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenRAL_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0x17], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tRAL{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenRAR_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0x1F], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tRAR{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenCMA_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0x2F], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tCMA{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenCMC_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0x3F], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tCMC{Environment.NewLine}", debugOutput.Output);
    }

    [Fact]
    public void Disassemble8080Op_WhenSTC_ShouldSucceed()
    {
        // Arrange
        TestDebugOutput debugOutput = new();
        Helper.DebugOutput = debugOutput;

        // Act
        int read = Helper.Disassemble8080Op([0x37], 0);

        // Assert
        Assert.Equal(1, read);
        Assert.Equal($"0000\tSTC{Environment.NewLine}", debugOutput.Output);
    }
}
