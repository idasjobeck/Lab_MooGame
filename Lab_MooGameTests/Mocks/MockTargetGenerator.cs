using Lab_MooGame.Models;
using Lab_MooGame.Services;

namespace Lab_MooGameTests.Mocks;

/// <summary>
/// Provides a mock implementation of the <see cref="ITargetGenerator"/> interface for generating predefined target
/// strings based on specified parameters.
/// </summary>
/// <remarks>This class is primarily intended for testing or demonstration purposes. It generates target strings
/// using a fixed pattern, either allowing or disallowing repeated characters, depending on the configuration. The
/// generated targets do not rely on randomness, even though the interface requires a random number generator.</remarks>

public class MockTargetGenerator : ITargetGenerator
{
    private readonly int _targetLength;
    public int TargetLength => _targetLength;
    private readonly int _maxRange;
    public int MaxRange => _maxRange;
    private readonly bool _allowRepeats;
    public bool AllowRepeats => _allowRepeats;
    public IRandom RandomNumberGenerator { get; }

    public MockTargetGenerator(int targetLength, int maxRange, bool allowRepeats)
    {
        _targetLength = targetLength;
        _maxRange = maxRange;
        _allowRepeats = allowRepeats;
        RandomNumberGenerator = new SystemRandom(); // Required by interface, but not used in this mock
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

    private string GenerateTargetRepeatsAllowed()
    {
        var target = "";

        for (int i = 0; i < _targetLength; i++)
        {
            target += i%2 == 0 ? "1" : "2";
        }

        return target;
    }

    private string GenerateTargetNoRepeats()
    {
        var target = "";

        for (int i = 0; i < _targetLength; i++)
        {
            target += (i + 1).ToString();
        }

        return target;
    }
}