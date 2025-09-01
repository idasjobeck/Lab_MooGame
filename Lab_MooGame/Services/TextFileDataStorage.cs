using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;

namespace Lab_MooGame.Services;

public class TextFileDataStorage : IDataStorage
{
    private string _filePath;
    public string FilePath => _filePath;
    private readonly IFileSystem _fileSystem;

    public TextFileDataStorage(string filePath) : this(new FileSystem(), filePath)
    {
        _filePath = filePath;
    }

    public TextFileDataStorage(IFileSystem fileSystem, string filePath)
    {
        _fileSystem = fileSystem;
        _filePath = filePath;
    }

    public void SaveData(CurrentGameUserScore currentGameUsernameAndScore, string separator)
    {
        using (var streamWriter = _fileSystem.File.AppendText(_filePath))
        {
            streamWriter.WriteLine($"{currentGameUsernameAndScore.UserName}{separator}{currentGameUsernameAndScore.NumberOfGuesses}");
        }
    }

    public List<string> GetData()
    {
        var data = new List<string>();
        string? lineOfTextRead;

        using (var streamReader = _fileSystem.File.OpenText(_filePath))
        {
            while ((lineOfTextRead = streamReader.ReadLine()) != null)
            {
                data.Add(lineOfTextRead);
            }
        }

        return data;
    }
}