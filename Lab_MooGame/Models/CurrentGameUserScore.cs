using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_MooGame.Models;

/// <summary>
/// Represents the current score of a user in the game, including their name and the number of guesses made.
/// </summary>
/// <remarks>This class is used to track the progress of a user during a game session.  The <see cref="UserName"/>
/// property identifies the user, and the <see cref="NumberOfGuesses"/> property  records the number of guesses the user
/// has made.</remarks>

public class CurrentGameUserScore
{
    public string UserName { get; set; }
    public int NumberOfGuesses { get; set; }

    public CurrentGameUserScore()
    {
        UserName = string.Empty;
    }
}