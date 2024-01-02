using System.Text;

namespace ConsoleEmulator.Test;

internal class TestDebugOutput : IDebugOutput
{
    private readonly StringBuilder _outputBuilder;

    public TestDebugOutput()
    {
        _outputBuilder = new StringBuilder();
    }

    public void Write(string message)
    {
        _outputBuilder.Append(message);
    }

    public void Write(char c)
    {
        _outputBuilder.Append(c);
    }

    public void Write(string messageFormat, object? arg0)
    {
        _outputBuilder.AppendFormat(messageFormat, arg0);
    }

    public void Write(string messageFormat, object? arg0, object? arg1)
    {
        _outputBuilder.AppendFormat(messageFormat, arg0, arg1);
    }

    public void WriteLine()
    {
        _outputBuilder.AppendLine();
    }

    public void WriteLine(string message)
    {
        _outputBuilder.AppendLine(message);
    }

    public string Output
    {
        get
        {
            return _outputBuilder.ToString();
        }
    }
}
