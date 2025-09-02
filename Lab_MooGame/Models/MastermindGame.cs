using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Services;

namespace Lab_MooGame.Models;
public class MastermindGame : IGuessingGame
{
    public string Name => "Mastermind Game";
    public string Description => "A game where you guess a 4-digit number (in the range 1-6) with the possibility of repeating digits. " +
                                 "You get feedback in the form of 'B' for correct number and position " +
                                 "and 'W' for correct number but wrong position.";
    private readonly ITargetGenerator _targetGenerator;
    private string _target = "";
    public string Target => _target == "" ? throw new InvalidOperationException("Target is not set. Call SetUpNewGame first.") : _target;
    private int _numberOfGuesses;
    public int NumberOfGuesses => _numberOfGuesses;

    public MastermindGame(ITargetGenerator targetGenerator)
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
        var numberOfBlack = 0;
        var numberOfWhite = 0;

        guess = EnsureCorrectLength(guess);

        for (int i = 0; i < _target.Length; i++)
        {
            if (IsInTargetAndCorrectPosition(i, guess!))
                numberOfBlack++;
            else if (IsInTarget(guess![i]))
                numberOfWhite++;
        }

        var result = $"{new string('B', numberOfBlack)},{new string('W', numberOfWhite)}";

        return result;
    }

    private string? EnsureCorrectLength(string? guess)
    {
        if (IsCorrectLength(guess!))
            guess = guess!.PadRight(_target.Length);

        return guess;
    }

    private bool IsCorrectLength(string guess) => guess.Length < _target.Length;

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