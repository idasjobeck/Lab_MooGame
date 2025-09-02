using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;

namespace Lab_MooGame.Services;

public class TargetGenerator : ITargetGenerator
{
    private readonly IRandom _randomNumberGenerator;
    private readonly int _targetLength;
    public int TargetLength => _targetLength;
    private readonly int _maxRange;
    public int MaxRange => _maxRange;
    private readonly bool _allowRepeats;
    public bool AllowRepeats => _allowRepeats;

    public TargetGenerator(int targetLength, int maxRange, bool allowRepeats)
    {
        _targetLength = targetLength;
        _maxRange = maxRange;
        _allowRepeats = allowRepeats;
        _randomNumberGenerator = new SystemRandom(); // Default random number generator
    }

    public TargetGenerator(int targetLength, int maxRange, bool allowRepeats, IRandom random)
    {
        _targetLength = targetLength;
        _maxRange = maxRange;
        _allowRepeats = allowRepeats;
        _randomNumberGenerator = random;
    }

    public string GenerateTarget()
    {
        string target;

        if (_allowRepeats)
            target = GenerateTargetRepeatsAllowed();
        else
            target = GenerateTargetNoRepeats();

        return target;
    }

    private string GenerateTargetNoRepeats()
    {
        var target = "";
        string randomDigit;
        var maxValue = _maxRange + 1; // Adjust for inclusive upper bound

        for (int i = 0; i < _targetLength; i++)
        {
            do
            {
                randomDigit = _randomNumberGenerator.Next(maxValue).ToString();
            } while (target.Contains(randomDigit));

            target += randomDigit;
        }

        return target;
    }

    private string GenerateTargetRepeatsAllowed()
    {
        var target = "";
        var maxValue = _maxRange + 1; // Adjust for inclusive upper bound

        for (int i = 0; i < _targetLength; i++)
        {
            target += _randomNumberGenerator.Next(maxValue).ToString();
        }
        return target;
    }
}