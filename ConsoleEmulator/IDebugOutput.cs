﻿namespace ConsoleEmulator;

internal interface IDebugOutput
{
    void WriteLine();
    void WriteLine(string message);
    void Write(string message);
    void Write(string messageFormat, object? arg0);
    void Write(string messageFormat, object? arg0, object? arg1);
}
