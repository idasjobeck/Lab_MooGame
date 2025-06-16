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

    public void PlayGame(string userName);

    private string GenerateTarget()
    {
        return ""; // Placeholder for the actual target generation logic
    }

    private string CheckGuess(string target, string? guess)
    {
        return ""; // Placeholder for the actual guess checking logic
    }
}