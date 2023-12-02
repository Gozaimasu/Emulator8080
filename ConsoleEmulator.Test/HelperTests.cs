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
}