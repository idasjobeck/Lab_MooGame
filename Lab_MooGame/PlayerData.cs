namespace Lab_MooGame;

class PlayerData
{
    public string UserName { get; private set; }
    public int NumberOfGames { get; private set; }
    int _totalNumberOfGuesses;

    public PlayerData(string userName, int numberOfGuesses)
    {
        this.UserName = userName;
        NumberOfGames = 1;
        _totalNumberOfGuesses = numberOfGuesses;
    }

    public void Update(int numberOfGuesses)
    {
        _totalNumberOfGuesses += numberOfGuesses;
        NumberOfGames++;
    }

    public double Average()
    {
        return (double)_totalNumberOfGuesses / NumberOfGames;
    }

    public override bool Equals(Object? player)
    {
        return UserName.Equals(((PlayerData?)player!).UserName);
    }

    public override int GetHashCode()
    {
        return UserName.GetHashCode();
    }
}