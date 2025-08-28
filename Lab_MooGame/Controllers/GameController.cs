using Lab_MooGame.Models;
using Lab_MooGame.Services;
using Lab_MooGame.UI;

namespace Lab_MooGame.Controllers;

public class GameController
{
    private readonly IUserInterface _userInterface;
    private readonly IGuessingGame _guessingGame;
    private readonly ScoreboardService _scoreboardService;
    private const bool IsPracticeMode = true; // Set to false for real games
    private CurrentGameUserScore _currentGameUserScore = new();

    public GameController(IUserInterface userInterface, IGuessingGame guessingGame, ScoreboardService scoreboardService)
    {
        _userInterface = userInterface;
        _guessingGame = guessingGame;
        _scoreboardService = scoreboardService;
    }

    public void Run()
    {
        _userInterface.Write("Enter your user name:\n");
        _currentGameUserScore.UserName = _userInterface.Read() ?? "";

        do
        {
            _guessingGame.SetUpNewGame();
            DisplayInstructions();
            PlayGame();
            UpdateAndDisplayScoreBoard();
        } while (ContinuePlayingPrompt());
    }

    private void DisplayInstructions()
    {
        _userInterface.Write("New game:\n");
        
        if (IsPracticeMode)
            _userInterface.Write($"For practice, number is: {_guessingGame.Target} \n");
    }

    private void PlayGame()
    {
        string result;
        string? guess;

        do
        {
            do
            {
                guess = _userInterface.Read();
                if(!IsNumbers(guess))
                    _userInterface.Write("Invalid input, please enter numbers only.\n");
            } while (!IsNumbers(guess));

            result = _guessingGame.CheckGuess(guess);
            _userInterface.Write($"{result}\n");
        } while (!_guessingGame.IsGuessCorrect(result));

        _currentGameUserScore.NumberOfGuesses = _guessingGame.NumberOfGuesses;

        var guessesWording = _guessingGame.NumberOfGuesses == 1 ? "guess" : "guesses";

        _userInterface.Write($"Correct, it took {_guessingGame.NumberOfGuesses} {guessesWording}");
    }

    private void UpdateAndDisplayScoreBoard()
    {
        _scoreboardService.UpdateScoreBoard(_currentGameUserScore);
        DisplayScoreBoard();
    }

    private bool ContinuePlayingPrompt()
    {
        do
        {
            _userInterface.Write("Continue? (y/n)");
            string? answer = _userInterface.Read()?.ToLower();

            if (answer == "y" || answer == "n")
                return answer == "y";
        } while (true);
    }

    private bool IsNumbers(string? userInput)
    {
        if (string.IsNullOrEmpty(userInput))
            return false;

        foreach (char c in userInput)
        {
            if (!char.IsDigit(c))
                return false;
        }
        return true;
    }

    private void DisplayScoreBoard()
    {
        var results = _scoreboardService.GetTopScores();
        if (results.Count == 0)
        {
            _userInterface.Write("No results yet.\n");
            return;
        }

        _userInterface.Write("Player   games  average");
        foreach (var player in results)
        {
            _userInterface.Write($"{player.UserName,-9}{player.NumberOfGames,5:D}{player.AverageNumberOfGuesses(),9:F2}");
        }
    }
}