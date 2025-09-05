using Lab_MooGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Services;

namespace Lab_MooGame.Models;

/// <summary>
/// Represents the configuration settings for a game, including target generation rules and constraints.
/// </summary>
/// <remarks>This class provides a way to define the parameters for a game, such as the name, target length, 
/// maximum range, and whether repeated values are allowed. It also supports specifying a custom  target generator or
/// automatically creating one based on the provided parameters.</remarks>

public class GameConfig
{
    public string Name { get; }
    public int TargetLength { get; }
    public int MaxRange { get; }
    public bool AllowRepeats { get; }
    public ITargetGenerator TargetGenerator { get; }

    public GameConfig(string name, ITargetGenerator targetGenerator)
    {
        Name = name;
        TargetGenerator = targetGenerator;
        TargetLength = targetGenerator.TargetLength;
        MaxRange = targetGenerator.MaxRange;
        AllowRepeats = targetGenerator.AllowRepeats;
    }

    public GameConfig(string name, int targetLength, int maxRange, bool allowRepeats)
    {
        Name = name;
        TargetLength = targetLength;
        MaxRange = maxRange;
        AllowRepeats = allowRepeats;
        TargetGenerator = new TargetGenerator(TargetLength, MaxRange, AllowRepeats);
    }
}