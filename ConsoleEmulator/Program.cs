using ConsoleEmulator;

byte[] buffer = new byte[0x10000];
int size;
using (FileStream stream = new(args[0], FileMode.Open))
{
    size = (int)stream.Length;
    stream.Read(buffer, 0, size);
}
CPU8080 cpu = new();
cpu.Init(buffer, 0);
while (true)
{
    int done = cpu.Step();
    if (done != 0)
    {
        break;
    }
}