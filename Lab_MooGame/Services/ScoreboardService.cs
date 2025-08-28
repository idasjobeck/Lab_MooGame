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

    public ScoreboardService(IDataStorage dataStorage)
    {
        _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
    }

    public void UpdateScoreBoard(CurrentGameUserScore gameUserScore)
    {
        _dataStorage.SaveData(gameUserScore.UserName, gameUserScore.NumberOfGuesses);
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
            string[] nameAndScore = line.Split(["#&#"], StringSplitOptions.None);
            var userName = nameAndScore[0];
            var numberOfGuesses = Convert.ToInt32(nameAndScore[1]);
            var playerStats = new PlayerStats(userName, numberOfGuesses);
            var position = parsedData.IndexOf(playerStats);
            if (position < 0)
                parsedData.Add(playerStats);
            else
                parsedData[position].Update(numberOfGuesses);
        }
        return parsedData;
    }
}