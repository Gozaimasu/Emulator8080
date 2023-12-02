namespace ConsoleEmulator.Test;

internal class DisassembleTestData
{
    public static IEnumerable<object[]> GetMOVData()
    {
        yield return new object[] { new byte[1] { 0x40 }, "0000\tMOV\tB,B\r\n" };
        yield return new object[] { new byte[1] { 0x41 }, "0000\tMOV\tB,C\r\n" };
        yield return new object[] { new byte[1] { 0x42 }, "0000\tMOV\tB,D\r\n" };
        yield return new object[] { new byte[1] { 0x43 }, "0000\tMOV\tB,E\r\n" };
        yield return new object[] { new byte[1] { 0x44 }, "0000\tMOV\tB,H\r\n" };
        yield return new object[] { new byte[1] { 0x45 }, "0000\tMOV\tB,L\r\n" };
        yield return new object[] { new byte[1] { 0x46 }, "0000\tMOV\tB,M\r\n" };
        yield return new object[] { new byte[1] { 0x47 }, "0000\tMOV\tB,A\r\n" };

        yield return new object[] { new byte[1] { 0x48 }, "0000\tMOV\tC,B\r\n" };
        yield return new object[] { new byte[1] { 0x49 }, "0000\tMOV\tC,C\r\n" };
        yield return new object[] { new byte[1] { 0x4A }, "0000\tMOV\tC,D\r\n" };
        yield return new object[] { new byte[1] { 0x4B }, "0000\tMOV\tC,E\r\n" };
        yield return new object[] { new byte[1] { 0x4C }, "0000\tMOV\tC,H\r\n" };
        yield return new object[] { new byte[1] { 0x4D }, "0000\tMOV\tC,L\r\n" };
        yield return new object[] { new byte[1] { 0x4E }, "0000\tMOV\tC,M\r\n" };
        yield return new object[] { new byte[1] { 0x4F }, "0000\tMOV\tC,A\r\n" };

        yield return new object[] { new byte[1] { 0x50 }, "0000\tMOV\tD,B\r\n" };
        yield return new object[] { new byte[1] { 0x51 }, "0000\tMOV\tD,C\r\n" };
        yield return new object[] { new byte[1] { 0x52 }, "0000\tMOV\tD,D\r\n" };
        yield return new object[] { new byte[1] { 0x53 }, "0000\tMOV\tD,E\r\n" };
        yield return new object[] { new byte[1] { 0x54 }, "0000\tMOV\tD,H\r\n" };
        yield return new object[] { new byte[1] { 0x55 }, "0000\tMOV\tD,L\r\n" };
        yield return new object[] { new byte[1] { 0x56 }, "0000\tMOV\tD,M\r\n" };
        yield return new object[] { new byte[1] { 0x57 }, "0000\tMOV\tD,A\r\n" };

        yield return new object[] { new byte[1] { 0x58 }, "0000\tMOV\tE,B\r\n" };
        yield return new object[] { new byte[1] { 0x59 }, "0000\tMOV\tE,C\r\n" };
        yield return new object[] { new byte[1] { 0x5A }, "0000\tMOV\tE,D\r\n" };
        yield return new object[] { new byte[1] { 0x5B }, "0000\tMOV\tE,E\r\n" };
        yield return new object[] { new byte[1] { 0x5C }, "0000\tMOV\tE,H\r\n" };
        yield return new object[] { new byte[1] { 0x5D }, "0000\tMOV\tE,L\r\n" };
        yield return new object[] { new byte[1] { 0x5E }, "0000\tMOV\tE,M\r\n" };
        yield return new object[] { new byte[1] { 0x5F }, "0000\tMOV\tE,A\r\n" };

        yield return new object[] { new byte[1] { 0x60 }, "0000\tMOV\tH,B\r\n" };
        yield return new object[] { new byte[1] { 0x61 }, "0000\tMOV\tH,C\r\n" };
        yield return new object[] { new byte[1] { 0x62 }, "0000\tMOV\tH,D\r\n" };
        yield return new object[] { new byte[1] { 0x63 }, "0000\tMOV\tH,E\r\n" };
        yield return new object[] { new byte[1] { 0x64 }, "0000\tMOV\tH,H\r\n" };
        yield return new object[] { new byte[1] { 0x65 }, "0000\tMOV\tH,L\r\n" };
        yield return new object[] { new byte[1] { 0x66 }, "0000\tMOV\tH,M\r\n" };
        yield return new object[] { new byte[1] { 0x67 }, "0000\tMOV\tH,A\r\n" };

        yield return new object[] { new byte[1] { 0x68 }, "0000\tMOV\tL,B\r\n" };
        yield return new object[] { new byte[1] { 0x69 }, "0000\tMOV\tL,C\r\n" };
        yield return new object[] { new byte[1] { 0x6A }, "0000\tMOV\tL,D\r\n" };
        yield return new object[] { new byte[1] { 0x6B }, "0000\tMOV\tL,E\r\n" };
        yield return new object[] { new byte[1] { 0x6C }, "0000\tMOV\tL,H\r\n" };
        yield return new object[] { new byte[1] { 0x6D }, "0000\tMOV\tL,L\r\n" };
        yield return new object[] { new byte[1] { 0x6E }, "0000\tMOV\tL,M\r\n" };
        yield return new object[] { new byte[1] { 0x6F }, "0000\tMOV\tL,A\r\n" };

        yield return new object[] { new byte[1] { 0x70 }, "0000\tMOV\tM,B\r\n" };
        yield return new object[] { new byte[1] { 0x71 }, "0000\tMOV\tM,C\r\n" };
        yield return new object[] { new byte[1] { 0x72 }, "0000\tMOV\tM,D\r\n" };
        yield return new object[] { new byte[1] { 0x73 }, "0000\tMOV\tM,E\r\n" };
        yield return new object[] { new byte[1] { 0x74 }, "0000\tMOV\tM,H\r\n" };
        yield return new object[] { new byte[1] { 0x75 }, "0000\tMOV\tM,L\r\n" };

        yield return new object[] { new byte[1] { 0x77 }, "0000\tMOV\tM,A\r\n" };

        yield return new object[] { new byte[1] { 0x78 }, "0000\tMOV\tA,B\r\n" };
        yield return new object[] { new byte[1] { 0x79 }, "0000\tMOV\tA,C\r\n" };
        yield return new object[] { new byte[1] { 0x7A }, "0000\tMOV\tA,D\r\n" };
        yield return new object[] { new byte[1] { 0x7B }, "0000\tMOV\tA,E\r\n" };
        yield return new object[] { new byte[1] { 0x7C }, "0000\tMOV\tA,H\r\n" };
        yield return new object[] { new byte[1] { 0x7D }, "0000\tMOV\tA,L\r\n" };
        yield return new object[] { new byte[1] { 0x7E }, "0000\tMOV\tA,M\r\n" };
        yield return new object[] { new byte[1] { 0x7F }, "0000\tMOV\tA,A\r\n" };

    }

