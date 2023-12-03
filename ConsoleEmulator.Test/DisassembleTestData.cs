namespace ConsoleEmulator.Test;

internal class DisassembleTestData
{
    public static IEnumerable<object[]> GetMOVData()
    {
        yield return new object[] { new byte[1] { 0x40 }, $"0000\tMOV\tB,B{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x41 }, $"0000\tMOV\tB,C{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x42 }, $"0000\tMOV\tB,D{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x43 }, $"0000\tMOV\tB,E{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x44 }, $"0000\tMOV\tB,H{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x45 }, $"0000\tMOV\tB,L{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x46 }, $"0000\tMOV\tB,M{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x47 }, $"0000\tMOV\tB,A{Environment.NewLine}" };

        yield return new object[] { new byte[1] { 0x48 }, $"0000\tMOV\tC,B{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x49 }, $"0000\tMOV\tC,C{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x4A }, $"0000\tMOV\tC,D{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x4B }, $"0000\tMOV\tC,E{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x4C }, $"0000\tMOV\tC,H{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x4D }, $"0000\tMOV\tC,L{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x4E }, $"0000\tMOV\tC,M{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x4F }, $"0000\tMOV\tC,A{Environment.NewLine}" };

        yield return new object[] { new byte[1] { 0x50 }, $"0000\tMOV\tD,B{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x51 }, $"0000\tMOV\tD,C{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x52 }, $"0000\tMOV\tD,D{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x53 }, $"0000\tMOV\tD,E{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x54 }, $"0000\tMOV\tD,H{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x55 }, $"0000\tMOV\tD,L{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x56 }, $"0000\tMOV\tD,M{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x57 }, $"0000\tMOV\tD,A{Environment.NewLine}" };

        yield return new object[] { new byte[1] { 0x58 }, $"0000\tMOV\tE,B{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x59 }, $"0000\tMOV\tE,C{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x5A }, $"0000\tMOV\tE,D{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x5B }, $"0000\tMOV\tE,E{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x5C }, $"0000\tMOV\tE,H{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x5D }, $"0000\tMOV\tE,L{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x5E }, $"0000\tMOV\tE,M{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x5F }, $"0000\tMOV\tE,A{Environment.NewLine}" };

        yield return new object[] { new byte[1] { 0x60 }, $"0000\tMOV\tH,B{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x61 }, $"0000\tMOV\tH,C{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x62 }, $"0000\tMOV\tH,D{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x63 }, $"0000\tMOV\tH,E{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x64 }, $"0000\tMOV\tH,H{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x65 }, $"0000\tMOV\tH,L{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x66 }, $"0000\tMOV\tH,M{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x67 }, $"0000\tMOV\tH,A{Environment.NewLine}" };

        yield return new object[] { new byte[1] { 0x68 }, $"0000\tMOV\tL,B{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x69 }, $"0000\tMOV\tL,C{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x6A }, $"0000\tMOV\tL,D{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x6B }, $"0000\tMOV\tL,E{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x6C }, $"0000\tMOV\tL,H{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x6D }, $"0000\tMOV\tL,L{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x6E }, $"0000\tMOV\tL,M{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x6F }, $"0000\tMOV\tL,A{Environment.NewLine}" };

        yield return new object[] { new byte[1] { 0x70 }, $"0000\tMOV\tM,B{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x71 }, $"0000\tMOV\tM,C{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x72 }, $"0000\tMOV\tM,D{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x73 }, $"0000\tMOV\tM,E{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x74 }, $"0000\tMOV\tM,H{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x75 }, $"0000\tMOV\tM,L{Environment.NewLine}" };

        yield return new object[] { new byte[1] { 0x77 }, $"0000\tMOV\tM,A{Environment.NewLine}" };

        yield return new object[] { new byte[1] { 0x78 }, $"0000\tMOV\tA,B{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x79 }, $"0000\tMOV\tA,C{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x7A }, $"0000\tMOV\tA,D{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x7B }, $"0000\tMOV\tA,E{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x7C }, $"0000\tMOV\tA,H{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x7D }, $"0000\tMOV\tA,L{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x7E }, $"0000\tMOV\tA,M{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x7F }, $"0000\tMOV\tA,A{Environment.NewLine}" };

    }

    public static IEnumerable<object[]> GetMVIData()
    {
        yield return new object[] { new byte[2] { 0x06, 0x01 }, $"0000\tMVI\tB,#$01{Environment.NewLine}" };
        yield return new object[] { new byte[2] { 0x0E, 0x01 }, $"0000\tMVI\tC,#$01{Environment.NewLine}" };
        yield return new object[] { new byte[2] { 0x16, 0x01 }, $"0000\tMVI\tD,#$01{Environment.NewLine}" };
        yield return new object[] { new byte[2] { 0x1E, 0x01 }, $"0000\tMVI\tE,#$01{Environment.NewLine}" };
        yield return new object[] { new byte[2] { 0x26, 0x01 }, $"0000\tMVI\tH,#$01{Environment.NewLine}" };
        yield return new object[] { new byte[2] { 0x2E, 0x01 }, $"0000\tMVI\tL,#$01{Environment.NewLine}" };
        yield return new object[] { new byte[2] { 0x36, 0x01 }, $"0000\tMVI\tM,#$01{Environment.NewLine}" };
        yield return new object[] { new byte[2] { 0x3E, 0x01 }, $"0000\tMVI\tA,#$01{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetLXIData()
    {
        yield return new object[] { new byte[3] { 0x01, 0x01, 0x02 }, $"0000\tLXI\tB,#$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0x11, 0x01, 0x02 }, $"0000\tLXI\tD,#$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0x21, 0x01, 0x02 }, $"0000\tLXI\tH,#$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0x31, 0x01, 0x02 }, $"0000\tLXI\tSP,#$0201{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetLDAXData()
    {
        yield return new object[] { new byte[1] { 0x0A }, $"0000\tLDAX\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x1A }, $"0000\tLDAX\tD{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetSTAXData()
    {
        yield return new object[] { new byte[1] { 0x02 }, $"0000\tSTAX\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x12 }, $"0000\tSTAX\tD{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetADDData()
    {
        yield return new object[] { new byte[1] { 0x80 }, $"0000\tADD\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x81 }, $"0000\tADD\tC{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x82 }, $"0000\tADD\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x83 }, $"0000\tADD\tE{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x84 }, $"0000\tADD\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x85 }, $"0000\tADD\tL{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x86 }, $"0000\tADD\tM{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x87 }, $"0000\tADD\tA{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetADCData()
    {
        yield return new object[] { new byte[1] { 0x88 }, $"0000\tADC\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x89 }, $"0000\tADC\tC{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x8A }, $"0000\tADC\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x8B }, $"0000\tADC\tE{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x8C }, $"0000\tADC\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x8D }, $"0000\tADC\tL{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x8E }, $"0000\tADC\tM{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x8F }, $"0000\tADC\tA{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetSUBData()
    {
        yield return new object[] { new byte[1] { 0x90 }, $"0000\tSUB\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x91 }, $"0000\tSUB\tC{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x92 }, $"0000\tSUB\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x93 }, $"0000\tSUB\tE{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x94 }, $"0000\tSUB\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x95 }, $"0000\tSUB\tL{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x96 }, $"0000\tSUB\tM{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x97 }, $"0000\tSUB\tA{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetSBBData()
    {
        yield return new object[] { new byte[1] { 0x98 }, $"0000\tSBB\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x99 }, $"0000\tSBB\tC{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x9A }, $"0000\tSBB\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x9B }, $"0000\tSBB\tE{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x9C }, $"0000\tSBB\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x9D }, $"0000\tSBB\tL{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x9E }, $"0000\tSBB\tM{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x9F }, $"0000\tSBB\tA{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetINRData()
    {
        yield return new object[] { new byte[1] { 0x04 }, $"0000\tINR\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x0C }, $"0000\tINR\tC{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x14 }, $"0000\tINR\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x1C }, $"0000\tINR\tE{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x24 }, $"0000\tINR\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x2C }, $"0000\tINR\tL{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x34 }, $"0000\tINR\tM{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x3C }, $"0000\tINR\tA{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetDCRData()
    {
        yield return new object[] { new byte[1] { 0x05 }, $"0000\tDCR\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x0D }, $"0000\tDCR\tC{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x15 }, $"0000\tDCR\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x1D }, $"0000\tDCR\tE{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x25 }, $"0000\tDCR\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x2D }, $"0000\tDCR\tL{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x35 }, $"0000\tDCR\tM{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x3D }, $"0000\tDCR\tA{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetINXData()
    {
        yield return new object[] { new byte[1] { 0x03 }, $"0000\tINX\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x13 }, $"0000\tINX\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x23 }, $"0000\tINX\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x33 }, $"0000\tINX\tSP{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetDCXData()
    {
        yield return new object[] { new byte[1] { 0x0B }, $"0000\tDCX\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x1B }, $"0000\tDCX\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x2B }, $"0000\tDCX\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x3B }, $"0000\tDCX\tSP{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetDADData()
    {
        yield return new object[] { new byte[1] { 0x09 }, $"0000\tDAD\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x19 }, $"0000\tDAD\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x29 }, $"0000\tDAD\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0x39 }, $"0000\tDAD\tSP{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetANAData()
    {
        yield return new object[] { new byte[1] { 0xA0 }, $"0000\tANA\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xA1 }, $"0000\tANA\tC{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xA2 }, $"0000\tANA\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xA3 }, $"0000\tANA\tE{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xA4 }, $"0000\tANA\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xA5 }, $"0000\tANA\tL{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xA6 }, $"0000\tANA\tM{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xA7 }, $"0000\tANA\tA{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetXRAData()
    {
        yield return new object[] { new byte[1] { 0xA8 }, $"0000\tXRA\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xA9 }, $"0000\tXRA\tC{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xAA }, $"0000\tXRA\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xAB }, $"0000\tXRA\tE{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xAC }, $"0000\tXRA\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xAD }, $"0000\tXRA\tL{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xAE }, $"0000\tXRA\tM{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xAF }, $"0000\tXRA\tA{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetORAData()
    {
        yield return new object[] { new byte[1] { 0xB0 }, $"0000\tORA\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xB1 }, $"0000\tORA\tC{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xB2 }, $"0000\tORA\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xB3 }, $"0000\tORA\tE{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xB4 }, $"0000\tORA\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xB5 }, $"0000\tORA\tL{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xB6 }, $"0000\tORA\tM{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xB7 }, $"0000\tORA\tA{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetCMPData()
    {
        yield return new object[] { new byte[1] { 0xB8 }, $"0000\tCMP\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xB9 }, $"0000\tCMP\tC{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xBA }, $"0000\tCMP\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xBB }, $"0000\tCMP\tE{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xBC }, $"0000\tCMP\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xBD }, $"0000\tCMP\tL{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xBE }, $"0000\tCMP\tM{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xBF }, $"0000\tCMP\tA{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetJConditionData()
    {
        yield return new object[] { new byte[3] { 0xC2, 0x01, 0x02 }, $"0000\tJNZ\t$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0xCA, 0x01, 0x02 }, $"0000\tJZ\t$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0xD2, 0x01, 0x02 }, $"0000\tJNC\t$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0xDA, 0x01, 0x02 }, $"0000\tJC\t$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0xE2, 0x01, 0x02 }, $"0000\tJPO\t$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0xEA, 0x01, 0x02 }, $"0000\tJPE\t$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0xF2, 0x01, 0x02 }, $"0000\tJP\t$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0xFA, 0x01, 0x02 }, $"0000\tJM\t$0201{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetCConditionData()
    {
        yield return new object[] { new byte[3] { 0xC4, 0x01, 0x02 }, $"0000\tCNZ\t$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0xCC, 0x01, 0x02 }, $"0000\tCZ\t$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0xD4, 0x01, 0x02 }, $"0000\tCNC\t$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0xDC, 0x01, 0x02 }, $"0000\tCC\t$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0xE4, 0x01, 0x02 }, $"0000\tCPO\t$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0xEC, 0x01, 0x02 }, $"0000\tCPE\t$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0xF4, 0x01, 0x02 }, $"0000\tCP\t$0201{Environment.NewLine}" };
        yield return new object[] { new byte[3] { 0xFC, 0x01, 0x02 }, $"0000\tCM\t$0201{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetRConditionData()
    {
        yield return new object[] { new byte[1] { 0xC0 }, $"0000\tRNZ{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xC8 }, $"0000\tRZ{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xD0 }, $"0000\tRNC{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xD8 }, $"0000\tRC{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xE0 }, $"0000\tRPO{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xE8 }, $"0000\tRPE{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xF0 }, $"0000\tRP{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xF8 }, $"0000\tRM{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetRSTData()
    {
        yield return new object[] { new byte[1] { 0xC7 }, $"0000\tRST\t0{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xCF }, $"0000\tRST\t1{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xD7 }, $"0000\tRST\t2{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xDF }, $"0000\tRST\t3{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xE7 }, $"0000\tRST\t4{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xEF }, $"0000\tRST\t5{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xF7 }, $"0000\tRST\t6{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xFF }, $"0000\tRST\t7{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetPUSHData()
    {
        yield return new object[] { new byte[1] { 0xC5 }, $"0000\tPUSH\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xD5 }, $"0000\tPUSH\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xE5 }, $"0000\tPUSH\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xF5 }, $"0000\tPUSH\tPSW{Environment.NewLine}" };
    }

    public static IEnumerable<object[]> GetPOPData()
    {
        yield return new object[] { new byte[1] { 0xC1 }, $"0000\tPOP\tB{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xD1 }, $"0000\tPOP\tD{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xE1 }, $"0000\tPOP\tH{Environment.NewLine}" };
        yield return new object[] { new byte[1] { 0xF1 }, $"0000\tPOP\tPSW{Environment.NewLine}" };
    }
}
