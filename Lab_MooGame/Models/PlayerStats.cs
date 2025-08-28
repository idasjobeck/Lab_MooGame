namespace Lab_MooGame.Models;

public class PlayerStats
{
    public string UserName { get; private set; }
    public int NumberOfGames { get; private set; }
    int _totalNumberOfGuesses;

    public PlayerStats(string userName, int numberOfGuesses)
    {
        UserName = userName;
        NumberOfGames = 1;
        _totalNumberOfGuesses = numberOfGuesses;
    }

    public void Update(int numberOfGuesses)
    {
        _totalNumberOfGuesses += numberOfGuesses;
        NumberOfGames++;
    }

    public double AverageNumberOfGuesses()
    {
        return (double)_totalNumberOfGuesses / NumberOfGames;
    }
}