    public static IEnumerable<object[]> GetMVIData()
    {
        yield return new object[] { new byte[2] { 0x06, 0x01 }, "0000\tMVI\tB,#$01\r\n" };
        yield return new object[] { new byte[2] { 0x0E, 0x01 }, "0000\tMVI\tC,#$01\r\n" };
        yield return new object[] { new byte[2] { 0x16, 0x01 }, "0000\tMVI\tD,#$01\r\n" };
        yield return new object[] { new byte[2] { 0x1E, 0x01 }, "0000\tMVI\tE,#$01\r\n" };
        yield return new object[] { new byte[2] { 0x26, 0x01 }, "0000\tMVI\tH,#$01\r\n" };
        yield return new object[] { new byte[2] { 0x2E, 0x01 }, "0000\tMVI\tL,#$01\r\n" };
        yield return new object[] { new byte[2] { 0x36, 0x01 }, "0000\tMVI\tM,#$01\r\n" };
        yield return new object[] { new byte[2] { 0x3E, 0x01 }, "0000\tMVI\tA,#$01\r\n" };
    }

    public static IEnumerable<object[]> GetLXIData()
    {
        yield return new object[] { new byte[3] { 0x01, 0x01, 0x02 }, "0000\tLXI\tB,#$0201\r\n" };
        yield return new object[] { new byte[3] { 0x11, 0x01, 0x02 }, "0000\tLXI\tD,#$0201\r\n" };
        yield return new object[] { new byte[3] { 0x21, 0x01, 0x02 }, "0000\tLXI\tH,#$0201\r\n" };
        yield return new object[] { new byte[3] { 0x31, 0x01, 0x02 }, "0000\tLXI\tSP,#$0201\r\n" };
    }

