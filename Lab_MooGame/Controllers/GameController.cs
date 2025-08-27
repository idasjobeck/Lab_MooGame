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
        _userInterface.WriteLine("Enter your user name:\n");
        _currentGameUserScore.UserName = _userInterface.ReadLine() ?? "";

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
        _userInterface.WriteLine("New game:\n");
        
        if (IsPracticeMode)
            _userInterface.WriteLine($"For practice, number is: {_guessingGame.Target} \n");
    }

    private void PlayGame()
    {
        string result;

        do
        {
            string? guess = _userInterface.ReadLine();
            result = _guessingGame.CheckGuess(guess);
            _userInterface.WriteLine($"{result}\n");
        } while (!_guessingGame.IsGuessCorrect(result));

        _currentGameUserScore.NumberOfGuesses = _guessingGame.NumberOfGuesses;

        var guessesWording = _guessingGame.NumberOfGuesses == 1 ? "guess" : "guesses";

        _userInterface.WriteLine($"Correct, it took {_guessingGame.NumberOfGuesses} {guessesWording}");
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
            _userInterface.WriteLine("Continue? (y/n)");
            string? answer = _userInterface.ReadLine()?.ToLower();

            if (answer == "y" || answer == "n")
                return answer == "y";
        } while (true);
    }

    private void DisplayScoreBoard()
    {
        var results = _scoreboardService.GetTopScores();
        if (results.Count == 0)
        {
            _userInterface.WriteLine("No results yet.\n");
            return;
        }

        _userInterface.WriteLine("Player   games  average");
        foreach (var player in results)
        {
            _userInterface.WriteLine($"{player.UserName,-9}{player.NumberOfGames,5:D}{player.Average(),9:F2}");
        }
    }
}