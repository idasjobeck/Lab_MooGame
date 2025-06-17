using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;

namespace Lab_MooGame.Services;

public interface IDataStorage
{
    public void SaveData(string userName, int numberOfGuesses);
    public List<PlayerData> GetData();
}