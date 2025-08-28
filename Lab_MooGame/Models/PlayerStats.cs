namespace Lab_MooGame.Models;

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
}