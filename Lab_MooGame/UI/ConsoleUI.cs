namespace Lab_MooGame.UI;

public class ConsoleUI : IUserInterface
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public string? ReadLine()
    {
        return Console.ReadLine();
    }
}