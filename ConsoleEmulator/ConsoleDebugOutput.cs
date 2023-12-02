namespace ConsoleEmulator;

internal class ConsoleDebugOutput : IDebugOutput
{
    public void Write(string message)
    {
        Console.Write(message);
    }

    public void Write(string messageFormat, object? arg0)
    {
        Console.WriteLine(messageFormat, arg0);
    }

    public void Write(string messageFormat, object? arg0, object? arg1)
    {
        Console.WriteLine(messageFormat, arg0, arg1);
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteLine()
    {
        Console.WriteLine();
    }
}
