namespace Lab_MooGame.UI;

/// <summary>
/// Provides a console-based implementation of the <see cref="IUserInterface"/> interface,  allowing interaction with
/// the user through standard input and output streams.
/// </summary>
/// <remarks>This class facilitates basic console operations such as writing messages to the console,  reading
/// user input, and clearing the console screen. It is designed for use in applications  that require a simple
/// text-based user interface.</remarks>

public class ConsoleUI : IUserInterface
{
    public void Write(string message)
    {
        Console.WriteLine(message);
    }

    public string? Read()
    {
        return Console.ReadLine();
    }

    public void Clear()
    {
        Console.Clear();
    }
}