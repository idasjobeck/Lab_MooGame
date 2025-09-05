using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;

namespace Lab_MooGame.Services;

/// <summary>
/// Defines methods for saving and retrieving game-related data.
/// </summary>
/// <remarks>This interface provides functionality to persist and retrieve data, such as user scores,  in a
/// structured format. Implementations of this interface should ensure data integrity and handle any storage-specific
/// concerns.</remarks>

public interface IDataStorage
{
    public void SaveData(CurrentGameUserScore currentGameUsernameAndScore, string separator);
    public List<string> GetData();
}