namespace Lab_MooGame;

class GameController
{
    private readonly IUserInterface _userInterface;
    private readonly IGuessingGame _guessingGame;

    public GameController(IUserInterface userInterface, IGuessingGame guessingGame)
    {
        _userInterface = userInterface;
        _guessingGame = guessingGame;
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
        _guessingGame.WriteToScoreBoard(userName);
        _guessingGame.ShowScoreBoard();
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
}