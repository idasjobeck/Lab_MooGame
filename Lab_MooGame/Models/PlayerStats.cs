namespace Lab_MooGame.Models;

public class PlayerStats
{
    public string UserName { get; private set; }
    public int NumberOfGames { get; private set; }
    private int _totalNumberOfGuesses;
    public int TotalNumberOfGuesses { get; }

    public PlayerStats(string userName, int numberOfGuesses)
    {
        UserName = userName;
        NumberOfGames = 1;
        _totalNumberOfGuesses = numberOfGuesses;
    }

    public void UpdateStats(int numberOfGuesses)
    {
        _totalNumberOfGuesses += numberOfGuesses;
        NumberOfGames++;
    }

    public double AverageNumberOfGuesses()
    {
        return (double)_totalNumberOfGuesses / NumberOfGames;
    }
}