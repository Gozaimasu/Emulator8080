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

    public static IEnumerable<object[]> GetLXIData()
    {
        yield return new object[] { 2, 1, 0, 0, 0 };
        yield return new object[] { 1, 0, 1, 0, 1 };
        yield return new object[] { 0, 0xFF, 0, 1, 0 };
        yield return new object[] { 128, 127, 0, 0, 0 };
    }

    public static IEnumerable<object[]> GetINXData()
    {
        yield return new object[] { 0, 1 };
        yield return new object[] { 0xFF, 0x0100 };
        yield return new object[] { 0xFFFF, 0 };
    }

    public static IEnumerable<object[]> GetLDAXBData()
    {
        byte[] data = new byte[259];
        data[0] = 0x0A;
        data[258] = 0x01;
        yield return new object[] { data, 0x02, 0x01, 0x01 };
    }

    public static IEnumerable<object[]> GetLDAXDData()
    {
        byte[] data = new byte[259];
        data[0] = 0x1A;
        data[258] = 0x01;
        yield return new object[] { data, 0x02, 0x01, 0x01 };
    }
}
