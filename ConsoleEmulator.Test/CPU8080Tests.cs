namespace ConsoleEmulator.Test;

public partial class CPU8080Tests
{
    [Fact]
    public void Emulate8080Op_WhenNOP_ShouldDoNothing()
    {
        // Arrange
        CPU8080 sut = new();
        sut.Init([0x00], 0);
        sut.State.A = 1;
        sut.State.B = 1;
        sut.State.C = 1;
        sut.State.D = 1;
        sut.State.E = 1;
        sut.State.H = 1;
        sut.State.L = 1;
        sut.State.SP = 1;
        sut.State.IntEnable = 1;
        sut.State.CC = new ConditionCodes()
        {
            AC = 1,
            CY = 1,
            P = 1,
            PAD = 1,
            S = 1,
            Z = 1
        };

        // Act
        sut.Step();

        // Assert
        Assert.Equal(1, sut.State.A);
        Assert.Equal(1, sut.State.B);
        Assert.Equal(1, sut.State.C);
        Assert.Equal(1, sut.State.D);
        Assert.Equal(1, sut.State.E);
        Assert.Equal(1, sut.State.H);
        Assert.Equal(1, sut.State.L);
        Assert.Equal(1, sut.State.SP);
        Assert.Equal(1, sut.State.IntEnable);
        Assert.Equal(1, sut.State.CC.AC);
        Assert.Equal(1, sut.State.CC.CY);
        Assert.Equal(1, sut.State.CC.P);
        Assert.Equal(1, sut.State.CC.PAD);
        Assert.Equal(1, sut.State.CC.S);
        Assert.Equal(1, sut.State.CC.Z);
        Assert.Equal(1, sut.State.PC);
    }

}
