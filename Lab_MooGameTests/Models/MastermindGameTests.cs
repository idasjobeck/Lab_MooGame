using Lab_MooGame.Models;
using Lab_MooGame.Services;
using Lab_MooGameTests.Mocks;

namespace Lab_MooGameTests.Models;
[TestClass]
public class MastermindGameTests
{
    private const bool AllowRepeats = true;
    private const int MaxRange = 6;

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(4, new int[] { 1, 2, 1, 3 }, "1213")]
    [DataRow(4, new int[] { 4, 5, 6, 4 }, "4564")]
    [DataRow(4, new int[] { 1, 1, 1, 2 }, "1112")]
    [DataRow(4, new int[] { 1, 2, 3, 4 }, "1234")]
    public void SetUpNewGame_ShouldSetGeneratedTargetAndSetGuessesToZero(int targetLength, IEnumerable<int> predefinedNumbers, string expectedTarget)
    {
        // Arrange
        var mockRandomNumbers = new Queue<int>(predefinedNumbers);
        var mockRandomNumberGenerator = new MockRandom(mockRandomNumbers);
        var targetGenerator = new TargetGenerator(targetLength, MaxRange, AllowRepeats, mockRandomNumberGenerator);
        var mastermindGame = new MastermindGame(targetGenerator);
        var expectedNumberOfGuesses = 0;

        // Act
        mastermindGame.SetUpNewGame();

        // Assert
        Assert.AreEqual(expectedTarget, mastermindGame.Target);
        Assert.AreEqual(expectedNumberOfGuesses, mastermindGame.NumberOfGuesses);
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(4, "1212", "BBBB,")]
    [DataRow(4, "2121", ",WWWW")]
    [DataRow(4, "5678", ",")]
    [DataRow(4, "1221", "BB,WW")]
    [DataRow(4, "2112", "BB,WW")]
    [DataRow(4, "1215", "BBB,")]
    [DataRow(4, "5672", "B,")]
    [DataRow(4, "5627", ",W")]
    [DataRow(4, "12", "BB,")]
    [DataRow(4, "21", ",WW")]
    [DataRow(4, "121256", "BBBB,")]
    [DataRow(4, "567812", ",")]
    public void CheckGuess_ShouldReturnResultBasedOnUserGuess(int targetLength, string userGuess, string expectedResult)
    {
        // Arrange
        var targetGenerator = new MockTargetGenerator(targetLength, MaxRange, AllowRepeats);
        var mastermindGame = new MastermindGame(targetGenerator);
        mastermindGame.SetUpNewGame();

        // Act
        var actualResult = mastermindGame.CheckGuess(userGuess);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void IsGuessCorrect_ShouldReturnTrue()
    {
        // Arrange
        var resultToCheck = "BBBB,";
        var targetLength = 4;
        var targetGenerator = new MockTargetGenerator(targetLength, MaxRange, AllowRepeats);
        var mastermindGame = new MastermindGame(targetGenerator);
        mastermindGame.SetUpNewGame();

        // Act
        var isCorrect = mastermindGame.IsGuessCorrect(resultToCheck);

        // Assert
        Assert.IsTrue(isCorrect);
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(4, ",")]
    [DataRow(4, null)]
    [DataRow(4, "")]
    [DataRow(4, ",CCCC")]
    [DataRow(4, ",CCC")]
    [DataRow(4, ",CC")]
    [DataRow(4, ",C")]
    [DataRow(4, "BBB,")]
    [DataRow(4, "BB,")]
    [DataRow(4, "B,")]
    [DataRow(4, "BBB,C")]
    [DataRow(4, "BB,CC")]
    [DataRow(4, "B,CCC")]
    [DataRow(4, "B,C")]
    [DataRow(4, "BB,C")]
    [DataRow(4, "B,CC")]
    public void IsGuessCorrect_ShouldReturnFalse(int targetLength, string resultToCheck)
    {
        // Arrange
        var targetGenerator = new MockTargetGenerator(targetLength, MaxRange, AllowRepeats);
        var mooGame = new MooGame(targetGenerator);
        mooGame.SetUpNewGame();

        // Act
        var isCorrect = mooGame.IsGuessCorrect(resultToCheck);

        // Assert
        Assert.IsFalse(isCorrect);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void MastermindGame_ShouldThrowArgumentNullException()
    {
        // Arrange, Act, and Assert
        Assert.ThrowsException<ArgumentNullException>(() => new MastermindGame(null!));
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void Target_ShouldThrowInvalidOperationExceptionIfGameNotSetUp()
    {
        // Arrange
        var targetLength = 4;
        var targetGenerator = new MockTargetGenerator(targetLength, MaxRange, AllowRepeats);
        var mastermindGame = new MastermindGame(targetGenerator);

        // Act & Assert
        Assert.ThrowsException<InvalidOperationException>(() => { var target = mastermindGame.Target; });
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void Name_ShouldGetName()
    {
        // Arrange
        var targetLength = 4;
        var targetGenerator = new MockTargetGenerator(targetLength, MaxRange, AllowRepeats);
        var mastermindGame = new MastermindGame(targetGenerator);
        var expectedName = "Mastermind Game";

        // Act
        var actualName = mastermindGame.Name;

        // Assert
        Assert.AreEqual(expectedName, actualName);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void Description_ShouldGetDescription()
    {
        // Arrange
        var targetLength = 4;
        var targetGenerator = new MockTargetGenerator(targetLength, MaxRange, AllowRepeats);
        var mastermindGame = new MastermindGame(targetGenerator);
        var expectedDescription = "A game where you guess a 4-digit number (in the range 1-6) with the possibility of repeating digits. " +
                                  "You get feedback in the form of 'B' for correct number and position " +
                                  "and 'W' for correct number but wrong position.";

        // Act
        var actualDescription = mastermindGame.Description;

        // Assert
        Assert.AreEqual(expectedDescription, actualDescription);
    }
}