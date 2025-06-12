using System;
using System.IO;
using System.Collections.Generic;

namespace Lab_MooGame;

class MainClass
{

    public static void Main(string[] args)
    {

        var continuePlaying = true;
        Console.WriteLine("Enter your user name:\n");
        string? userName = Console.ReadLine();

        while (continuePlaying)
        {
            var target = GenerateTarget();


            Console.WriteLine("New game:\n");
            //comment out or remove next line to play real games!
            Console.WriteLine("For practice, number is: " + target + "\n");
            string? guess = Console.ReadLine();

            var numberOfGuesses = 1;
            var result = CheckGuess(target, guess);
            Console.WriteLine(result + "\n");
            while (result != "BBBB,")
            {
                numberOfGuesses++;
                guess = Console.ReadLine();
                Console.WriteLine(guess + "\n");
                result = CheckGuess(target, guess);
                Console.WriteLine(result + "\n");
            }

            var streamWriter = new StreamWriter("result.txt", append: true);
            streamWriter.WriteLine(userName + "#&#" + numberOfGuesses);
            streamWriter.Close();
            ShowScoreBoard();
            Console.WriteLine("Correct, it took " + numberOfGuesses + " guesses\nContinue?");
            string? answer = Console.ReadLine();
            if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
            {
                continuePlaying = false;
            }
        }
    }

    static string GenerateTarget()
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

    static string CheckGuess(string target, string? guess)
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


    static void ShowScoreBoard()
    {
        var streamReader = new StreamReader("result.txt");
        var results = new List<PlayerData>();
        string? lineOfTextRead;

        while ((lineOfTextRead = streamReader.ReadLine()) != null)
        {
            string[] nameAndScore = lineOfTextRead.Split(new string[] { "#&#" }, StringSplitOptions.None);
            var userName = nameAndScore[0];
            var numberOfGuesses = Convert.ToInt32(nameAndScore[1]);
            var playerData = new PlayerData(userName, numberOfGuesses);
            var position = results.IndexOf(playerData);
            if (position < 0)
            {
                results.Add(playerData);
            }
            else
            {
                results[position].Update(numberOfGuesses);
            }


        }

        results.Sort((player1, player2) => player1.Average().CompareTo(player2.Average()));
        Console.WriteLine("Player   games average");
        foreach (var player in results)
        {
            Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", player.UserName, player.NumberOfGames, player.Average()));
        }

        streamReader.Close();
    }
}

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