    public static IEnumerable<object[]> GetLDAXData()
    {
        yield return new object[] { new byte[1] { 0x0A }, "0000\tLDAX\tB\r\n" };
        yield return new object[] { new byte[1] { 0x1A }, "0000\tLDAX\tD\r\n" };
    }

    public static IEnumerable<object[]> GetSTAXData()
    {
        yield return new object[] { new byte[1] { 0x02 }, "0000\tSTAX\tB\r\n" };
        yield return new object[] { new byte[1] { 0x12 }, "0000\tSTAX\tD\r\n" };
    }

    public static IEnumerable<object[]> GetADDData()
    {
        yield return new object[] { new byte[1] { 0x80 }, "0000\tADD\tB\r\n" };
        yield return new object[] { new byte[1] { 0x81 }, "0000\tADD\tC\r\n" };
        yield return new object[] { new byte[1] { 0x82 }, "0000\tADD\tD\r\n" };
        yield return new object[] { new byte[1] { 0x83 }, "0000\tADD\tE\r\n" };
        yield return new object[] { new byte[1] { 0x84 }, "0000\tADD\tH\r\n" };
        yield return new object[] { new byte[1] { 0x85 }, "0000\tADD\tL\r\n" };
        yield return new object[] { new byte[1] { 0x86 }, "0000\tADD\tM\r\n" };
        yield return new object[] { new byte[1] { 0x87 }, "0000\tADD\tA\r\n" };
    }

    public static IEnumerable<object[]> GetADCData()
    {
        yield return new object[] { new byte[1] { 0x88 }, "0000\tADC\tB\r\n" };
        yield return new object[] { new byte[1] { 0x89 }, "0000\tADC\tC\r\n" };
        yield return new object[] { new byte[1] { 0x8A }, "0000\tADC\tD\r\n" };
        yield return new object[] { new byte[1] { 0x8B }, "0000\tADC\tE\r\n" };
        yield return new object[] { new byte[1] { 0x8C }, "0000\tADC\tH\r\n" };
        yield return new object[] { new byte[1] { 0x8D }, "0000\tADC\tL\r\n" };
        yield return new object[] { new byte[1] { 0x8E }, "0000\tADC\tM\r\n" };
        yield return new object[] { new byte[1] { 0x8F }, "0000\tADC\tA\r\n" };
    }

    public static IEnumerable<object[]> GetSUBData()
    {
        yield return new object[] { new byte[1] { 0x90 }, "0000\tSUB\tB\r\n" };
        yield return new object[] { new byte[1] { 0x91 }, "0000\tSUB\tC\r\n" };
        yield return new object[] { new byte[1] { 0x92 }, "0000\tSUB\tD\r\n" };
        yield return new object[] { new byte[1] { 0x93 }, "0000\tSUB\tE\r\n" };
        yield return new object[] { new byte[1] { 0x94 }, "0000\tSUB\tH\r\n" };
        yield return new object[] { new byte[1] { 0x95 }, "0000\tSUB\tL\r\n" };
        yield return new object[] { new byte[1] { 0x96 }, "0000\tSUB\tM\r\n" };
        yield return new object[] { new byte[1] { 0x97 }, "0000\tSUB\tA\r\n" };
    }

