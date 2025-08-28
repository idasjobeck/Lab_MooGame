using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Services;

namespace Lab_MooGameTests.Mocks;

public class MockDataStorage : IDataStorage
{
    private List<string> _dataStorage = new List<string>();

    public void SaveData(string userName, int numberOfGuesses)
    {
        _dataStorage.Add($"{userName}#&#{numberOfGuesses}");
    }

    public List<string> GetData() => _dataStorage;
}