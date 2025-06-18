using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;

namespace Lab_MooGame.Services;

public class TextFileDataStorage : IDataStorage
{
    private string _filePath;
    public string FilePath => _filePath;

    public TextFileDataStorage(string filePath)
    {
        _filePath = filePath;
    }

    public void SaveData(string userName, int numberOfGuesses)
    {
        var streamWriter = new StreamWriter(_filePath, append: true);
        streamWriter.WriteLine($"{userName}#&#{numberOfGuesses}");
        streamWriter.Close();
    }

    public List<PlayerData> GetData()
    {
        var streamReader = new StreamReader(_filePath);
        var results = new List<PlayerData>();
        string? lineOfTextRead;

        while ((lineOfTextRead = streamReader.ReadLine()) != null)
        {
            string[] nameAndScore = lineOfTextRead.Split(["#&#"], StringSplitOptions.None);
            var userName = nameAndScore[0];
            var numberOfGuesses = Convert.ToInt32(nameAndScore[1]);
            var playerData = new PlayerData(userName, numberOfGuesses);
            var position = results.IndexOf(playerData);

            if (position < 0)
                results.Add(playerData);
            else
                results[position].Update(numberOfGuesses);
        }

        streamReader.Close();

        return results;
    }
}