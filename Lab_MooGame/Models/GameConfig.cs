using Lab_MooGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Services;

namespace Lab_MooGame.Models;
public class GameConfig
{
    public string Name { get; }
    public int TargetLength { get; }
    public int MaxRange { get; }
    public bool AllowRepeats { get; }
    public ITargetGenerator TargetGenerator { get; }

    public GameConfig(string name, int targetLength, int maxRange, bool allowRepeats, ITargetGenerator targetGenerator)
    {
        Name = name;
        TargetLength = targetLength;
        MaxRange = maxRange;
        AllowRepeats = allowRepeats;
        TargetGenerator = targetGenerator;
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