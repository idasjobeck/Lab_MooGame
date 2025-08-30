using Lab_MooGame.Models;
using Lab_MooGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_MooGameTests.Mocks;

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