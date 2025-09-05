using System.Xml.Linq;

namespace Lab_MooGame.Models;

/// <summary>
/// Represents the statistical data for a player, including their username, the number of games played,  and the total
/// number of guesses made.
/// </summary>
/// <remarks>This class provides methods to update the player's statistics and calculate their average number of 
/// guesses per game. Instances of this class are immutable with respect to the player's username, but  the statistics
/// (number of games and total guesses) can be updated.</remarks>

public class PlayerStats
{
    public string UserName { get; private set; }
    public int NumberOfGames { get; private set; }
    public int TotalNumberOfGuesses { get; private set; }

    public PlayerStats(string userName, int numberOfGuesses)
    {
        UserName = userName;
        NumberOfGames = 1;
        TotalNumberOfGuesses = numberOfGuesses;
    }

    public void UpdateStats(int numberOfGuesses)
    {
        TotalNumberOfGuesses += numberOfGuesses;
        NumberOfGames++;
    }

    public double AverageNumberOfGuesses()
    {
        return (double)TotalNumberOfGuesses / NumberOfGames;
    }

    public override bool Equals(Object? p)
    {
        if (p is not PlayerStats playerStats)
            return false;

        return UserName == playerStats.UserName && 
               NumberOfGames == playerStats.NumberOfGames && 
               TotalNumberOfGuesses == playerStats.TotalNumberOfGuesses;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserName, NumberOfGames, TotalNumberOfGuesses);
    }
}