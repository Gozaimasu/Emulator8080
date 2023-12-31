namespace ConsoleEmulator;

public interface ISystemCall
{
    bool TryStep(CPU8080 cpu, out int result);
}
