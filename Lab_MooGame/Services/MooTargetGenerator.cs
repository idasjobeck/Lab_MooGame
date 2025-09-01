using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;

namespace Lab_MooGame.Services;

public class MooTargetGenerator : ITargetGenerator
{
    private readonly int _targetLength;
    private readonly IRandom _randomNumberGenerator;
    public int TargetLength => _targetLength;
    public IRandom RandomNumberGenerator => _randomNumberGenerator;

    public MooTargetGenerator(int targetLength)
    {
        _targetLength = targetLength;
        _randomNumberGenerator = new SystemRandom(); // Default random number generator
    }

    public MooTargetGenerator(int targetLength, IRandom random)
    {
        _targetLength = targetLength;
        _randomNumberGenerator = random;
    }

    public string GenerateTarget()
    {
        var target = "";

        for (int i = 0; i < _targetLength; i++)
        {
            var randomDigit = RandomNumberGenerator.Next(10).ToString();

            while (target.Contains(randomDigit))
            {
                randomDigit = RandomNumberGenerator.Next(10).ToString();
            }

            target += randomDigit;
        }

        return target;
    }
}