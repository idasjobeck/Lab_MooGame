using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_MooGame;

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

    public void WriteToScoreBoard(string? userName);

    public void ShowScoreBoard();
}