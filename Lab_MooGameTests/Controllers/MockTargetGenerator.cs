using Lab_MooGame.Services;

namespace Lab_MooGame.Controllers.Tests;

public class MockTargetGenerator : ITargetGenerator
{
    private readonly int _targetLength;
    public int TargetLength { get; }

    public MockTargetGenerator()
    {
        _targetLength = 4; // Fixed length for testing
    }

    public MockTargetGenerator(int targetLength)
    {
        _targetLength = targetLength;
    }

    public string GenerateTarget()
    {
        // Return a fixed target for testing purposes
        return "1234";
    }
}