namespace Lab_MooGame;

class MooGame : IGuessingGame
{
    public string Name => "Moo Game";
    public string Description => "A game where you guess a 4-digit number with no repeating digits. " +
                                 "You get feedback in the form of 'B' for bulls (correct digit and position) " +
                                 "and 'C' for cows (correct digit but wrong position).";
    private IUserInterface _userInterface;

    public MooGame(IUserInterface userInterface)
    {
        _userInterface = userInterface;
    }

    public void PlayGame(string? userName)
    {
        var target = GenerateTarget();
        _userInterface.WriteLine("New game:\n");
        //comment out or remove next line to play real games!
        _userInterface.WriteLine($"For practice, number is: {target} \n");
        string? guess = _userInterface.ReadLine();

        var numberOfGuesses = 1;
        var result = CheckGuess(target, guess);
        _userInterface.WriteLine($"{result}\n");
        while (result != "BBBB,")
        {
            numberOfGuesses++;
            guess = _userInterface.ReadLine();
            _userInterface.WriteLine($"{guess}\n");
            result = CheckGuess(target, guess);
            _userInterface.WriteLine($"{result}\n");
        }

        var streamWriter = new StreamWriter("result.txt", append: true);
        streamWriter.WriteLine($"{userName}#&#{numberOfGuesses}");
        streamWriter.Close();

        ShowScoreBoard();

        _userInterface.WriteLine($"Correct, it took {numberOfGuesses} guesses");
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

    private string CheckGuess(string target, string? guess)
    {
        var numberOfCows = 0;
        var numberOfBulls = 0;
        guess += "    "; // if player entered less than 4 chars

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (target[i] == guess[j])
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


    private void ShowScoreBoard()
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