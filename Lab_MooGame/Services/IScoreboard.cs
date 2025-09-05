using Lab_MooGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_MooGame.Services;

/// <summary>
/// Represents a scoreboard that tracks and manages player scores.
/// </summary>
/// <remarks>This interface provides methods to update the scoreboard with a player's current score  and retrieve
/// the top player scores. Implementations of this interface should ensure  thread safety if accessed
/// concurrently.</remarks>

public interface IScoreboard
{
    public void UpdateScoreBoard(CurrentGameUserScore currentGameUsernameAndScore);
    public List<PlayerStats> GetTopScores();
}

