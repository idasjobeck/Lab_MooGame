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
    [DataRow("TestUser,1234,n")]
    [DataRow("TestUser,4321,1234,n")]
    [DataRow("Ida,1234,y,1234,n")]
    [DataRow("Ida,2143,1234,y,2134,1234,n")]
    public void Run_ShouldCompleteGameSuccessfullyWithDefaultTargetLength(string userInputs)
    {
        // Arrange
        var userInterface = new MockUI(userInputs);
        var targetGenerator = new MockTargetGenerator();
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
    [DataRow(6, "TestUser,123456,n")]
    [DataRow(5, "TestUser,54321,12345,n")]
    [DataRow(3, "Ida,123,y,123,n")]
    [DataRow(3, "Ida,213,123,y,134,123,n")]
    public void Run_ShouldCompleteGameSuccessfullyWithSpecifiedTargetLength(int targetLength, string userInputs)
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
}