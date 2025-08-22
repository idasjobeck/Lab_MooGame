using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_MooGame.Models;

public class CurrentGameUserScore
{
    public string UserName { get; set; }
    public int NumberOfGuesses { get; set; }

    public CurrentGameUserScore()
    {
        UserName = string.Empty;
    }
}