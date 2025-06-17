using Lab_MooGame.UI;

namespace Lab_MooGame.Models;

class MooGame : IGuessingGame
{
    public string Name => "Moo Game";
    public string Description => "A game where you guess a 4-digit number with no repeating digits. " +
                                 "You get feedback in the form of 'B' for bulls (correct digit and position) " +
                                 "and 'C' for cows (correct digit but wrong position).";

    private string _target = "";
    public string Target => _target ?? throw new InvalidOperationException("Target is not set. Call SetUpNewGame first.");
    private int _numberOfGuesses;
    public int NumberOfGuesses => _numberOfGuesses;

    private IUserInterface _userInterface;

    public MooGame(IUserInterface userInterface)
    {
        _userInterface = userInterface;
    }

    public void WriteToScoreBoard(string? userName)
    {
        var streamWriter = new StreamWriter("result.txt", append: true);
        streamWriter.WriteLine($"{userName}#&#{_numberOfGuesses}");
        streamWriter.Close();
    }

    public void SetUpNewGame()
    {
        _target = GenerateTarget();
        _numberOfGuesses = 0;
    }

    private string GenerateTarget()
    {
        var randomNumberGenerator = new Random();
        var target = "";

        for (int i = 0; i < 4; i++)
        {
            var randomDigit = randomNumberGenerator.Next(10).ToString();

            while (target.Contains(randomDigit))
            {
                randomDigit = randomNumberGenerator.Next(10).ToString();
            }

            target += randomDigit;
        }

        return target;
    }

    public string CheckGuess(string? guess)
    {
        _numberOfGuesses++;
        var numberOfCows = 0;
        var numberOfBulls = 0;
        guess += "    "; // if player entered less than 4 chars

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (_target[i] == guess[j])
                {
                    if (i == j)
                    {
                        numberOfBulls++;
                    }
                    else
                    {
                        numberOfCows++;
                    }
                }
            }
        }

        var result = $"{"BBBB".Substring(0, numberOfBulls)},{"CCCC".Substring(0, numberOfCows)}";

        return result;
    }

    public bool IsGuessCorrect(string resultToCheck)
    {
        return resultToCheck == "BBBB,";
    }


    public void ShowScoreBoard()
    {
        var streamReader = new StreamReader("result.txt");
        var results = new List<PlayerData>();
        string? lineOfTextRead;

        while ((lineOfTextRead = streamReader.ReadLine()) != null)
        {
            string[] nameAndScore = lineOfTextRead.Split(["#&#"], StringSplitOptions.None);
            var userName = nameAndScore[0];
            var numberOfGuesses = Convert.ToInt32(nameAndScore[1]);
            var playerData = new PlayerData(userName, numberOfGuesses);
            var position = results.IndexOf(playerData);

            if (position < 0)
                results.Add(playerData);
            else
                results[position].Update(numberOfGuesses);
        }

        results.Sort((player1, player2) => player1.Average().CompareTo(player2.Average()));
        _userInterface.WriteLine("Player   games  average");
        foreach (var player in results)
        {
            _userInterface.WriteLine($"{player.UserName,-9}{player.NumberOfGames,5:D}{player.Average(),9:F2}");
        }

        streamReader.Close();
    }
}