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
        var continuePlaying = true;
        _userInterface.WriteLine("Enter your user name:\n");
        string? userName = _userInterface.ReadLine();

        do
        {
            _guessingGame.PlayGame(userName!);

            continuePlaying = PromptForContinue();
        } while (continuePlaying);
    }

    private bool PromptForContinue()
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