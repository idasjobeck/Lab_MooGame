using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;

namespace Lab_MooGameTests.Mocks;

public class MockRandom : IRandom
{
    private readonly Queue<int> _predefinedValues;

    public MockRandom(Queue<int> mockRandomValues)
    {
        _predefinedValues = mockRandomValues;
    }
    public int Next(int maxValue)
    {
        return _predefinedValues.Count > 0 ? _predefinedValues.Dequeue() : throw new InvalidOperationException("No more predefined values available.");
    }
}