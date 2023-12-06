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

    public static IEnumerable<object[]> GetMOVBData()
    {
        yield return new object[] { new byte[] { 0x40 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x00 /* expected */ };
        yield return new object[] { new byte[] { 0x41 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x01 /* expected */ };
        yield return new object[] { new byte[] { 0x42 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x02 /* expected */ };
        yield return new object[] { new byte[] { 0x43 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x03 /* expected */ };
        yield return new object[] { new byte[] { 0x44 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x04 /* expected */ };
        yield return new object[] { new byte[] { 0x45 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x05 /* expected */ };
        yield return new object[] { new byte[] { 0x47 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x06 /* expected */ };
    }

    public static IEnumerable<object[]> GetMOVCData()
    {
        yield return new object[] { new byte[] { 0x48 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x00 /* expected */ };
        yield return new object[] { new byte[] { 0x49 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x01 /* expected */ };
        yield return new object[] { new byte[] { 0x4A }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x02 /* expected */ };
        yield return new object[] { new byte[] { 0x4B }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x03 /* expected */ };
        yield return new object[] { new byte[] { 0x4C }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x04 /* expected */ };
        yield return new object[] { new byte[] { 0x4D }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x05 /* expected */ };
        yield return new object[] { new byte[] { 0x4F }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x06 /* expected */ };
    }

    public static IEnumerable<object[]> GetMOVDData()
    {
        yield return new object[] { new byte[] { 0x50 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x00 /* expected */ };
        yield return new object[] { new byte[] { 0x51 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x01 /* expected */ };
        yield return new object[] { new byte[] { 0x52 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x02 /* expected */ };
        yield return new object[] { new byte[] { 0x53 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x03 /* expected */ };
        yield return new object[] { new byte[] { 0x54 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x04 /* expected */ };
        yield return new object[] { new byte[] { 0x55 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x05 /* expected */ };
        yield return new object[] { new byte[] { 0x57 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x06 /* expected */ };
    }

    public static IEnumerable<object[]> GetMOVEData()
    {
        yield return new object[] { new byte[] { 0x58 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x00 /* expected */ };
        yield return new object[] { new byte[] { 0x59 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x01 /* expected */ };
        yield return new object[] { new byte[] { 0x5A }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x02 /* expected */ };
        yield return new object[] { new byte[] { 0x5B }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x03 /* expected */ };
        yield return new object[] { new byte[] { 0x5C }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x04 /* expected */ };
        yield return new object[] { new byte[] { 0x5D }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x05 /* expected */ };
        yield return new object[] { new byte[] { 0x5F }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x06 /* expected */ };
    }

    public static IEnumerable<object[]> GetMOVHData()
    {
        yield return new object[] { new byte[] { 0x60 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x00 /* expected */ };
        yield return new object[] { new byte[] { 0x61 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x01 /* expected */ };
        yield return new object[] { new byte[] { 0x62 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x02 /* expected */ };
        yield return new object[] { new byte[] { 0x63 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x03 /* expected */ };
        yield return new object[] { new byte[] { 0x64 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x04 /* expected */ };
        yield return new object[] { new byte[] { 0x65 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x05 /* expected */ };
        yield return new object[] { new byte[] { 0x67 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x06 /* expected */ };
    }

    public static IEnumerable<object[]> GetMOVLData()
    {
        yield return new object[] { new byte[] { 0x68 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x00 /* expected */ };
        yield return new object[] { new byte[] { 0x69 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x01 /* expected */ };
        yield return new object[] { new byte[] { 0x6A }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x02 /* expected */ };
        yield return new object[] { new byte[] { 0x6B }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x03 /* expected */ };
        yield return new object[] { new byte[] { 0x6C }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x04 /* expected */ };
        yield return new object[] { new byte[] { 0x6D }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x05 /* expected */ };
        yield return new object[] { new byte[] { 0x6F }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x06 /* expected */ };
    }

    public static IEnumerable<object[]> GetMOVMData()
    {
        byte[] dataB = new byte[259];
        dataB[0] = 0x70;
        dataB[258] = 0x00;
        yield return new object[] { dataB, 0x03 /* B */, 0x04 /* C */, 0x05 /* D */, 0x06 /* E */, 0x01 /* H */, 0x02 /* L */, 0x07 /* A */, 0x03 /* expected */ };
        byte[] dataC = new byte[259];
        dataC[0] = 0x71;
        dataC[258] = 0x00;
        yield return new object[] { dataC, 0x03 /* B */, 0x04 /* C */, 0x05 /* D */, 0x06 /* E */, 0x01 /* H */, 0x02 /* L */, 0x07 /* A */, 0x04 /* expected */ };
        byte[] dataD = new byte[259];
        dataD[0] = 0x72;
        dataD[258] = 0x00;
        yield return new object[] { dataD, 0x03 /* B */, 0x04 /* C */, 0x05 /* D */, 0x06 /* E */, 0x01 /* H */, 0x02 /* L */, 0x07 /* A */, 0x05 /* expected */ };
        byte[] dataE = new byte[259];
        dataE[0] = 0x73;
        dataE[258] = 0x00;
        yield return new object[] { dataE, 0x03 /* B */, 0x04 /* C */, 0x05 /* D */, 0x06 /* E */, 0x01 /* H */, 0x02 /* L */, 0x07 /* A */, 0x06 /* expected */ };
        byte[] dataH = new byte[259];
        dataH[0] = 0x74;
        dataH[258] = 0x00;
        yield return new object[] { dataH, 0x03 /* B */, 0x04 /* C */, 0x05 /* D */, 0x06 /* E */, 0x01 /* H */, 0x02 /* L */, 0x07 /* A */, 0x01 /* expected */ };
        byte[] dataL = new byte[259];
        dataL[0] = 0x75;
        dataL[258] = 0x00;
        yield return new object[] { dataL, 0x03 /* B */, 0x04 /* C */, 0x05 /* D */, 0x06 /* E */, 0x01 /* H */, 0x02 /* L */, 0x07 /* A */, 0x02 /* expected */ };
        byte[] dataA = new byte[259];
        dataA[0] = 0x77;
        dataA[258] = 0x00;
        yield return new object[] { dataA, 0x03 /* B */, 0x04 /* C */, 0x05 /* D */, 0x06 /* E */, 0x01 /* H */, 0x02 /* L */, 0x07 /* A */, 0x07 /* expected */ };
    }

    public static IEnumerable<object[]> GetMOVAData()
    {
        yield return new object[] { new byte[] { 0x78 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x00 /* expected */ };
        yield return new object[] { new byte[] { 0x79 }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x01 /* expected */ };
        yield return new object[] { new byte[] { 0x7A }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x02 /* expected */ };
        yield return new object[] { new byte[] { 0x7B }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x03 /* expected */ };
        yield return new object[] { new byte[] { 0x7C }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x04 /* expected */ };
        yield return new object[] { new byte[] { 0x7D }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x05 /* expected */ };
        yield return new object[] { new byte[] { 0x7F }, 0x00 /* B */, 0x01 /* C */, 0x02 /* D */, 0x03 /* E */, 0x04 /* H */, 0x05 /* L */, 0x06 /* A */, 0x06 /* expected */ };
    }
}
