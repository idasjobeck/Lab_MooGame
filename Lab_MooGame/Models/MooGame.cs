using Lab_MooGame.Services;
using Lab_MooGame.UI;

namespace Lab_MooGame.Models;

public class MooGame : IGuessingGame
{
    public string Name => "Moo Game";
    public string Description => "A game where you guess a 4-digit number with no repeating digits. " +
                                 "You get feedback in the form of 'B' for bulls (correct digit and position) " +
                                 "and 'C' for cows (correct digit but wrong position).";
    private readonly ITargetGenerator _targetGenerator;
    private string _target = "";
    public string Target => _target ?? throw new InvalidOperationException("Target is not set. Call SetUpNewGame first.");
    private int _numberOfGuesses;
    public int NumberOfGuesses => _numberOfGuesses;

    public MooGame(ITargetGenerator targetGenerator)
    {
        _targetGenerator = targetGenerator ?? throw new ArgumentNullException(nameof(targetGenerator));
    }

    public void SetUpNewGame()
    {
        _target = _targetGenerator.GenerateTarget();
        _numberOfGuesses = 0;
    }

    public string CheckGuess(string? guess)
    {
        _numberOfGuesses++;
        var numberOfCows = 0;
        var numberOfBulls = 0;

        guess = EnsureCorrectLength(guess);

        for (int i = 0; i < _target.Length; i++)
        {
            if (IsInTargetAndCorrectPosition(i, guess!))
                numberOfBulls++;
            else if (IsInTarget(guess![i]))
                numberOfCows++;
        }
        
        var result = $"{new string('B', numberOfBulls)},{new string('C', numberOfCows)}";

        return result;
    }

    private string? EnsureCorrectLength(string? guess)
    {
        if (IsCorrectLength(guess!))
            guess = guess!.PadRight(_target.Length);

        return guess;
    }

    private bool IsCorrectLength(string guess) => string.IsNullOrEmpty(guess) || guess.Length < _target.Length;

    private bool IsInTargetAndCorrectPosition(int position, string guess) => _target[position] == guess[position];

    private bool IsInTarget(char guess) => _target.Contains(guess);

    public bool IsGuessCorrect(string resultToCheck)
    {
        if (string.IsNullOrEmpty(resultToCheck))
            return false;

        var correctResult = $"{new string('B', _target.Length)},";

        return resultToCheck == correctResult;
    }
}