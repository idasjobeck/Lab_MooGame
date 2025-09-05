namespace Lab_MooGame.UI;

/// <summary>
/// Represents a user interface for interacting with input and output operations.
/// </summary>
/// <remarks>This interface defines methods for writing messages, reading input, and clearing the display. It can
/// be implemented to provide various types of user interfaces, such as console-based or graphical.</remarks>

public interface IUserInterface
{
    public void Write(string message);
    public string? Read();
    public void Clear();
}