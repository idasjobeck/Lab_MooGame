namespace Lab_MooGame.UI;

public interface IUserInterface
{
    public void Write(string message);
    public string? Read();
    public void Clear();
}