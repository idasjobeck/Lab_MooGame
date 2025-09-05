using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;

namespace Lab_MooGameTests.Mocks;

/// <summary>
/// Provides a mock implementation of the <see cref="IRandom"/> interface that generates predefined random numbers.
/// </summary>
/// <remarks>This class is useful for testing scenarios where deterministic random number generation is required.
/// The sequence of random numbers is defined by a queue of integers provided at construction time. If the queue is
/// exhausted, an <see cref="InvalidOperationException"/> is thrown.</remarks>

public class MockRandom : IRandom
{
    private readonly Queue<int> _predefinedValues;

    public MockRandom(Queue<int> mockRandomValues)
    {
        _predefinedValues = mockRandomValues;
    }
    public int Next(int maxValue)
    {
        var mockRandomNumber = _predefinedValues.Count > 0 ? _predefinedValues.Dequeue() : throw new InvalidOperationException("No more predefined values available.");
        
        if (mockRandomNumber >= maxValue)
            throw new ArgumentOutOfRangeException($"Mock random number {mockRandomNumber} is out of range for maxValue {maxValue}.");
        
        if (mockRandomNumber < 0)
            throw new ArgumentOutOfRangeException($"Mock random number {mockRandomNumber} is out of range for minValue 0.");

        return mockRandomNumber;
    }

    public int Next(int minValue, int maxValue)
    {
        var mockRandomNumber = _predefinedValues.Count > 0 ? _predefinedValues.Dequeue() : throw new InvalidOperationException("No more predefined values available.");
        
        if (mockRandomNumber >= maxValue || mockRandomNumber < minValue)
            throw new ArgumentOutOfRangeException($"Mock random number {mockRandomNumber} is out of range for minValue {minValue} and maxValue {maxValue}.");
        
        return mockRandomNumber;
    }

    public int Next()
    {
        var mockRandomNumber = _predefinedValues.Count > 0 ? _predefinedValues.Dequeue() : throw new InvalidOperationException("No more predefined values available.");
        
        if (mockRandomNumber < 0 || mockRandomNumber == int.MaxValue)
            throw new ArgumentOutOfRangeException($"Mock random number {mockRandomNumber} is out of range for minValue 0 and maxValue {int.MaxValue}.");

        return mockRandomNumber;
    }
}