namespace Lab_MooGame.UI;

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
}