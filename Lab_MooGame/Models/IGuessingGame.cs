namespace Lab_MooGame.Models;

public interface IGuessingGame
{
    string Name { get; }
    string Description { get; }
    public string Target { get; }
    public int NumberOfGuesses { get; }

    public void SetUpNewGame();

    private string GenerateTarget()
    {
        return ""; // Placeholder for the actual target generation logic
    }

    public string CheckGuess(string? guess);

    public bool IsGuessCorrect(string resultToCheck);
}