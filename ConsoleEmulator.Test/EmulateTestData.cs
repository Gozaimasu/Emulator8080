namespace ConsoleEmulator.Test;

internal class EmulateTestData
{
    public static IEnumerable<object[]> GetDCRData()
    {
        yield return new object[] { 2, 1, 0, 0, 0 };
        yield return new object[] { 1, 0, 1, 0, 1 };
        yield return new object[] { 0, 0xFF, 0, 1, 0 };
        yield return new object[] { 128, 127, 0, 0, 0 };
    }
}
