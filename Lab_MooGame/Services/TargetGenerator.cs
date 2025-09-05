using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;

namespace Lab_MooGame.Services;

/// <summary>
/// Generates random numeric targets based on specified constraints, such as length, range, and whether repeats are
/// allowed.
/// </summary>
/// <remarks>This class provides functionality to generate random numeric strings of a specified length, within a
/// given range,  and with optional control over whether digits can repeat. It supports dependency injection of a custom
/// random number  generator via the <see cref="IRandom"/> interface, or defaults to using a system-provided random
/// number generator.</remarks>

public class TargetGenerator : ITargetGenerator
{
    private readonly IRandom _randomNumberGenerator;
    private readonly int _targetLength;
    public int TargetLength => _targetLength;
    private const int MinRange = 1;
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
        var maxValue = _maxRange + 1; // Adjust for exclusive upper bound

        for (int i = 0; i < _targetLength; i++)
        {
            do
            {
                randomDigit = _randomNumberGenerator.Next(MinRange, maxValue).ToString();
            } while (target.Contains(randomDigit));

            target += randomDigit;
        }

        return target;
    }

    private string GenerateTargetRepeatsAllowed()
    {
        var target = "";
        var maxValue = _maxRange + 1; // Adjust for exclusive upper bound

        for (int i = 0; i < _targetLength; i++)
        {
            target += _randomNumberGenerator.Next(MinRange, maxValue).ToString();
        }
        return target;
    }
}