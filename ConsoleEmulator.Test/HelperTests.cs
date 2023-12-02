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

    // 01DDDSSS
    [Theory]
    [MemberData(nameof(DisassembleTestData.GetMoveData), MemberType = typeof(DisassembleTestData))]
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
}