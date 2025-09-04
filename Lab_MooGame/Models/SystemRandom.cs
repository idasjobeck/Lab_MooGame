using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_MooGame.Models;

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