using Lab_MooGame.Models;
using Lab_MooGame.Services;
using Lab_MooGame.UI;

namespace Lab_MooGame.Controllers;

class GameController
{
    private readonly IUserInterface _userInterface;
    private readonly IGuessingGame _guessingGame;
    private readonly ScoreboardService _scoreboardService;

    public GameController(IUserInterface userInterface, IGuessingGame guessingGame, ScoreboardService scoreboardService)
    {
        _userInterface = userInterface;
        _guessingGame = guessingGame;
        _scoreboardService = scoreboardService;
    }

    public void Run()
    {
        _userInterface.WriteLine("Enter your user name:\n");
        string? userName = _userInterface.ReadLine();

        do
        {
            _guessingGame.SetUpNewGame();
            DisplayInstructions();
            PlayGame();
            UpdateAndDisplayScoreBoard(userName);
        } while (ContinuePlayingPrompt());
    }

    private void DisplayInstructions()
    {
        _userInterface.WriteLine("New game:\n");
        //comment out or remove next line to play real games!
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

        _userInterface.WriteLine($"Correct, it took {_guessingGame.NumberOfGuesses} guesses");
    }

    private void UpdateAndDisplayScoreBoard(string? userName)
    {
        _scoreboardService.UpdateScoreBoard(userName!, _guessingGame.NumberOfGuesses);
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