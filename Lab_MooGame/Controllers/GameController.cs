using Lab_MooGame.Models;
using Lab_MooGame.Services;
using Lab_MooGame.UI;

namespace Lab_MooGame.Controllers;

/// <summary>
/// Manages the flow of a guessing game, including user interaction, game setup, and score tracking.
/// </summary>
/// <remarks>The <see cref="GameController"/> class coordinates the main gameplay loop, including displaying game
/// information,  handling user input, managing practice mode, and updating the scoreboard. It relies on injected
/// dependencies  for user interface interactions, game logic, and scoreboard management.  This class is designed to be
/// the entry point for running the game. It ensures that the game progresses  through its various stages, such as
/// setting up a new game, processing user guesses, and displaying results.</remarks>

public class GameController
{
    private readonly IUserInterface _userInterface;
    private readonly IGuessingGame _guessingGame;
    private readonly IScoreboard _scoreboardService;
    private bool _isPracticeMode = true; // Set to false for real games
    private CurrentGameUserScore _currentGameUserScore = new();

    public GameController(IUserInterface userInterface, IGuessingGame guessingGame, IScoreboard scoreboardService)
    {
        _userInterface = userInterface;
        _guessingGame = guessingGame;
        _scoreboardService = scoreboardService;
    }

    public void Run()
    {
        DisplayGameInformation();
        RequestUsername();
        RequestPracticeModeSelection();

        do
        {
            _guessingGame.SetUpNewGame();
            DisplayInstructions();
            PlayGame();
            UpdateAndDisplayScoreBoard();
        } while (ContinuePlayingPrompt());
    }

    private void RequestUsername()
    {
        string? userName;

        do
        {
            _userInterface.Write("Enter your user name:");
            userName = _userInterface.Read();
        } while (string.IsNullOrEmpty(userName));

        _currentGameUserScore.UserName = userName;
    }

    private void RequestPracticeModeSelection()
    {
        do
        {
            _userInterface.Write("\nDo you want to play in Practice Mode, whereby the target is visible? (y/n)");
            string? answer = _userInterface.Read()?.ToLower();

            if (answer == "y" || answer == "n")
            {
                _isPracticeMode = answer == "y";
                return;
            }
        } while (true);
    }

    private void DisplayGameInformation()
    {
        _userInterface.Write($"\n{_guessingGame.Name}");
        _userInterface.Write($"{_guessingGame.Description}\n");
    }

    private void DisplayInstructions()
    {
        _userInterface.Write("\nNew game:\n");
        
        if (_isPracticeMode)
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
        var topScores = _scoreboardService.GetTopScores();
        if (topScores.Count == 0)
        {
            _userInterface.Write("No results yet.\n");
            return;
        }

        _userInterface.Write("Player   games  average");
        foreach (var player in topScores)
        {
            _userInterface.Write($"{player.UserName,-9}{player.NumberOfGames,5:D}{player.AverageNumberOfGuesses(),9:F2}");
        }
    }
}