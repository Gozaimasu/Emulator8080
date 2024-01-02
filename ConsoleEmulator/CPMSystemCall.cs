using System;
using System.Runtime.CompilerServices;

namespace ConsoleEmulator;

/// <summary>
/// https://www.seasip.info/Cpm/bdos.html
/// </summary>
public class CPMSystemCall : ISystemCall
{
    public bool TryStep(CPU8080 cpu, out int result)
    {
        result = 0;
        if (cpu.State.PC == 5)
        {
            if (cpu.State.C == 9)
            {
                // C_WRITESTR
                ushort addr = (ushort)((cpu.State.D << 8) + cpu.State.E);
                //addr += 3;
                char str = (char)cpu.Memory.AsSpan()[addr++];
                while (str != '$')
                {
                    CPU8080.DebugOutput.Write(str);
                    str = (char)cpu.Memory.AsSpan()[addr++];
                }
                cpu.State.PC = Unsafe.As<byte, ushort>(ref cpu.Memory.AsSpan()[cpu.State.SP]);
                cpu.State.SP += 2;
                return true;
            }

            if (cpu.State.C == 2)
            {
                char c = (char)cpu.State.E;
                CPU8080.DebugOutput.Write(c);
                cpu.State.PC = Unsafe.As<byte, ushort>(ref cpu.Memory.AsSpan()[cpu.State.SP]);
                cpu.State.SP += 2;
                return true;
            }

            throw new NotImplementedException();
        }
        if (cpu.State.PC == 0)
        {
            result = 1;
            return true;
        }

        return false;
    }
}
