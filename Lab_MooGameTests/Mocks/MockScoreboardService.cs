using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;
using Lab_MooGame.Services;

namespace Lab_MooGameTests.Mocks;

/// <summary>
/// Provides a mock implementation of the <see cref="IScoreboard"/> interface for managing and retrieving player scores.
/// </summary>
/// <remarks>This class is intended for testing or development purposes where a fully functional scoreboard
/// service is not required. It uses an <see cref="IDataStorage"/> instance to persist data and provides basic
/// functionality for updating and retrieving scores.</remarks>

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
