using Lab_MooGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_MooGame.Services;

public interface IScoreboard
{
    public void UpdateScoreBoard(CurrentGameUserScore currentGameUsernameAndScore);
    public List<PlayerStats> GetTopScores();
}

