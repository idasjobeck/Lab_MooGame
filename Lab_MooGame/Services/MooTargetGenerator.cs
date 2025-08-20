using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_MooGame.Services;

public class MooTargetGenerator : ITargetGenerator
{
    private readonly int _targetLength;
    public int TargetLength { get; }

    public MooTargetGenerator()
    {
        _targetLength = 4; // Default target length
    }

    public MooTargetGenerator(int targetLength)
    {
        _targetLength = targetLength;
    }

    public string GenerateTarget()
    {
        var randomNumberGenerator = new Random();
        var target = "";

        for (int i = 0; i < _targetLength; i++)
        {
            var randomDigit = randomNumberGenerator.Next(10).ToString();

            while (target.Contains(randomDigit))
            {
                randomDigit = randomNumberGenerator.Next(10).ToString();
            }

            target += randomDigit;
        }

        return target;
    }
}