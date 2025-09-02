using Lab_MooGame.Models;
using Lab_MooGame.Services;

namespace Lab_MooGameTests.Mocks;

public class MockTargetGenerator : ITargetGenerator
{
    private readonly int _targetLength;
    public int TargetLength { get; }
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
            target += "1";
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