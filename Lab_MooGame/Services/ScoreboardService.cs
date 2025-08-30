using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;

namespace Lab_MooGame.Services;

public class ScoreboardService
{
    private readonly IDataStorage _dataStorage;
    private readonly string _separator = "#&#";

    public ScoreboardService(IDataStorage dataStorage)
    {
        _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
    }

    public void UpdateScoreBoard(CurrentGameUserScore currentGameUsernameAndScore)
    {
        _dataStorage.SaveData(currentGameUsernameAndScore, _separator);
    }

    public List<PlayerStats> GetTopScores()
    {
        var data = _dataStorage.GetData();
        var topScores = ParseTopScoresData(data);
        topScores.Sort((player1, player2) => player1.AverageNumberOfGuesses().CompareTo(player2.AverageNumberOfGuesses()));
        return topScores;
    }

    private List<PlayerStats> ParseTopScoresData(List<string> data)
    {
        var parsedData = new List<PlayerStats>();

        foreach (var line in data)
        {
            string[] nameAndScore = line.Split([_separator], StringSplitOptions.None);
            var userName = nameAndScore[0];
            var numberOfGuesses = ParseNumberOfGuesses(nameAndScore[1]);

            var existingPlayer = parsedData.Find(p => p.UserName == userName);
            if (existingPlayer == null)
                parsedData.Add(new PlayerStats(userName, numberOfGuesses));
            else
                existingPlayer.UpdateStats(numberOfGuesses);
        }

        return parsedData;
    }

    private int ParseNumberOfGuesses(string numberOfGuesses)
    {
        var isNumber = int.TryParse(numberOfGuesses, out int result);

        if (!isNumber)
            throw new InvalidDataException("Number of guesses is not a numerical value.");

        return result;
    }
}