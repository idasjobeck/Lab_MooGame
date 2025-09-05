namespace Lab_MooGame.Models;

/// <summary>
/// Represents a guessing game with a specific target and rules for making guesses.
/// </summary>
/// <remarks>This interface defines the structure for a guessing game, including properties to describe the game,
/// methods to set up a new game, check guesses, and determine if a guess is correct.</remarks>

public interface IGuessingGame
{
    string Name { get; }
    string Description { get; }
    public string Target { get; }
    public int NumberOfGuesses { get; }

    public void SetUpNewGame();

    public string CheckGuess(string? guess);

    public bool IsGuessCorrect(string resultToCheck);
}