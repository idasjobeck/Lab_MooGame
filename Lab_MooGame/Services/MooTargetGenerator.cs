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

    public MooTargetGenerator(int targetLength)
    {
        _targetLength = targetLength;
        RandomNumberGenerator = new SystemRandom(); // Default random number generator
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