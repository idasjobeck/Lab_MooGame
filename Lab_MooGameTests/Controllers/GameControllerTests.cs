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
    [DataRow(4, 9, false, "TestUser,y,1234,n")]
    [DataRow(4, 9, false, "TestUser,y,4321,1234,n")]
    [DataRow(4, 9, false, "Ida,y,1234,y,1234,n")]
    [DataRow(4, 9, false, "Ida,y,2143,1234,y,2134,1234,n")]
    [DataRow(4, 9, false, "Bob,n,12,34,1234,n")]
    [DataRow(4, 9, false, "Bob,n,543216,1234,n")]
    [DataRow(4, 9, false, "Bob,n,4321,1234,b,n")]
    [DataRow(4, 9, false, ",TestUser2,n,1234,n")]
    public void Run_ShouldCompleteGameSuccessfully(int targetLength, int maxRange, bool allowRepeats, string userInputs)
    {
        // Arrange
        var userInterface = new MockUI(userInputs);
        var targetGenerator = new MockTargetGenerator(targetLength, maxRange, allowRepeats);
        var guessingGame = new MooGame(targetGenerator);
        var scoreboardService = new ScoreboardService(new TextFileDataStorage("test_highscores.txt"));
        var gameController = new GameController(userInterface, guessingGame, scoreboardService);

        // Act
        gameController.Run();

        // Assert
        StringAssert.Contains(userInterface.Output, "Correct");
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void Run_UsernameShouldHaveBeenRequestedMultipleTimes()
    {
        // Arrange
        var userInterface = new MockUI(",TestUser2,y,1234,n");
        var targetLength = 4;
        var maxRange = 9;
        var allowRepeats = false;
        var targetGenerator = new MockTargetGenerator(targetLength, maxRange, allowRepeats);
        var guessingGame = new MooGame(targetGenerator);
        var scoreboardService = new ScoreboardService(new MockDataStorage());
        var gameController = new GameController(userInterface, guessingGame, scoreboardService);
        var substring = "Enter your user name:";
        var expectedOccurrences = 2;

        // Act
        gameController.Run();

        // Assert
        if (string.IsNullOrEmpty(substring) || userInterface.Output.Length < substring.Length)
            Assert.Fail();
        
        var actualOccurrences = userInterface.Output.Select((_, i) => userInterface.Output.Substring(i))
            .Count(s => s.StartsWith(substring, StringComparison.Ordinal));

        Assert.AreEqual(expectedOccurrences, actualOccurrences);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void Run_PracticeModeSelectionShouldHaveBeenRequestedMultipleTimes()
    {
        // Arrange
        var userInterface = new MockUI(",TestUser2,t,y,1234,n");
        var targetLength = 4;
        var maxRange = 9;
        var allowRepeats = false;
        var targetGenerator = new MockTargetGenerator(targetLength, maxRange, allowRepeats);
        var guessingGame = new MooGame(targetGenerator);
        var scoreboardService = new ScoreboardService(new MockDataStorage());
        var gameController = new GameController(userInterface, guessingGame, scoreboardService);
        var substring = "\nDo you want to play in Practice Mode, whereby the target is visible? (y/n)";
        var expectedOccurrences = 2;

        // Act
        gameController.Run();

        // Assert
        if (string.IsNullOrEmpty(substring) || userInterface.Output.Length < substring.Length)
            Assert.Fail();

        var actualOccurrences = userInterface.Output.Select((_, i) => userInterface.Output.Substring(i))
            .Count(s => s.StartsWith(substring, StringComparison.Ordinal));

        Assert.AreEqual(expectedOccurrences, actualOccurrences);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void Run_ContinueMessageShouldHaveBeenDisplayedMultipleTimes()
    {
        // Arrange
        var userInterface = new MockUI("TestUser,y,1234,b,h,n");
        var targetLength = 4;
        var maxRange = 9;
        var allowRepeats = false;
        var targetGenerator = new MockTargetGenerator(targetLength, maxRange, allowRepeats);
        var guessingGame = new MooGame(targetGenerator);
        var scoreboardService = new ScoreboardService(new MockDataStorage());
        var gameController = new GameController(userInterface, guessingGame, scoreboardService);
        var substring = "Continue? (y/n)";
        var expectedOccurrences = 3;

        // Act
        gameController.Run();

        // Assert
        if (string.IsNullOrEmpty(substring) || userInterface.Output.Length < substring.Length)
            Assert.Fail();

        var actualOccurrences = userInterface.Output.Select((_, i) => userInterface.Output.Substring(i))
            .Count(s => s.StartsWith(substring, StringComparison.Ordinal));

        Assert.AreEqual(expectedOccurrences, actualOccurrences);
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(4, 9, false, "TestUser,y,,1234,n")]
    [DataRow(4, 9, false, "TestUser,y,abcd,1234,n")]
    [DataRow(4, 9, false, "TestUser,y,4321,abcd,1234,n")]
    [DataRow(4, 9, false, "TestUser,n,ab12,1234,n")]
    [DataRow(4, 9, false, "TestUser,n,12ab,1234,n")]
    public void Run_ErrorMessageShouldHaveDisplayedForNonNumericalUserInput(int targetLength, int maxRange, bool allowRepeats, string userInputs)
    {
        // Arrange
        var userInterface = new MockUI(userInputs);
        var targetGenerator = new MockTargetGenerator(targetLength, maxRange, allowRepeats);
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
    [DataRow(4, 9, false, "TestUser,y,abcd,1234,n", 1)]
    [DataRow(4, 9, false, "TestUser,n,4321,abcd,1234,n", 2)]
    [DataRow(4, 9, false, "TestUser,y,abc1,2143,4231,ab12,1234,n", 3)]
    [DataRow(4, 9, false, "TestUser,n,4132,12ab,3142,4321,ab12,1234,n", 4)]
    public void Run_NonNumericalUserInputForGuessesShouldNotHaveCounted(int targetLength, int maxRange, bool allowRepeats, string userInputs, int expectedNumberOfGuesses)
    {
        // Arrange
        var userInterface = new MockUI(userInputs);
        var targetGenerator = new MockTargetGenerator(targetLength, maxRange, allowRepeats);
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
    [DataRow(4, 9, false, "TestUser,y,1234,n")]
    [DataRow(4, 9, false, "Ida,n,1234,y,1234,n")]
    public void Run_ShouldThrowExceptionWhenTwoRunsAreAccessingHighscoresFileSimultaneously(int targetLength, int maxRange, bool allowRepeats, string userInputs)
    {
        // Arrange
        var userInterface = new MockUI(userInputs);
        var targetGenerator = new MockTargetGenerator(targetLength, maxRange, allowRepeats);
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

    [TestMethod]
    [TestCategory("Unit")]
    public void Run_ShouldDisplayErrorMessageIfTopScoresIsEmpty()
    {
        // Arrange
        var userInterface = new MockUI("TestUser,n,1234,n");
        var targetLength = 4;
        var maxRange = 9;
        var allowRepeats = false;
        var targetGenerator = new MockTargetGenerator(targetLength, maxRange, allowRepeats);
        var guessingGame = new MooGame(targetGenerator);
        var scoreboardService = new MockScoreboardService(new MockDataStorage());
        var gameController = new GameController(userInterface, guessingGame, scoreboardService);

        // Act
        gameController.Run();

        // Assert
        StringAssert.Contains(userInterface.Output, "No results yet.");
    }
}