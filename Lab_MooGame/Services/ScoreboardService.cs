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

    public void UpdateScoreBoard(string userName, int numberOfGuesses)
    {
        _dataStorage.SaveData(userName, numberOfGuesses);
    }

    public List<PlayerData> GetTopScores()
    {
        var results = _dataStorage.GetData();
        results.Sort((player1, player2) => player1.Average().CompareTo(player2.Average()));
        return results;
    }
}