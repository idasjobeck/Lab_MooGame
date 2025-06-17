using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;

namespace Lab_MooGame.Services;

public class TextFileStrategy : IDataStorage
{
    private string _filePath;
    private string _fileName;
    private string _fullPath;
    public string FilePath => _filePath;
    public string FileName => _fileName;

    public TextFileStrategy(string filePath, string fileName)
    {
        _filePath = filePath;
        _fileName = fileName;
        _fullPath = $"{(String.IsNullOrEmpty(_filePath) ? "" : _filePath + "\\")}{_fileName}";
    }

    public void SaveData(string userName, int numberOfGuesses)
    {
        var streamWriter = new StreamWriter(_fullPath, append: true);
        streamWriter.WriteLine($"{userName}#&#{numberOfGuesses}");
        streamWriter.Close();
    }

    public List<PlayerData> GetData()
    {
        var streamReader = new StreamReader(_fullPath);
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