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

    public void SaveData(CurrentGameUserScore currentGameUsernameAndScore, string separator)
    {
        using (StreamWriter streamWriter = new StreamWriter(_filePath, append: true))
        {
            streamWriter.WriteLine($"{currentGameUsernameAndScore.UserName}{separator}{currentGameUsernameAndScore.NumberOfGuesses}");
        }
    }

    public List<string> GetData()
    {
        var data = new List<string>();
        string? lineOfTextRead;

        using (StreamReader streamReader = new StreamReader(_filePath))
        {
            while ((lineOfTextRead = streamReader.ReadLine()) != null)
            {
                data.Add(lineOfTextRead);
            }
        }

        return data;
    }
}