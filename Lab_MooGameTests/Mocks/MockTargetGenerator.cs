using Lab_MooGame.Models;
using Lab_MooGame.Services;

namespace Lab_MooGameTests.Mocks;

public class MockTargetGenerator : ITargetGenerator
{
    private readonly int _targetLength;
    public int TargetLength { get; }
    public IRandom RandomNumberGenerator { get; }

    public MockTargetGenerator()
    {
        _targetLength = 4; // Fixed length for testing
        RandomNumberGenerator = new SystemRandom(); // Required by interface, but not used in this mock
    }

    public string GenerateTarget()
    {
        // Return a fixed target for testing purposes
        return "1234";
    }
}