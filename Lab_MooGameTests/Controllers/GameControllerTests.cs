using Lab_MooGame.Controllers;
using Lab_MooGame.Models;
using Lab_MooGame.Services;
using Lab_MooGameTests.Mocks;

namespace Lab_MooGameTests.Controllers;

[TestClass]
public class GameControllerTests
{
    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(4, "TestUser,1234,n")]
    [DataRow(4, "TestUser,4321,1234,n")]
    [DataRow(4, "Ida,1234,y,1234,n")]
    [DataRow(4, "Ida,2143,1234,y,2134,1234,n")]
    [DataRow(4, "Bob,12,34,1234,n")]
    [DataRow(4, "Bob,543216,1234,n")]
    [DataRow(4, "Bob,4321,1234,b,n")]
    [DataRow(6, "TestUser,123456,n")]
    [DataRow(5, "TestUser,54321,12345,n")]
    [DataRow(3, "Ida,123,y,123,n")]
    [DataRow(3, "Ida,213,123,y,134,123,n")]
    [DataRow(6, "Bob,12,34,56,123456,n")]
    [DataRow(5, "Bob,543216,12345,n")]
    public void Run_ShouldCompleteGameSuccessfully(int targetLength, string userInputs)
    {
        // Arrange
        var userInterface = new MockUI(userInputs);
        var targetGenerator = new MockTargetGenerator(targetLength);
        var guessingGame = new MooGame(targetGenerator);
        var scoreboardService = new ScoreboardService(new TextFileDataStorage("test_highscores.txt"));
        var gameController = new GameController(userInterface, guessingGame, scoreboardService);

        // Act
        gameController.Run();

        // Assert
        StringAssert.Contains(userInterface.Output, "Correct");
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(4, "TestUser,,1234,n")]
    [DataRow(4, "TestUser,abcd,1234,n")]
    [DataRow(4, "TestUser,4321,abcd,1234,n")]
    [DataRow(4, "TestUser,ab12,1234,n")]
    [DataRow(4, "TestUser,12ab,1234,n")]
    [DataRow(6, "TestUser,abcd,123456,n")]
    [DataRow(6, "TestUser,654321,abcd,123456,n")]
    [DataRow(6, "TestUser,abc123,123456,n")]
    [DataRow(6, "TestUser,123abc,123456,n")]
    public void Run_ErrorMessageShouldHaveDisplayedForNonNumericalUserInput(int targetLength, string userInputs)
    {
        // Arrange
        var userInterface = new MockUI(userInputs);
        var targetGenerator = new MockTargetGenerator(targetLength);
        var guessingGame = new MooGame(targetGenerator);
        var scoreboardService = new ScoreboardService(new MockDataStorage());
        var gameController = new GameController(userInterface, guessingGame, scoreboardService);

        // Act
        gameController.Run();

        // Assert
        StringAssert.Contains(userInterface.Output, "Invalid input, please enter numbers only.");
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(4, "TestUser,abcd,1234,n", 1)]
    [DataRow(4, "TestUser,4321,abcd,1234,n", 2)]
    [DataRow(4, "TestUser,abc1,2143,4231,ab12,1234,n", 3)]
    [DataRow(4, "TestUser,4132,12ab,3142,4321,ab12,1234,n", 4)]
    [DataRow(6, "TestUser,abcd,123456,n", 1)]
    [DataRow(6, "TestUser,654321,abcd,123456,n", 2)]
    [DataRow(6, "TestUser,456123,abc123,654321,a12b34,123456,n", 3)]
    [DataRow(6, "TestUser,123654,123abc,ab1234,142536,654321,123456,n", 4)]
    public void Run_NonNumericalUserInputForGuessesShouldNotHaveCounted(int targetLength, string userInputs, int expectedNumberOfGuesses)
    {
        // Arrange
        var userInterface = new MockUI(userInputs);
        var targetGenerator = new MockTargetGenerator(targetLength);
        var guessingGame = new MooGame(targetGenerator);
        var scoreboardService = new ScoreboardService(new MockDataStorage());
        var gameController = new GameController(userInterface, guessingGame, scoreboardService);

        // Act
        gameController.Run();

        // Assert
        Assert.AreEqual(expectedNumberOfGuesses, guessingGame.NumberOfGuesses);
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(4, "TestUser,1234,n")]
    [DataRow(4, "Ida,1234,y,1234,n")]
    public void Run_ShouldThrowExceptionWhenTwoRunsAreAccessingHighscoresFileSimultaneously(int targetLength, string userInputs)
    {
        // Arrange
        var userInterface = new MockUI(userInputs);
        var targetGenerator = new MockTargetGenerator(targetLength);
        var guessingGame = new MooGame(targetGenerator);
        var scoreboardService = new ScoreboardService(new TextFileDataStorage("test_highscores.txt"));
        var gameController = new GameController(userInterface, guessingGame, scoreboardService);

        using (StreamReader fileLock = new StreamReader("test_highscores.txt"))
        {
            // The file is now locked for reading, simulating another process accessing it.
            // Act & Assert
            Assert.ThrowsException<IOException>(() => gameController.Run());
        }
    }
}