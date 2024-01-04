namespace ConsoleEmulator;

public interface IDebugOutput
{
    void WriteLine();
    void WriteLine(string message);
    void Write(string message);
    void Write(char c);
    void Write(string messageFormat, object? arg0);
    void Write(string messageFormat, object? arg0, object? arg1);
}