    public static IEnumerable<object[]> GetSBBData()
    {
        yield return new object[] { new byte[1] { 0x98 }, "0000\tSBB\tB\r\n" };
        yield return new object[] { new byte[1] { 0x99 }, "0000\tSBB\tC\r\n" };
        yield return new object[] { new byte[1] { 0x9A }, "0000\tSBB\tD\r\n" };
        yield return new object[] { new byte[1] { 0x9B }, "0000\tSBB\tE\r\n" };
        yield return new object[] { new byte[1] { 0x9C }, "0000\tSBB\tH\r\n" };
        yield return new object[] { new byte[1] { 0x9D }, "0000\tSBB\tL\r\n" };
        yield return new object[] { new byte[1] { 0x9E }, "0000\tSBB\tM\r\n" };
        yield return new object[] { new byte[1] { 0x9F }, "0000\tSBB\tA\r\n" };
    }

    public static IEnumerable<object[]> GetINRData()
    {
        yield return new object[] { new byte[1] { 0x04 }, "0000\tINR\tB\r\n" };
        yield return new object[] { new byte[1] { 0x0C }, "0000\tINR\tC\r\n" };
        yield return new object[] { new byte[1] { 0x14 }, "0000\tINR\tD\r\n" };
        yield return new object[] { new byte[1] { 0x1C }, "0000\tINR\tE\r\n" };
        yield return new object[] { new byte[1] { 0x24 }, "0000\tINR\tH\r\n" };
        yield return new object[] { new byte[1] { 0x2C }, "0000\tINR\tL\r\n" };
        yield return new object[] { new byte[1] { 0x34 }, "0000\tINR\tM\r\n" };
        yield return new object[] { new byte[1] { 0x3C }, "0000\tINR\tA\r\n" };
    }

    public static IEnumerable<object[]> GetDCRData()
    {
        yield return new object[] { new byte[1] { 0x05 }, "0000\tDCR\tB\r\n" };
        yield return new object[] { new byte[1] { 0x0D }, "0000\tDCR\tC\r\n" };
        yield return new object[] { new byte[1] { 0x15 }, "0000\tDCR\tD\r\n" };
        yield return new object[] { new byte[1] { 0x1D }, "0000\tDCR\tE\r\n" };
        yield return new object[] { new byte[1] { 0x25 }, "0000\tDCR\tH\r\n" };
        yield return new object[] { new byte[1] { 0x2D }, "0000\tDCR\tL\r\n" };
        yield return new object[] { new byte[1] { 0x35 }, "0000\tDCR\tM\r\n" };
        yield return new object[] { new byte[1] { 0x3D }, "0000\tDCR\tA\r\n" };
    }

    public static IEnumerable<object[]> GetINXData()
    {
        yield return new object[] { new byte[1] { 0x03 }, "0000\tINX\tB\r\n" };
        yield return new object[] { new byte[1] { 0x13 }, "0000\tINX\tD\r\n" };
        yield return new object[] { new byte[1] { 0x23 }, "0000\tINX\tH\r\n" };
        yield return new object[] { new byte[1] { 0x33 }, "0000\tINX\tSP\r\n" };
    }

    public static IEnumerable<object[]> GetDCXData()
    {
        yield return new object[] { new byte[1] { 0x0B }, "0000\tDCX\tB\r\n" };
        yield return new object[] { new byte[1] { 0x1B }, "0000\tDCX\tD\r\n" };
        yield return new object[] { new byte[1] { 0x2B }, "0000\tDCX\tH\r\n" };
        yield return new object[] { new byte[1] { 0x3B }, "0000\tDCX\tSP\r\n" };
    }

    public static IEnumerable<object[]> GetDADData()
    {
        yield return new object[] { new byte[1] { 0x09 }, "0000\tDAD\tB\r\n" };
        yield return new object[] { new byte[1] { 0x19 }, "0000\tDAD\tD\r\n" };
        yield return new object[] { new byte[1] { 0x29 }, "0000\tDAD\tH\r\n" };
        yield return new object[] { new byte[1] { 0x39 }, "0000\tDAD\tSP\r\n" };
    }
}
