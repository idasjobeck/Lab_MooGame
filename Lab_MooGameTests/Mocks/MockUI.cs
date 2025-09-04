using System.Diagnostics;
using Lab_MooGame.UI;

namespace Lab_MooGameTests.Mocks;

public class MockUI : IUserInterface
{
    public List<string> OutputMessages { get; } = new();
    private List<string> _userInputs = new();
    private int _currentInputIndex = -1;

    public MockUI(string inputs)
    {
        _userInputs = inputs.Split(",").ToList();
    }

    public string Read()
    {
        _currentInputIndex++;
        Debug.WriteLine($"<User input: {_userInputs[_currentInputIndex]}>");
        return _userInputs[_currentInputIndex];
    }
    public void Write(string message)
    {
        Debug.WriteLine(message);
        OutputMessages.Add(message);
    }

    public string Output => string.Join(",", OutputMessages);

    public void Clear()
    {
        OutputMessages.Clear();
    }
}