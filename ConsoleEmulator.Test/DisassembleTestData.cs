namespace ConsoleEmulator.Test;

internal class DisassembleTestData
{
    public static IEnumerable<object[]> GetMoveData()
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
}
