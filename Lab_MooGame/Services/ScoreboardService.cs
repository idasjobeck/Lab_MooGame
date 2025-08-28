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

    public List<PlayerData> GetTopScores()
    {
        var data = _dataStorage.GetData();
        var topScores = ParseTopScoresData(data);
        topScores.Sort((player1, player2) => player1.Average().CompareTo(player2.Average()));
        return topScores;
    }

    private List<PlayerData> ParseTopScoresData(List<string> data)
    {
        var results = new List<PlayerData>();
        foreach (var line in data)
        {
            string[] nameAndScore = line.Split(["#&#"], StringSplitOptions.None);
            var userName = nameAndScore[0];
            var numberOfGuesses = Convert.ToInt32(nameAndScore[1]);
            var playerData = new PlayerData(userName, numberOfGuesses);
            var position = results.IndexOf(playerData);
            if (position < 0)
                results.Add(playerData);
            else
                results[position].Update(numberOfGuesses);
        }
        return results;
    }
}