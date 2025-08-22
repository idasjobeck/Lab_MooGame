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
    public int TargetLength { get; }
    public IRandom RandomNumberGenerator { get; }

    public MooTargetGenerator()
    {
        _targetLength = 4; // Default target length
        RandomNumberGenerator = new SystemRandom(); // Default random number generator
    }

    public MooTargetGenerator(IRandom random)
    {
        _targetLength = 4; // Default target length
        RandomNumberGenerator = random;
    }

    public MooTargetGenerator(int targetLength, IRandom random)
    {
        _targetLength = targetLength;
        RandomNumberGenerator = random;
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