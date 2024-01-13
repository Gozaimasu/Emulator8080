using ConsoleEmulator;

CPU8080 cpu = new();

byte[] buffer = new byte[0x4000];
ConsoleCPUDiag.Properties.Resources.cpudiag.AsSpan().CopyTo(buffer.AsSpan()[0x0100..]);

cpu.Init(buffer, 0x100);
cpu.SystemCall = new CPMSystemCall();

while(true)
{
    int done = cpu.Step();
    if (done != 0)
    {
        break;
    }
}