using Lab_MooGame.Models;
using Lab_MooGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_MooGameTests.Mocks;

/// <summary>
/// Provides a mock implementation of the <see cref="IDataStorage"/> interface for testing purposes.
/// </summary>
/// <remarks>This class simulates a data storage mechanism using an in-memory list of strings. It is intended for
/// use in  scenarios where a lightweight, non-persistent storage solution is sufficient, such as unit
/// testing.</remarks>

public class MockDataStorage : IDataStorage
{
    private List<string> _dataStorage;

    public MockDataStorage()
    {
        _dataStorage = new List<string>();
    }

    public MockDataStorage(List<string> mockData)
    {
        _dataStorage = mockData;
    }

    public void SaveData(CurrentGameUserScore currentGameUsernameAndScore, string separator)
    {
        _dataStorage.Add($"{currentGameUsernameAndScore.UserName}{separator}{currentGameUsernameAndScore.NumberOfGuesses}");
    }

    public List<string> GetData() => _dataStorage;
}