using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_MooGame.Models;

/// <summary>
/// Provides a random number generator that produces pseudo-random numbers using the .NET <see cref="System.Random"/>
/// class.
/// </summary>
/// <remarks>This implementation wraps the <see cref="System.Random"/> class to provide random number generation
/// functionality. It supports generating random integers within a specified range or without bounds.</remarks>

public class SystemRandom : IRandom
{
    private readonly Random _random;

    public SystemRandom()
    {
        _random = new Random();
    }

    public int Next(int maxValue) => _random.Next(maxValue);

    public int Next(int minValue, int maxValue) => _random.Next(minValue, maxValue);

    public int Next() => _random.Next();
}