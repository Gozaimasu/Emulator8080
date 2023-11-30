using System.Buffers;

byte[] buffer;
int size;
using (FileStream stream = new(args[0], FileMode.Open))
{
    size = (int)stream.Length;
    buffer = ArrayPool<byte>.Shared.Rent(size);
    stream.Read(buffer, 0, size);
}
int pc = 0;
while (pc < size)
{
    pc += Helper.Disassemble8080Op(buffer, pc);
}

ArrayPool<byte>.Shared.Return(buffer);

internal static class Helper
{
    public static int Disassemble8080Op(ReadOnlySpan<byte> input, int pc)
    {
        byte code = input[pc];
        int opbytes = 1;
        Console.Write("{0:X4}\t", pc);
        switch (code)
        {
            case 0x00: { Console.Write("NOP"); break; }
            case 0x01: { Console.Write("LXI\tB,#${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x02: { Console.Write("STAX\tB"); break; }
            case 0x03: { Console.Write("INX\tB"); break; }
            case 0x04: { Console.Write("INR\tB"); break; }
            case 0x05: { Console.Write("DCR\tB"); break; }
            case 0x06: { Console.Write("MVI\tB,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x07: { Console.Write("RLC"); break; }

            case 0x09: { Console.Write("DAD\tB"); break; }
            case 0x0A: { Console.Write("LDAX\tB"); break; }
            case 0x0B: { Console.Write("DCX\tB"); break; }
            case 0x0C: { Console.Write("INR\tC"); break; }
            case 0x0D: { Console.Write("DCR\tC"); break; }
            case 0x0E: { Console.Write("MVI\tC,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x0F: { Console.Write("RRC"); break; }

            case 0x11: { Console.Write("LXI\tD,#${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x12: { Console.Write("STAX\tD"); break; }
            case 0x13: { Console.Write("INX\tD"); break; }
            case 0x14: { Console.Write("INR\tD"); break; }
            case 0x15: { Console.Write("DCR\tD"); break; }
            case 0x16: { Console.Write("MVI\tD,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x17: { Console.Write("RAL"); break; }

            case 0x19: { Console.Write("DAD\tD"); break; }
            case 0x1A: { Console.Write("LDAX\tD"); break; }
            case 0x1B: { Console.Write("DCX\tD"); break; }
            case 0x1C: { Console.Write("INR\tD"); break; }
            case 0x1D: { Console.Write("DCR\tD"); break; }
            case 0x1E: { Console.Write("MVI\tD,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x1F: { Console.Write("RAR"); break; }

            case 0x21: { Console.Write("LXI\tH,#${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x22: { Console.Write("SHLD\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x23: { Console.Write("INX\tH"); break; }
            case 0x24: { Console.Write("INR\tH"); break; }
            case 0x25: { Console.Write("DCR\tH"); break; }
            case 0x26: { Console.Write("MVI\tH,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x27: { Console.Write("DAA"); break; }

            case 0x29: { Console.Write("DAD\tH"); break; }
            case 0x2A: { Console.Write("LHLD\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x2B: { Console.Write("DCX\tH"); break; }
            case 0x2C: { Console.Write("INR\tL"); break; }
            case 0x2D: { Console.Write("DCR\tL"); break; }
            case 0x2E: { Console.Write("MVI\tL,{0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x2F: { Console.Write("CMA"); break; }

            case 0x31: { Console.Write("LXI\tSP,#${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x32: { Console.Write("STA\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x33: { Console.Write("INX\tSP"); break; }
            case 0x34: { Console.Write("INR\tM"); break; }
            case 0x35: { Console.Write("DCR\tM"); break; }
            case 0x36: { Console.Write("MVI\tM,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x37: { Console.Write("STC"); break; }

            case 0x39: { Console.Write("DAD\tSP"); break; }
            case 0x3A: { Console.Write("LDA\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x3B: { Console.Write("DCX\tSP"); break; }
            case 0x3C: { Console.Write("INR\tA"); break; }
            case 0x3D: { Console.Write("DCR\tA"); break; }
            case 0x3E: { Console.Write("MVI\tA,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x3F: { Console.Write("CMC"); break; }
            case 0x40: { Console.Write("MOV\tB,B"); break; }
            case 0x41: { Console.Write("MOV\tB,C"); break; }
            case 0x42: { Console.Write("MOV\tB,D"); break; }
            case 0x43: { Console.Write("MOV\tB,E"); break; }
            case 0x44: { Console.Write("MOV\tB,H"); break; }
            case 0x45: { Console.Write("MOV\tB,L"); break; }
            case 0x46: { Console.Write("MOV\tB,M"); break; }
            case 0x47: { Console.Write("MOV\tB,A"); break; }
            case 0x48: { Console.Write("MOV\tC,B"); break; }
            case 0x49: { Console.Write("MOV\tC,C"); break; }
            case 0x4A: { Console.Write("MOV\tC,D"); break; }
            case 0x4B: { Console.Write("MOV\tC,E"); break; }
            case 0x4C: { Console.Write("MOV\tC,H"); break; }
            case 0x4D: { Console.Write("MOV\tC,L"); break; }
            case 0x4E: { Console.Write("MOV\tC,M"); break; }
            case 0x4F: { Console.Write("MOV\tC,A"); break; }
            case 0x50: { Console.Write("MOV\tD,B"); break; }
            case 0x51: { Console.Write("MOV\tD,C"); break; }
            case 0x52: { Console.Write("MOV\tD,D"); break; }
            case 0x53: { Console.Write("MOV\tD,E"); break; }
            case 0x54: { Console.Write("MOV\tD,H"); break; }
            case 0x55: { Console.Write("MOV\tD,L"); break; }
            case 0x56: { Console.Write("MOV\tD,M"); break; }
            case 0x57: { Console.Write("MOV\tD,A"); break; }
            case 0x58: { Console.Write("MOV\tE,B"); break; }
            case 0x59: { Console.Write("MOV\tE,C"); break; }
            case 0x5A: { Console.Write("MOV\tE,D"); break; }
            case 0x5B: { Console.Write("MOV\tE,E"); break; }
            case 0x5C: { Console.Write("MOV\tE,H"); break; }
            case 0x5D: { Console.Write("MOV\tE,L"); break; }
            case 0x5E: { Console.Write("MOV\tE,M"); break; }
            case 0x5F: { Console.Write("MOV\tE,A"); break; }
            case 0x60: { Console.Write("MOV\tH,B"); break; }
            case 0x61: { Console.Write("MOV\tH,C"); break; }
            case 0x62: { Console.Write("MOV\tH,D"); break; }
            case 0x63: { Console.Write("MOV\tH,E"); break; }
            case 0x64: { Console.Write("MOV\tH,H"); break; }
            case 0x65: { Console.Write("MOV\tH,L"); break; }
            case 0x66: { Console.Write("MOV\tH,M"); break; }
            case 0x67: { Console.Write("MOV\tH,A"); break; }
            case 0x68: { Console.Write("MOV\tL,B"); break; }
            case 0x69: { Console.Write("MOV\tL,C"); break; }
            case 0x6A: { Console.Write("MOV\tL,D"); break; }
            case 0x6B: { Console.Write("MOV\tL,E"); break; }
            case 0x6C: { Console.Write("MOV\tL,H"); break; }
            case 0x6D: { Console.Write("MOV\tL,L"); break; }
            case 0x6E: { Console.Write("MOV\tL,M"); break; }
            case 0x6F: { Console.Write("MOV\tL,A"); break; }
            case 0x70: { Console.Write("MOV\tM,B"); break; }
            case 0x71: { Console.Write("MOV\tM,C"); break; }
            case 0x72: { Console.Write("MOV\tM,D"); break; }
            case 0x73: { Console.Write("MOV\tM,E"); break; }
            case 0x74: { Console.Write("MOV\tM,H"); break; }
            case 0x75: { Console.Write("MOV\tM,L"); break; }
            case 0x76: { Console.Write("HLT"); break; }
            case 0x77: { Console.Write("MOV\tM,A"); break; }
            case 0x78: { Console.Write("MOV\tA,B"); break; }
            case 0x79: { Console.Write("MOV\tA,C"); break; }
            case 0x7A: { Console.Write("MOV\tA,D"); break; }
            case 0x7B: { Console.Write("MOV\tA,E"); break; }
            case 0x7C: { Console.Write("MOV\tA,H"); break; }
            case 0x7D: { Console.Write("MOV\tA,L"); break; }
            case 0x7E: { Console.Write("MOV\tA,M"); break; }
            case 0x7F: { Console.Write("MOV\tA,A"); break; }
            case 0x80: { Console.Write("ADD\tB"); break; }
            case 0x81: { Console.Write("ADD\tC"); break; }
            case 0x82: { Console.Write("ADD\tD"); break; }
            case 0x83: { Console.Write("ADD\tE"); break; }
            case 0x84: { Console.Write("ADD\tH"); break; }
            case 0x85: { Console.Write("ADD\tL"); break; }
            case 0x86: { Console.Write("ADD\tM"); break; }
            case 0x87: { Console.Write("ADD\tA"); break; }
            case 0x88: { Console.Write("ADC\tB"); break; }
            case 0x89: { Console.Write("ADC\tC"); break; }
            case 0x8A: { Console.Write("ADC\tD"); break; }
            case 0x8B: { Console.Write("ADC\tE"); break; }
            case 0x8C: { Console.Write("ADC\tH"); break; }
            case 0x8D: { Console.Write("ADC\tL"); break; }
            case 0x8E: { Console.Write("ADC\tM"); break; }
            case 0x8F: { Console.Write("ADC\tA"); break; }
            case 0x90: { Console.Write("SUB\tB"); break; }
            case 0x91: { Console.Write("SUB\tC"); break; }
            case 0x92: { Console.Write("SUB\tD"); break; }
            case 0x93: { Console.Write("SUB\tE"); break; }
            case 0x94: { Console.Write("SUB\tH"); break; }
            case 0x95: { Console.Write("SUB\tL"); break; }
            case 0x96: { Console.Write("SUB\tM"); break; }
            case 0x97: { Console.Write("SUB\tA"); break; }
            case 0x98: { Console.Write("SBB\tB"); break; }
            case 0x99: { Console.Write("SBB\tC"); break; }
            case 0x9A: { Console.Write("SBB\tD"); break; }
            case 0x9B: { Console.Write("SBB\tE"); break; }
            case 0x9C: { Console.Write("SBB\tH"); break; }
            case 0x9D: { Console.Write("SBB\tL"); break; }
            case 0x9E: { Console.Write("SBB\tM"); break; }
            case 0x9F: { Console.Write("SBB\tA"); break; }
            case 0xA0: { Console.Write("ANA\tB"); break; }
            case 0xA1: { Console.Write("ANA\tC"); break; }
            case 0xA2: { Console.Write("ANA\tD"); break; }
            case 0xA3: { Console.Write("ANA\tE"); break; }
            case 0xA4: { Console.Write("ANA\tH"); break; }
            case 0xA5: { Console.Write("ANA\tL"); break; }
            case 0xA6: { Console.Write("ANA\tM"); break; }
            case 0xA7: { Console.Write("ANA\tA"); break; }
            case 0xA8: { Console.Write("XRA\tB"); break; }
            case 0xA9: { Console.Write("XRA\tC"); break; }
            case 0xAA: { Console.Write("XRA\tD"); break; }
            case 0xAB: { Console.Write("XRA\tE"); break; }
            case 0xAC: { Console.Write("XRA\tH"); break; }
            case 0xAD: { Console.Write("XRA\tL"); break; }
            case 0xAE: { Console.Write("XRA\tM"); break; }
            case 0xAF: { Console.Write("XRA\tA"); break; }
            case 0xB0: { Console.Write("ORA\tB"); break; }
            case 0xB1: { Console.Write("ORA\tC"); break; }
            case 0xB2: { Console.Write("ORA\tD"); break; }
            case 0xB3: { Console.Write("ORA\tE"); break; }
            case 0xB4: { Console.Write("ORA\tH"); break; }
            case 0xB5: { Console.Write("ORA\tL"); break; }
            case 0xB6: { Console.Write("ORA\tM"); break; }
            case 0xB7: { Console.Write("ORA\tA"); break; }
            case 0xB8: { Console.Write("CMP\tB"); break; }
            case 0xB9: { Console.Write("CMP\tC"); break; }
            case 0xBA: { Console.Write("CMP\tD"); break; }
            case 0xBB: { Console.Write("CMP\tE"); break; }
            case 0xBC: { Console.Write("CMP\tH"); break; }
            case 0xBD: { Console.Write("CMP\tL"); break; }
            case 0xBE: { Console.Write("CMP\tM"); break; }
            case 0xBF: { Console.Write("CMP\tA"); break; }
            case 0xC0: { Console.Write("RNZ"); break; }
            case 0xC1: { Console.Write("POP\tB"); break; }
            case 0xC2: { Console.Write("JNZ\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xC3: { Console.Write("JMP\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xC4: { Console.Write("CNZ\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xC5: { Console.Write("PUSH\tB"); break; }
            case 0xC6: { Console.Write("ADI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xC7: { Console.Write("RST\t0"); break; }
            case 0xC8: { Console.Write("RZ"); break; }
            case 0xC9: { Console.Write("RET"); break; }
            case 0xCA: { Console.Write("JZ\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }

            case 0xCC: { Console.Write("CZ\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xCD: { Console.Write("CALL\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xCE: { Console.Write("ACI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xCF: { Console.Write("RST\t1"); break; }
            case 0xD0: { Console.Write("RNC"); break; }
            case 0xD1: { Console.Write("POP\tD"); break; }
            case 0xD2: { Console.Write("JNC\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xD3: { Console.Write("OUT\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xD4: { Console.Write("CNC\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xD5: { Console.Write("PUSH\tD"); break; }
            case 0xD6: { Console.Write("SUI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xD7: { Console.Write("RST\t2"); break; }
            case 0xD8: { Console.Write("RC"); break; }

            case 0xDA: { Console.Write("JC\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xDB: { Console.Write("IN\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xDC: { Console.Write("CC\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }

            case 0xDE: { Console.Write("SBI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xDF: { Console.Write("RST\t3"); break; }
            case 0xE0: { Console.Write("RPO"); break; }
            case 0xE1: { Console.Write("POP\tH"); break; }
            case 0xE2: { Console.Write("JPO\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xE3: { Console.Write("XTHL"); break; }
            case 0xE4: { Console.Write("CPO\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xE5: { Console.Write("PUSH\tH"); break; }
            case 0xE6: { Console.Write("ANI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xE7: { Console.Write("RST\t4"); break; }
            case 0xE8: { Console.Write("RPE"); break; }
            case 0xE9: { Console.Write("PCHL"); break; }
            case 0xEA: { Console.Write("JPE\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xEB: { Console.Write("XCHG"); break; }
            case 0xEC: { Console.Write("CPE\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }

            case 0xEE: { Console.Write("XRI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xEF: { Console.Write("RST\t5"); break; }
            case 0xF0: { Console.Write("RP"); break; }
            case 0xF1: { Console.Write("POP\tPSW"); break; }
            case 0xF2: { Console.Write("JP\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xF3: { Console.Write("DI"); break; }
            case 0xF4: { Console.Write("CP\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xF5: { Console.Write("PUSH\tPSW"); break; }
            case 0xF6: { Console.Write("ORI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xF7: { Console.Write("RST\t6"); break; }
            case 0xF8: { Console.Write("RM"); break; }
            case 0xF9: { Console.Write("SPHL"); break; }
            case 0xFA: { Console.Write("JM\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xFB: { Console.Write("EI"); break; }
            case 0xFC: { Console.Write("CM\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }

            case 0xFE: { Console.Write("CPI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xFF: { Console.Write("RST\t7"); break; }
        }
        Console.WriteLine();
        return opbytes;
    }
}