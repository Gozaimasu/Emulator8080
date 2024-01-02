using System.Runtime.CompilerServices;

namespace ConsoleEmulator;

public class CPU8080
{
    public delegate void OutputCallback(byte port, byte data);

    public State8080 State { get; private set; }
    public static IDebugOutput DebugOutput { get; set; } = null!;
    public OutputCallback? Output { get; set; }
    public int Cycles { get; private set; }
    public int States { get; private set; }
    public int Steps { get; private set; }

    public ISystemCall? SystemCall { get; set; }

    public byte[] Memory { get; private set; } = [];

    public CPU8080(ISystemCall? systemCall = null)
    {
        State = new State8080();

        SystemCall = systemCall;
    }

    static CPU8080()
    {
        DebugOutput = new ConsoleDebugOutput();
    }

    public void Init(byte[] memory, ushort start)
    {
        Memory = memory;
        State.PC = start;
    }

    public int Step()
    {
        if (SystemCall != null && SystemCall.TryStep(this, out int done))
        {
            Steps++;
            return done;
        }
        byte opcode = Memory.AsSpan()[State.PC];

        // Pour debugger
        //DebugOutput.WriteLine(string.Format("A={0:X2}, BC={1:X2}{2:X2}, DE={3:X2}{4:X2}, HL={5:X2}{6:X2}, PC={8:X4}, SP={7:X4}", State.A, State.B, State.C, State.D, State.E, State.H, State.L, State.SP, State.PC));
        //DebugOutput.WriteLine(string.Format("Z={0}, S={1}, P={2}, CY={3}, AC={4}, PAD={5}, Cycles={6}, States={7}", State.CC.Z, State.CC.S, State.CC.P, State.CC.CY, State.CC.AC, State.CC.PAD, Cycles, States));
        //Disassemble8080Op(State.PC);

        State.PC++;

        // INR
        if ((opcode & 0xC7) == 0x04)
        {
            // Récupération de l'offset
            int offset = (opcode >> 3) & 0x07;
            int res;
            if (offset == 6)
            {
                // INR M
                // Récupération de l'adresse
                ushort addr = (ushort)((State.H << 8) + State.L);
                // Récupération et incrément de la valeur
                res = Memory.AsSpan()[addr] + 1;
                // Modification de la mémoire
                Memory.AsSpan()[addr] = (byte)(0xFF & res);
                Cycles += 3;
                States += 10;
            }
            else
            {
                // Récupération du registre correspondant et on incrémente la valeur
                res = State.GetRegister(offset) + 1;
                // Modification du registre
                State.SetRegister(offset, (byte)(0xFF & res));
                Cycles++;
                States += 5;
            }
            ConditionCodes cc = State.CC;
            // Modification de CC
            cc.Z = res == 0 ? (byte)1 : (byte)0;
            cc.S = (res & 0x80) != 0 ? (byte)1 : (byte)0;
            cc.P = Parity(res, 8);
            // Affectation de la nouvelle valeur de CC
            State.CC = cc;
        }
        // DCR
        else if ((opcode & 0xC7) == 0x05)
        {
            // Récupération de l'offset
            int offset = (opcode >> 3) & 0x07;
            int res;
            if (offset == 6)
            {
                // DCR M
                // Récupération de l'adresse
                ushort addr = (ushort)((State.H << 8) + State.L);
                // Récupération et incrément de la valeur
                res = Memory.AsSpan()[addr] - 1;
                // Modification de la mémoire
                Memory.AsSpan()[addr] = (byte)(0xFF & res);
                Cycles += 3;
                States += 10;
            }
            else
            {
                // Récupération du registre correspondant et on décrémente la valeur
                res = State.GetRegister(offset) - 1;
                // Modification du registre
                State.SetRegister(offset, (byte)(0xFF & res));
                Cycles++;
                States += 5;
            }
            ConditionCodes cc = State.CC;
            // Modification de CC
            cc.Z = res == 0 ? (byte)1 : (byte)0;
            cc.S = (res & 0x80) != 0 ? (byte)1 : (byte)0;
            cc.P = Parity(res, 8);
            // Affectation de la nouvelle valeur de CC
            State.CC = cc;
        }
        // MVI
        else if ((opcode & 0xC7) == 0x06)
        {
            // Récupération de l'offset
            int offset = (opcode >> 3) & 0x07;

            if (offset == 6)
            {
                Memory.AsSpan()[(State.H << 8) + State.L] = Memory.AsSpan()[State.PC];
                Cycles += 3;
                States += 10;
            }
            else
            {
                State.SetRegister(offset, Memory.AsSpan()[State.PC]);
                Cycles += 2;
                States += 7;
            }

            State.PC++;
        }
        // RCondition
        else if ((opcode & 0xC7) == 0xC0)
        {
            // Récupération de la condition
            int condition = (opcode >> 3) & 0x07;

            switch (condition)
            {
                case 0:
                    // RNZ adr
                    if (State.CC.Z != 1)
                    {
                        Return();
                        Cycles += 2;
                        States += 6;
                    }
                    break;
                case 1:
                    // RZ adr
                    if (State.CC.Z == 1)
                    {
                        Return();
                        Cycles += 2;
                        States += 6;
                    }
                    break;
                case 2:
                    // RNC adr
                    if (State.CC.CY != 1)
                    {
                        Return();
                        Cycles += 2;
                        States += 6;
                    }
                    break;
                case 3:
                    // RC adr
                    if (State.CC.CY == 1)
                    {
                        Return();
                        Cycles += 2;
                        States += 6;
                    }
                    break;
                case 4:
                    // RPO adr
                    if (State.CC.P != 1)
                    {
                        Return();
                        Cycles += 2;
                        States += 6;
                    }
                    break;
                case 5:
                    // RPE adr
                    if (State.CC.P == 1)
                    {
                        Return();
                        Cycles += 2;
                        States += 6;
                    }
                    break;
                case 6:
                    // RP adr
                    if (State.CC.S != 1)
                    {
                        Return();
                        Cycles += 2;
                        States += 6;
                    }
                    break;
                case 7:
                    // RM adr
                    if (State.CC.S == 1)
                    {
                        Return();
                        Cycles += 2;
                        States += 6;
                    }
                    break;
            }
            Cycles += 1;
            States += 5;
        }
        // JCondition
        else if ((opcode & 0xC7) == 0xC2)
        {
            // Récupération de la condition
            int condition = (opcode >> 3) & 0x07;

            switch (condition)
            {
                case 0:
                    // JNZ adr
                    if (State.CC.Z != 1)
                    {
                        State.PC = Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.PC]);
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
                case 1:
                    // JZ adr
                    if (State.CC.Z == 1)
                    {
                        State.PC = Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.PC]);
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
                case 2:
                    // JNC adr
                    if (State.CC.CY != 1)
                    {
                        State.PC = Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.PC]);
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
                case 3:
                    // JC adr
                    if (State.CC.CY == 1)
                    {
                        State.PC = Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.PC]);
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
                case 4:
                    // JPO adr
                    if (State.CC.P != 1)
                    {
                        State.PC = Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.PC]);
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
                case 5:
                    // JPE adr
                    if (State.CC.P == 1)
                    {
                        State.PC = Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.PC]);
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
                case 6:
                    // JP adr
                    if (State.CC.S != 1)
                    {
                        State.PC = Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.PC]);
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
                case 7:
                    // JM adr
                    if (State.CC.S == 1)
                    {
                        State.PC = Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.PC]);
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
            }
            Cycles += 3;
            States += 10;
        }
        // CCondition
        else if ((opcode & 0xC7) == 0xC4)
        {
            // Récupération de la condition
            int condition = (opcode >> 3) & 0x07;

            switch (condition)
            {
                case 0:
                    // CNZ adr
                    if (State.CC.Z != 1)
                    {
                        Call();
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
                case 1:
                    // CZ adr
                    if (State.CC.Z == 1)
                    {
                        Call();
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
                case 2:
                    // CNC adr
                    if (State.CC.CY != 1)
                    {
                        Call();
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
                case 3:
                    // CC adr
                    if (State.CC.CY == 1)
                    {
                        Call();
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
                case 4:
                    // CPO adr
                    if (State.CC.P != 1)
                    {
                        Call();
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
                case 5:
                    // CPE adr
                    if (State.CC.P == 1)
                    {
                        Call();
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
                case 6:
                    // CP adr
                    if (State.CC.S != 1)
                    {
                        Call();
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
                case 7:
                    // CM adr
                    if (State.CC.S == 1)
                    {
                        Call();
                    }
                    else
                    {
                        State.PC += 2;
                    }
                    break;
            }
            Cycles += 5;
            States += 17;
        }
        // LXI
        else if ((opcode & 0xCF) == 0x01)
        {
            int offset = (opcode >> 4) & 0x3;
            switch (offset)
            {
                case 0: { State.C = Memory.AsSpan()[State.PC]; State.B = Memory.AsSpan()[State.PC + 1]; break; } // LXI B,D16
                case 1: { State.E = Memory.AsSpan()[State.PC]; State.D = Memory.AsSpan()[State.PC + 1]; break; } // LXI D,D16
                case 2: { State.L = Memory.AsSpan()[State.PC]; State.H = Memory.AsSpan()[State.PC + 1]; break; } // LXI H,D16
                case 3: { State.SP = Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.PC]); break; } // LXI SP,D16
            }
            State.PC += 2;
            Cycles += 3;
            States += 10;
        }
        // STAX
        else if ((opcode & 0xCF) == 0x02)
        {
            int offset = (opcode >> 4) & 0x3;
            switch (offset)
            {
                case 0:
                    {
                        // STAX B
                        ushort addr = (ushort)((State.B << 8) + State.C);
                        Memory.AsSpan()[addr] = State.A;
                        break;
                    }
                case 1:
                    {
                        // STAX D
                        ushort addr = (ushort)((State.D << 8) + State.E);
                        Memory.AsSpan()[addr] = State.A;
                        break;
                    }
                case 2:
                    {
                        // SHLD
                        ushort addr = Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.PC]);
                        Memory.AsSpan()[addr] = State.L;
                        Memory.AsSpan()[addr + 1] = State.H;
                        State.PC += 2;
                        Cycles += 3;
                        States += 9;
                        break;
                    }
                case 3:
                    {
                        // STA addr
                        int addr = Memory.AsSpan()[State.PC++] + (Memory.AsSpan()[State.PC++] << 8);
                        Memory.AsSpan()[addr] = State.A;
                        Cycles += 2;
                        States += 6;
                        break;
                    }
            }
            Cycles += 2;
            States += 7;
        }
        // INX
        else if ((opcode & 0xCF) == 0x03)
        {
            int offset = (opcode >> 4) & 0x3;
            switch (offset)
            {
                case 0: { if (State.C == 0xFF) { State.C = 0; State.B++; } else { State.C++; } break; } // INX B
                case 1: { if (State.E == 0xFF) { State.E = 0; State.D++; } else { State.E++; } break; } // INX D
                case 2: { if (State.L == 0xFF) { State.L = 0; State.H++; } else { State.L++; } break; } // INX H
                case 3: { State.SP++; break; } // INX SP
            }
            Cycles++;
            States += 5;
        }
        // DAD
        else if ((opcode & 0xCF) == 0x09)
        {
            int offset = (opcode >> 4) & 0x3;
            int result = (State.H << 8) + State.L;
            ConditionCodes cc = State.CC;
            cc.CY = 0;
            switch (offset)
            {
                case 0: { result += (State.B << 8) + State.C; break; }// DAD B
                case 1: { result += (State.D << 8) + State.E; break; }// DAD D
                case 2: { result <<= 1; break; }// DAD J
                case 3: { result += State.SP; break; }// DAD SP
            }
            State.L = (byte)(result & 0xFF);
            State.H = (byte)((result >> 8) & 0xFF);
            if ((result & 0xFFFF0000) != 0)
            {
                cc.CY = 1;
            }
            State.CC = cc;
            Cycles += 3;
            States += 10;
        }
        // LDAX
        else if ((opcode & 0xCF) == 0x0A)
        {
            int offset = (opcode >> 4) & 0x3;
            switch (offset)
            {
                case 0: { State.A = Memory.AsSpan()[(State.B << 8) + State.C]; break; } // LDAX B
                case 1: { State.A = Memory.AsSpan()[(State.D << 8) + State.E]; break; } // LDAX D
                case 2:
                    {
                        // LHLD
                        ushort addr = Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.PC]);
                        State.L = Memory.AsSpan()[addr];
                        State.H = Memory.AsSpan()[addr + 1];
                        State.PC += 2;
                        Cycles += 3;
                        States += 9;
                        break;
                    }
                case 3:
                    {
                        // LDA addr
                        int addr = Memory.AsSpan()[State.PC++] + (Memory.AsSpan()[State.PC++] << 8);
                        State.A = Memory.AsSpan()[addr];
                        Cycles += 2;
                        States += 6;
                        break;
                    }
            }
            Cycles += 2;
            States += 7;
        }
        // DCX
        else if ((opcode & 0xCF) == 0x0B)
        {
            int offset = (opcode >> 4) & 0x3;
            switch (offset)
            {
                case 0: { if (State.C == 0x00) { State.C = 0xFF; State.B--; } else { State.C--; } break; } // DCX B
                case 1: { if (State.E == 0x00) { State.E = 0xFF; State.D--; } else { State.E--; } break; } // DCX D
                case 2: { if (State.L == 0x00) { State.L = 0xFF; State.H--; } else { State.L--; } break; } // DCX H
                case 3: { State.SP--; break; } // INX SP
            }
            Cycles++;
            States += 5;
        }
        // POP
        else if ((opcode & 0xCF) == 0xC1)
        {
            int offset = (opcode >> 4) & 0x3;
            switch (offset)
            {
                case 0: { State.C = Memory.AsSpan()[State.SP]; State.B = Memory.AsSpan()[State.SP + 1]; break; } // POP B
                case 1: { State.E = Memory.AsSpan()[State.SP]; State.D = Memory.AsSpan()[State.SP + 1]; break; } // POP D
                case 2: { State.L = Memory.AsSpan()[State.SP]; State.H = Memory.AsSpan()[State.SP + 1]; break; } // POP H
                default:
                    {
                        State.A = Memory.AsSpan()[State.SP + 1];
                        var cc = State.CC;
                        byte psw = Memory.AsSpan()[State.SP];
                        cc.CY = (byte)(psw & 0x01);
                        cc.P = (byte)((psw >> 2) & 0x01);
                        cc.AC = (byte)((psw >> 4) & 0x01);
                        cc.Z = (byte)((psw >> 6) & 0x01);
                        cc.S = (byte)((psw >> 7) & 0x01);
                        State.CC = cc;
                        break;
                    }
            }
            State.SP += 2;
            Cycles += 3;
            States += 10;
        }
        // PUSH
        else if ((opcode & 0xCF) == 0xC5)
        {
            int offset = (opcode >> 4) & 0x3;
            switch (offset)
            {
                case 0: { Memory.AsSpan()[State.SP - 2] = State.C; Memory.AsSpan()[State.SP - 1] = State.B; break; } // PUSH B
                case 1: { Memory.AsSpan()[State.SP - 2] = State.E; Memory.AsSpan()[State.SP - 1] = State.D; break; } // PUSH D
                case 2: { Memory.AsSpan()[State.SP - 2] = State.L; Memory.AsSpan()[State.SP - 1] = State.H; break; } // PUSH H
                default:
                    {
                        Memory.AsSpan()[State.SP - 2] = (byte)((State.CC.S << 7) + (State.CC.Z << 6) + (State.CC.AC << 4) + (State.CC.P << 2) + 2 + State.CC.CY);
                        Memory.AsSpan()[State.SP - 1] = State.A;
                        break;
                    }
            }
            State.SP -= 2;
            Cycles += 3;
            States += 11;
        }
        // MOV
        else if ((opcode & 0xC0) == 0x40)
        {
            // Récupération des offsets
            int offsetDestination = (opcode >> 3) & 0x07;
            int offsetSource = opcode & 0x07;

            if (offsetSource == 6 && offsetDestination == 6)
            {
                // HLT
                return UnimplementedInstruction();
            }
            else if (offsetSource == 6)
            {
                // MOV r, M
                State.SetRegister(offsetDestination, Memory.AsSpan()[(State.H << 8) + State.L]);
                Cycles += 2;
                States += 7;
            }
            else if (offsetDestination == 6)
            {
                // MOV M, r
                Memory.AsSpan()[(State.H << 8) + State.L] = State.GetRegister(offsetSource);
                Cycles += 2;
                States += 7;
            }
            else
            {
                State.SetRegister(offsetDestination, State.GetRegister(offsetSource));
                Cycles++;
                States += 5;
            }
        }
        // ADD
        else if ((opcode & 0xF8) == 0x80)
        {
            // Récupération de l'offset
            int offset = opcode & 0x07;
            byte value;

            if (offset == 0x06)
            {
                // ADD M
                value = Memory.AsSpan()[(State.H << 8) + State.L];
                Cycles += 2;
                States += 7;
            }
            else
            {
                // ADD r
                value = State.GetRegister(offset);
                Cycles++;
                States += 4;
            }
            int result = State.A + value;
            State.A = (byte)(result & 0xFF);
            ConditionCodes cc = State.CC;
            cc.Z = State.A == 0 ? (byte)1 : (byte)0;
            cc.S = (result & 0x80) != 0 ? (byte)1 : (byte)0;
            cc.P = Parity(result, 8);
            cc.CY = result > 0xFF ? (byte)1 : (byte)0;
            cc.AC = 0;
            State.CC = cc;
        }
        // ADC
        else if ((opcode & 0xF8) == 0x88)
        {
            // Récupération de l'offset
            int offset = opcode & 0x07;
            byte value;

            if (offset == 0x06)
            {
                // ADC M
                value = Memory.AsSpan()[(State.H << 8) + State.L];
                Cycles += 2;
                States += 7;
            }
            else
            {
                // ADC r
                value = State.GetRegister(offset);
                Cycles++;
                States += 4;
            }
            int result = State.A + value + State.CC.CY;
            State.A = (byte)(result & 0xFF);
            ConditionCodes cc = State.CC;
            cc.Z = State.A == 0 ? (byte)1 : (byte)0;
            cc.S = (result & 0x80) != 0 ? (byte)1 : (byte)0;
            cc.P = Parity(result, 8);
            cc.CY = result > 0xFF ? (byte)1 : (byte)0;
            cc.AC = 0;
            State.CC = cc;
        }
        // SUB
        else if ((opcode & 0xF8) == 0x90)
        {
            // Récupération de l'offset
            int offset = opcode & 0x07;
            byte value;

            if (offset == 0x06)
            {
                // SUB M
                value = Memory.AsSpan()[(State.H << 8) + State.L];
                Cycles += 2;
                States += 7;
            }
            else
            {
                // SUB r
                value = State.GetRegister(offset);
                Cycles++;
                States += 4;
            }
            int result = State.A - value;
            ConditionCodes cc = State.CC;
            if (State.A < value)
            {
                cc.CY = 0x01;
            }
            else
            {
                cc.CY = 0x00;
            }
            State.A = (byte)(result & 0xFF);
            cc.Z = State.A == 0 ? (byte)1 : (byte)0;
            cc.S = (result & 0x80) != 0 ? (byte)1 : (byte)0;
            cc.P = Parity(result, 8);
            cc.AC = 0;
            State.CC = cc;
        }
        // SBB
        else if ((opcode & 0xF8) == 0x98)
        {
            // Récupération de l'offset
            int offset = opcode & 0x07;
            byte value;

            if (offset == 0x06)
            {
                // SBB M
                value = Memory.AsSpan()[(State.H << 8) + State.L];
                Cycles += 2;
                States += 7;
            }
            else
            {
                // SBB r
                value = State.GetRegister(offset);
                Cycles++;
                States += 4;
            }
            int result = State.A - value - State.CC.CY;
            ConditionCodes cc = State.CC;
            if (State.A < (value + cc.CY))
            {
                cc.CY = 0x01;
            }
            else
            {
                cc.CY = 0x00;
            }
            State.A = (byte)(result & 0xFF);
            cc.Z = State.A == 0 ? (byte)1 : (byte)0;
            cc.S = (result & 0x80) != 0 ? (byte)1 : (byte)0;
            cc.P = Parity(result, 8);
            cc.AC = 0;
            State.CC = cc;
        }
        // ANA
        else if ((opcode & 0xF8) == 0xA0)
        {
            // Récupération de l'offset
            int offset = opcode & 0x07;

            if (offset == 0x06)
            {
                // XRA M
                State.A &= Memory.AsSpan()[(State.H << 8) + State.L];
                Cycles += 2;
                States += 7;
            }
            else
            {
                // XRA r
                State.A &= State.GetRegister(offset);
                Cycles++;
                States += 4;
            }
            ConditionCodes cc = State.CC;
            cc.Z = (byte)(State.A == 0 ? 1 : 0);
            cc.S = (byte)((State.A >> 7) & 0x01);
            cc.P = Parity(State.A, 8);
            cc.CY = 0;
            cc.AC = 0;
            State.CC = cc;
        }
        // XRA
        else if ((opcode & 0xF8) == 0xA8)
        {
            // Récupération de l'offset
            int offset = opcode & 0x07;

            if (offset == 0x06)
            {
                // XRA M
                State.A ^= Memory.AsSpan()[(State.H << 8) + State.L];
                Cycles += 2;
                States += 7;
            }
            else
            {
                // XRA r
                State.A ^= State.GetRegister(offset);
                Cycles++;
                States += 4;
            }
            ConditionCodes cc = State.CC;
            cc.Z = (byte)(State.A == 0 ? 1 : 0);
            cc.S = (byte)((State.A >> 7) & 0x01);
            cc.P = Parity(State.A, 8);
            cc.CY = 0;
            cc.AC = 0;
            State.CC = cc;
        }
        // ORA
        else if ((opcode & 0xF8) == 0xB0)
        {
            // Récupération de l'offset
            int offset = opcode & 0x07;

            if (offset == 0x06)
            {
                // ORA M
                State.A |= Memory.AsSpan()[(State.H << 8) + State.L];
                Cycles += 2;
                States += 7;
            }
            else
            {
                // ORA r
                State.A |= State.GetRegister(offset);
                Cycles++;
                States += 4;
            }
            ConditionCodes cc = State.CC;
            cc.Z = (byte)(State.A == 0 ? 1 : 0);
            cc.S = (byte)((State.A >> 7) & 0x01);
            cc.P = Parity(State.A, 8);
            cc.CY = 0;
            cc.AC = 0;
            State.CC = cc;
        }
        // CMP
        else if ((opcode & 0xF8) == 0xB8)
        {
            // Récupération de l'offset
            int offset = opcode & 0x07;
            byte value;
            if (offset == 0x06)
            {
                // CMP M
                value = Memory.AsSpan()[(State.H << 8) + State.L];
                Cycles += 2;
                States += 7;
            }
            else
            {
                // CMP r
                value = State.GetRegister(offset);
                Cycles++;
                States += 4;
            }
            int result = State.A - value;
            ConditionCodes cc = State.CC;
            cc.Z = result == 0 ? (byte)1 : (byte)0;
            cc.S = (result & 0x80) != 0 ? (byte)1 : (byte)0;
            cc.P = Parity(result, 8);
            cc.CY = State.A < value ? (byte)1 : (byte)0;
            cc.AC = 0;
            State.CC = cc;
        }
        else
        {
            switch (opcode)
            {
                // NOP
                case 0x00: { Cycles++; States += 4; break; }
                // RRC
                case 0x0F:
                    {
                        var cc = State.CC;
                        cc.CY = (byte)(State.A & 0x01);
                        State.CC = cc;
                        State.A = (byte)((State.A >> 1) | (State.A << 7));
                        Cycles++;
                        States += 4;
                        break;
                    }
                // JMP
                case 0xC3:
                    {
                        State.PC = Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.PC]);
                        Cycles += 3;
                        States += 10;
                        break;
                    }
                // ADI data
                case 0xC6:
                    {
                        byte value = Memory.AsSpan()[State.PC++];
                        int result = State.A + value;
                        State.A = (byte)(result & 0xFF);
                        ConditionCodes cc = State.CC;
                        // Modification de CC
                        cc.Z = State.A == 0 ? (byte)1 : (byte)0;
                        cc.S = (result & 0x80) != 0 ? (byte)1 : (byte)0;
                        cc.P = Parity(result, 8);
                        cc.CY = result > 0xFF ? (byte)1 : (byte)0;
                        // Affectation de la nouvelle valeur de CC
                        State.CC = cc;
                        Cycles += 2;
                        States += 7;
                        break;
                    }
                // RET
                case 0xC9:
                    {
                        Return();
                        Cycles += 3;
                        States += 10;
                        break;
                    }
                // CALL
                case 0xCD:
                    {
                        Call();
                        Cycles += 5;
                        States += 17;
                        break;
                    };
                // ACI
                case 0xCE:
                    {
                        byte value = Memory.AsSpan()[State.PC++];
                        int result = State.A + value + State.CC.CY;
                        State.A = (byte)(result & 0xFF);
                        ConditionCodes cc = State.CC;
                        // Modification de CC
                        cc.Z = State.A == 0 ? (byte)1 : (byte)0;
                        cc.S = (result & 0x80) != 0 ? (byte)1 : (byte)0;
                        cc.P = Parity(result, 8);
                        cc.CY = result > 0xFF ? (byte)1 : (byte)0;
                        // Affectation de la nouvelle valeur de CC
                        State.CC = cc;
                        Cycles += 2;
                        States += 7;
                        break;
                    }
                // OUT port
                case 0xD3:
                    {
                        byte port = Memory.AsSpan()[State.PC++];
                        Output?.Invoke(port, State.A);
                        Cycles += 3;
                        States += 10;
                        break;
                    }
                // SUI
                case 0xD6:
                    {
                        byte value = Memory.AsSpan()[State.PC++];
                        ConditionCodes cc = State.CC;
                        int result = State.A - value;
                        if (State.A < (value + State.CC.CY))
                        {
                            cc.CY = 0x01;
                        }
                        else
                        {
                            cc.CY = 0x00;
                        }
                        State.A = (byte)(result & 0xFF);
                        // Modification de CC
                        cc.Z = State.A == 0 ? (byte)1 : (byte)0;
                        cc.S = (result & 0x80) != 0 ? (byte)1 : (byte)0;
                        cc.P = Parity(result, 8);
                        // Affectation de la nouvelle valeur de CC
                        State.CC = cc;
                        Cycles += 2;
                        States += 7;
                        break;
                    }
                // SBI
                case 0xDE:
                    {
                        byte value = Memory.AsSpan()[State.PC++];
                        ConditionCodes cc = State.CC;
                        int result = State.A - value - State.CC.CY;
                        if (State.A < (value + State.CC.CY))
                        {
                            cc.CY = 0x01;
                        }
                        else
                        {
                            cc.CY = 0x00;
                        }
                        State.A = (byte)(result & 0xFF);
                        // Modification de CC
                        cc.Z = State.A == 0 ? (byte)1 : (byte)0;
                        cc.S = (result & 0x80) != 0 ? (byte)1 : (byte)0;
                        cc.P = Parity(result, 8);
                        // Affectation de la nouvelle valeur de CC
                        State.CC = cc;
                        Cycles += 2;
                        States += 7;
                        break;
                    }
                // XTHL
                case 0xE3:
                    {
                        (State.L, Memory.AsSpan()[State.SP]) = (Memory.AsSpan()[State.SP], State.L);
                        (State.H, Memory.AsSpan()[State.SP + 1]) = (Memory.AsSpan()[State.SP + 1], State.H);
                        Cycles += 5;
                        States += 18;
                        break;
                    }
                // ANI data
                case 0xE6:
                    {
                        State.A &= Memory.AsSpan()[State.PC++];
                        ConditionCodes cc = State.CC;
                        // Modification de CC
                        cc.Z = State.A == 0 ? (byte)1 : (byte)0;
                        cc.S = (State.A & 0x80) != 0 ? (byte)1 : (byte)0;
                        cc.P = Parity(State.A, 8);
                        cc.CY = 0x00;
                        cc.AC = 0x00;
                        // Affectation de la nouvelle valeur de CC
                        State.CC = cc;
                        Cycles += 2;
                        States += 7;
                        break;
                    }
                // XCHG
                case 0xEB:
                    {
                        (State.D, State.H) = (State.H, State.D);
                        (State.E, State.L) = (State.L, State.E);
                        Cycles++;
                        States += 4;
                        break;
                    }
                // XRI
                case 0xEE:
                    {
                        State.A ^= Memory.AsSpan()[State.PC++];
                        ConditionCodes cc = State.CC;
                        // Modification de CC
                        cc.Z = State.A == 0 ? (byte)1 : (byte)0;
                        cc.S = (State.A & 0x80) != 0 ? (byte)1 : (byte)0;
                        cc.P = Parity(State.A, 8);
                        cc.CY = 0x00;
                        cc.AC = 0x00;
                        // Affectation de la nouvelle valeur de CC
                        State.CC = cc;
                        Cycles += 2;
                        States += 7;
                        break;
                    }
                // ORI
                case 0xF6:
                    {
                        State.A |= Memory.AsSpan()[State.PC++];
                        ConditionCodes cc = State.CC;
                        // Modification de CC
                        cc.Z = State.A == 0 ? (byte)1 : (byte)0;
                        cc.S = (State.A & 0x80) != 0 ? (byte)1 : (byte)0;
                        cc.P = Parity(State.A, 8);
                        cc.CY = 0x00;
                        cc.AC = 0x00;
                        // Affectation de la nouvelle valeur de CC
                        State.CC = cc;
                        Cycles += 2;
                        States += 7;
                        break;
                    }
                // EI
                case 0xFB:
                    {
                        State.IntEnable = 1;
                        Cycles++;
                        States += 4;
                        break;
                    }
                // CPI
                case 0xFE:
                    {
                        byte value = Memory.AsSpan()[State.PC++];
                        int result = State.A - value;
                        ConditionCodes cc = State.CC;
                        // Modification de CC
                        cc.Z = result == 0 ? (byte)1 : (byte)0;
                        cc.S = (result & 0x80) != 0 ? (byte)1 : (byte)0;
                        cc.P = Parity(result, 8);
                        cc.CY = State.A < value ? (byte)1 : (byte)0;
                        // Affectation de la nouvelle valeur de CC
                        State.CC = cc;
                        Cycles += 2;
                        States += 7;
                        break;
                    }

                default: { return UnimplementedInstruction(); }
            }
        }

        Steps++;

        return 0;
    }

    public int Disassemble8080Op(int pc)
    {
        ReadOnlySpan<byte> input = Memory.AsSpan();
        byte code = input[pc];
        int opbytes = 1;
        DebugOutput.Write("{0:X4}\t", pc);
        switch (code)
        {
            case 0x00: { DebugOutput.Write("NOP"); break; }
            case 0x01: { DebugOutput.Write("LXI\tB,#${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x02: { DebugOutput.Write("STAX\tB"); break; }
            case 0x03: { DebugOutput.Write("INX\tB"); break; }
            case 0x04: { DebugOutput.Write("INR\tB"); break; }
            case 0x05: { DebugOutput.Write("DCR\tB"); break; }
            case 0x06: { DebugOutput.Write("MVI\tB,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x07: { DebugOutput.Write("RLC"); break; }

            case 0x09: { DebugOutput.Write("DAD\tB"); break; }
            case 0x0A: { DebugOutput.Write("LDAX\tB"); break; }
            case 0x0B: { DebugOutput.Write("DCX\tB"); break; }
            case 0x0C: { DebugOutput.Write("INR\tC"); break; }
            case 0x0D: { DebugOutput.Write("DCR\tC"); break; }
            case 0x0E: { DebugOutput.Write("MVI\tC,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x0F: { DebugOutput.Write("RRC"); break; }

            case 0x11: { DebugOutput.Write("LXI\tD,#${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x12: { DebugOutput.Write("STAX\tD"); break; }
            case 0x13: { DebugOutput.Write("INX\tD"); break; }
            case 0x14: { DebugOutput.Write("INR\tD"); break; }
            case 0x15: { DebugOutput.Write("DCR\tD"); break; }
            case 0x16: { DebugOutput.Write("MVI\tD,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x17: { DebugOutput.Write("RAL"); break; }

            case 0x19: { DebugOutput.Write("DAD\tD"); break; }
            case 0x1A: { DebugOutput.Write("LDAX\tD"); break; }
            case 0x1B: { DebugOutput.Write("DCX\tD"); break; }
            case 0x1C: { DebugOutput.Write("INR\tE"); break; }
            case 0x1D: { DebugOutput.Write("DCR\tE"); break; }
            case 0x1E: { DebugOutput.Write("MVI\tE,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x1F: { DebugOutput.Write("RAR"); break; }

            case 0x21: { DebugOutput.Write("LXI\tH,#${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x22: { DebugOutput.Write("SHLD\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x23: { DebugOutput.Write("INX\tH"); break; }
            case 0x24: { DebugOutput.Write("INR\tH"); break; }
            case 0x25: { DebugOutput.Write("DCR\tH"); break; }
            case 0x26: { DebugOutput.Write("MVI\tH,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x27: { DebugOutput.Write("DAA"); break; }

            case 0x29: { DebugOutput.Write("DAD\tH"); break; }
            case 0x2A: { DebugOutput.Write("LHLD\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x2B: { DebugOutput.Write("DCX\tH"); break; }
            case 0x2C: { DebugOutput.Write("INR\tL"); break; }
            case 0x2D: { DebugOutput.Write("DCR\tL"); break; }
            case 0x2E: { DebugOutput.Write("MVI\tL,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x2F: { DebugOutput.Write("CMA"); break; }

            case 0x31: { DebugOutput.Write("LXI\tSP,#${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x32: { DebugOutput.Write("STA\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x33: { DebugOutput.Write("INX\tSP"); break; }
            case 0x34: { DebugOutput.Write("INR\tM"); break; }
            case 0x35: { DebugOutput.Write("DCR\tM"); break; }
            case 0x36: { DebugOutput.Write("MVI\tM,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x37: { DebugOutput.Write("STC"); break; }

            case 0x39: { DebugOutput.Write("DAD\tSP"); break; }
            case 0x3A: { DebugOutput.Write("LDA\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0x3B: { DebugOutput.Write("DCX\tSP"); break; }
            case 0x3C: { DebugOutput.Write("INR\tA"); break; }
            case 0x3D: { DebugOutput.Write("DCR\tA"); break; }
            case 0x3E: { DebugOutput.Write("MVI\tA,#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0x3F: { DebugOutput.Write("CMC"); break; }
            case 0x40: { DebugOutput.Write("MOV\tB,B"); break; }
            case 0x41: { DebugOutput.Write("MOV\tB,C"); break; }
            case 0x42: { DebugOutput.Write("MOV\tB,D"); break; }
            case 0x43: { DebugOutput.Write("MOV\tB,E"); break; }
            case 0x44: { DebugOutput.Write("MOV\tB,H"); break; }
            case 0x45: { DebugOutput.Write("MOV\tB,L"); break; }
            case 0x46: { DebugOutput.Write("MOV\tB,M"); break; }
            case 0x47: { DebugOutput.Write("MOV\tB,A"); break; }
            case 0x48: { DebugOutput.Write("MOV\tC,B"); break; }
            case 0x49: { DebugOutput.Write("MOV\tC,C"); break; }
            case 0x4A: { DebugOutput.Write("MOV\tC,D"); break; }
            case 0x4B: { DebugOutput.Write("MOV\tC,E"); break; }
            case 0x4C: { DebugOutput.Write("MOV\tC,H"); break; }
            case 0x4D: { DebugOutput.Write("MOV\tC,L"); break; }
            case 0x4E: { DebugOutput.Write("MOV\tC,M"); break; }
            case 0x4F: { DebugOutput.Write("MOV\tC,A"); break; }
            case 0x50: { DebugOutput.Write("MOV\tD,B"); break; }
            case 0x51: { DebugOutput.Write("MOV\tD,C"); break; }
            case 0x52: { DebugOutput.Write("MOV\tD,D"); break; }
            case 0x53: { DebugOutput.Write("MOV\tD,E"); break; }
            case 0x54: { DebugOutput.Write("MOV\tD,H"); break; }
            case 0x55: { DebugOutput.Write("MOV\tD,L"); break; }
            case 0x56: { DebugOutput.Write("MOV\tD,M"); break; }
            case 0x57: { DebugOutput.Write("MOV\tD,A"); break; }
            case 0x58: { DebugOutput.Write("MOV\tE,B"); break; }
            case 0x59: { DebugOutput.Write("MOV\tE,C"); break; }
            case 0x5A: { DebugOutput.Write("MOV\tE,D"); break; }
            case 0x5B: { DebugOutput.Write("MOV\tE,E"); break; }
            case 0x5C: { DebugOutput.Write("MOV\tE,H"); break; }
            case 0x5D: { DebugOutput.Write("MOV\tE,L"); break; }
            case 0x5E: { DebugOutput.Write("MOV\tE,M"); break; }
            case 0x5F: { DebugOutput.Write("MOV\tE,A"); break; }
            case 0x60: { DebugOutput.Write("MOV\tH,B"); break; }
            case 0x61: { DebugOutput.Write("MOV\tH,C"); break; }
            case 0x62: { DebugOutput.Write("MOV\tH,D"); break; }
            case 0x63: { DebugOutput.Write("MOV\tH,E"); break; }
            case 0x64: { DebugOutput.Write("MOV\tH,H"); break; }
            case 0x65: { DebugOutput.Write("MOV\tH,L"); break; }
            case 0x66: { DebugOutput.Write("MOV\tH,M"); break; }
            case 0x67: { DebugOutput.Write("MOV\tH,A"); break; }
            case 0x68: { DebugOutput.Write("MOV\tL,B"); break; }
            case 0x69: { DebugOutput.Write("MOV\tL,C"); break; }
            case 0x6A: { DebugOutput.Write("MOV\tL,D"); break; }
            case 0x6B: { DebugOutput.Write("MOV\tL,E"); break; }
            case 0x6C: { DebugOutput.Write("MOV\tL,H"); break; }
            case 0x6D: { DebugOutput.Write("MOV\tL,L"); break; }
            case 0x6E: { DebugOutput.Write("MOV\tL,M"); break; }
            case 0x6F: { DebugOutput.Write("MOV\tL,A"); break; }
            case 0x70: { DebugOutput.Write("MOV\tM,B"); break; }
            case 0x71: { DebugOutput.Write("MOV\tM,C"); break; }
            case 0x72: { DebugOutput.Write("MOV\tM,D"); break; }
            case 0x73: { DebugOutput.Write("MOV\tM,E"); break; }
            case 0x74: { DebugOutput.Write("MOV\tM,H"); break; }
            case 0x75: { DebugOutput.Write("MOV\tM,L"); break; }
            case 0x76: { DebugOutput.Write("HLT"); break; }
            case 0x77: { DebugOutput.Write("MOV\tM,A"); break; }
            case 0x78: { DebugOutput.Write("MOV\tA,B"); break; }
            case 0x79: { DebugOutput.Write("MOV\tA,C"); break; }
            case 0x7A: { DebugOutput.Write("MOV\tA,D"); break; }
            case 0x7B: { DebugOutput.Write("MOV\tA,E"); break; }
            case 0x7C: { DebugOutput.Write("MOV\tA,H"); break; }
            case 0x7D: { DebugOutput.Write("MOV\tA,L"); break; }
            case 0x7E: { DebugOutput.Write("MOV\tA,M"); break; }
            case 0x7F: { DebugOutput.Write("MOV\tA,A"); break; }
            case 0x80: { DebugOutput.Write("ADD\tB"); break; }
            case 0x81: { DebugOutput.Write("ADD\tC"); break; }
            case 0x82: { DebugOutput.Write("ADD\tD"); break; }
            case 0x83: { DebugOutput.Write("ADD\tE"); break; }
            case 0x84: { DebugOutput.Write("ADD\tH"); break; }
            case 0x85: { DebugOutput.Write("ADD\tL"); break; }
            case 0x86: { DebugOutput.Write("ADD\tM"); break; }
            case 0x87: { DebugOutput.Write("ADD\tA"); break; }
            case 0x88: { DebugOutput.Write("ADC\tB"); break; }
            case 0x89: { DebugOutput.Write("ADC\tC"); break; }
            case 0x8A: { DebugOutput.Write("ADC\tD"); break; }
            case 0x8B: { DebugOutput.Write("ADC\tE"); break; }
            case 0x8C: { DebugOutput.Write("ADC\tH"); break; }
            case 0x8D: { DebugOutput.Write("ADC\tL"); break; }
            case 0x8E: { DebugOutput.Write("ADC\tM"); break; }
            case 0x8F: { DebugOutput.Write("ADC\tA"); break; }
            case 0x90: { DebugOutput.Write("SUB\tB"); break; }
            case 0x91: { DebugOutput.Write("SUB\tC"); break; }
            case 0x92: { DebugOutput.Write("SUB\tD"); break; }
            case 0x93: { DebugOutput.Write("SUB\tE"); break; }
            case 0x94: { DebugOutput.Write("SUB\tH"); break; }
            case 0x95: { DebugOutput.Write("SUB\tL"); break; }
            case 0x96: { DebugOutput.Write("SUB\tM"); break; }
            case 0x97: { DebugOutput.Write("SUB\tA"); break; }
            case 0x98: { DebugOutput.Write("SBB\tB"); break; }
            case 0x99: { DebugOutput.Write("SBB\tC"); break; }
            case 0x9A: { DebugOutput.Write("SBB\tD"); break; }
            case 0x9B: { DebugOutput.Write("SBB\tE"); break; }
            case 0x9C: { DebugOutput.Write("SBB\tH"); break; }
            case 0x9D: { DebugOutput.Write("SBB\tL"); break; }
            case 0x9E: { DebugOutput.Write("SBB\tM"); break; }
            case 0x9F: { DebugOutput.Write("SBB\tA"); break; }
            case 0xA0: { DebugOutput.Write("ANA\tB"); break; }
            case 0xA1: { DebugOutput.Write("ANA\tC"); break; }
            case 0xA2: { DebugOutput.Write("ANA\tD"); break; }
            case 0xA3: { DebugOutput.Write("ANA\tE"); break; }
            case 0xA4: { DebugOutput.Write("ANA\tH"); break; }
            case 0xA5: { DebugOutput.Write("ANA\tL"); break; }
            case 0xA6: { DebugOutput.Write("ANA\tM"); break; }
            case 0xA7: { DebugOutput.Write("ANA\tA"); break; }
            case 0xA8: { DebugOutput.Write("XRA\tB"); break; }
            case 0xA9: { DebugOutput.Write("XRA\tC"); break; }
            case 0xAA: { DebugOutput.Write("XRA\tD"); break; }
            case 0xAB: { DebugOutput.Write("XRA\tE"); break; }
            case 0xAC: { DebugOutput.Write("XRA\tH"); break; }
            case 0xAD: { DebugOutput.Write("XRA\tL"); break; }
            case 0xAE: { DebugOutput.Write("XRA\tM"); break; }
            case 0xAF: { DebugOutput.Write("XRA\tA"); break; }
            case 0xB0: { DebugOutput.Write("ORA\tB"); break; }
            case 0xB1: { DebugOutput.Write("ORA\tC"); break; }
            case 0xB2: { DebugOutput.Write("ORA\tD"); break; }
            case 0xB3: { DebugOutput.Write("ORA\tE"); break; }
            case 0xB4: { DebugOutput.Write("ORA\tH"); break; }
            case 0xB5: { DebugOutput.Write("ORA\tL"); break; }
            case 0xB6: { DebugOutput.Write("ORA\tM"); break; }
            case 0xB7: { DebugOutput.Write("ORA\tA"); break; }
            case 0xB8: { DebugOutput.Write("CMP\tB"); break; }
            case 0xB9: { DebugOutput.Write("CMP\tC"); break; }
            case 0xBA: { DebugOutput.Write("CMP\tD"); break; }
            case 0xBB: { DebugOutput.Write("CMP\tE"); break; }
            case 0xBC: { DebugOutput.Write("CMP\tH"); break; }
            case 0xBD: { DebugOutput.Write("CMP\tL"); break; }
            case 0xBE: { DebugOutput.Write("CMP\tM"); break; }
            case 0xBF: { DebugOutput.Write("CMP\tA"); break; }
            case 0xC0: { DebugOutput.Write("RNZ"); break; }
            case 0xC1: { DebugOutput.Write("POP\tB"); break; }
            case 0xC2: { DebugOutput.Write("JNZ\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xC3: { DebugOutput.Write("JMP\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xC4: { DebugOutput.Write("CNZ\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xC5: { DebugOutput.Write("PUSH\tB"); break; }
            case 0xC6: { DebugOutput.Write("ADI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xC7: { DebugOutput.Write("RST\t0"); break; }
            case 0xC8: { DebugOutput.Write("RZ"); break; }
            case 0xC9: { DebugOutput.Write("RET"); break; }
            case 0xCA: { DebugOutput.Write("JZ\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }

            case 0xCC: { DebugOutput.Write("CZ\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xCD: { DebugOutput.Write("CALL\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xCE: { DebugOutput.Write("ACI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xCF: { DebugOutput.Write("RST\t1"); break; }
            case 0xD0: { DebugOutput.Write("RNC"); break; }
            case 0xD1: { DebugOutput.Write("POP\tD"); break; }
            case 0xD2: { DebugOutput.Write("JNC\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xD3: { DebugOutput.Write("OUT\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xD4: { DebugOutput.Write("CNC\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xD5: { DebugOutput.Write("PUSH\tD"); break; }
            case 0xD6: { DebugOutput.Write("SUI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xD7: { DebugOutput.Write("RST\t2"); break; }
            case 0xD8: { DebugOutput.Write("RC"); break; }

            case 0xDA: { DebugOutput.Write("JC\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xDB: { DebugOutput.Write("IN\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xDC: { DebugOutput.Write("CC\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }

            case 0xDE: { DebugOutput.Write("SBI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xDF: { DebugOutput.Write("RST\t3"); break; }
            case 0xE0: { DebugOutput.Write("RPO"); break; }
            case 0xE1: { DebugOutput.Write("POP\tH"); break; }
            case 0xE2: { DebugOutput.Write("JPO\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xE3: { DebugOutput.Write("XTHL"); break; }
            case 0xE4: { DebugOutput.Write("CPO\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xE5: { DebugOutput.Write("PUSH\tH"); break; }
            case 0xE6: { DebugOutput.Write("ANI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xE7: { DebugOutput.Write("RST\t4"); break; }
            case 0xE8: { DebugOutput.Write("RPE"); break; }
            case 0xE9: { DebugOutput.Write("PCHL"); break; }
            case 0xEA: { DebugOutput.Write("JPE\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xEB: { DebugOutput.Write("XCHG"); break; }
            case 0xEC: { DebugOutput.Write("CPE\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }

            case 0xEE: { DebugOutput.Write("XRI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xEF: { DebugOutput.Write("RST\t5"); break; }
            case 0xF0: { DebugOutput.Write("RP"); break; }
            case 0xF1: { DebugOutput.Write("POP\tPSW"); break; }
            case 0xF2: { DebugOutput.Write("JP\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xF3: { DebugOutput.Write("DI"); break; }
            case 0xF4: { DebugOutput.Write("CP\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xF5: { DebugOutput.Write("PUSH\tPSW"); break; }
            case 0xF6: { DebugOutput.Write("ORI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xF7: { DebugOutput.Write("RST\t6"); break; }
            case 0xF8: { DebugOutput.Write("RM"); break; }
            case 0xF9: { DebugOutput.Write("SPHL"); break; }
            case 0xFA: { DebugOutput.Write("JM\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }
            case 0xFB: { DebugOutput.Write("EI"); break; }
            case 0xFC: { DebugOutput.Write("CM\t${0:X2}{1:X2}", input[pc + 2], input[pc + 1]); opbytes = 3; break; }

            case 0xFE: { DebugOutput.Write("CPI\t#${0:X2}", input[pc + 1]); opbytes = 2; break; }
            case 0xFF: { DebugOutput.Write("RST\t7"); break; }
        }
        DebugOutput.WriteLine();
        return opbytes;
    }

    private int UnimplementedInstruction()
    {
        State.PC--;
        DebugOutput.WriteLine(string.Format("Error: Unimplemented instruction : {0:X2}", Memory.AsSpan()[State.PC]));
        Disassemble8080Op(State.PC);
        return 1;
    }

    private static byte Parity(int value, int size)
    {
        int i;
        int p = 0;
        value &= (1 << size) - 1;
        for (i = 0; i < size; i++)
        {
            if ((value & 0x1) != 0) p++;
            value >>= 1;
        }
        return (byte)((0 == (p & 0x1)) ? 0x01 : 0x00);
    }

    private void Call()
    {
        Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.SP - 2]) = (ushort)(State.PC + 2);
        State.SP -= 2;
        State.PC = Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.PC]);
    }

    private void Return()
    {
        State.PC = Unsafe.As<byte, ushort>(ref Memory.AsSpan()[State.SP]);
        State.SP += 2;
    }
}
