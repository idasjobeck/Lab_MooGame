using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;
using Lab_MooGame.Services;

namespace Lab_MooGameTests.Mocks;
public class MockScoreboardService : IScoreboard
{
    private readonly IDataStorage _dataStorage;
    private readonly string _separator = "#&#";
    public MockScoreboardService(IDataStorage dataStorage)
    {
        _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
    }

    public void UpdateScoreBoard(CurrentGameUserScore currentGameUsernameAndScore)
    {
        _dataStorage.SaveData(currentGameUsernameAndScore, _separator);
    }

    public List<PlayerStats> GetTopScores()
    {
        var emptyTopScores = new List<PlayerStats>();
        return emptyTopScores;
    }
}
