using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab_MooGame.Models;
using Lab_MooGame.Services;
using Lab_MooGameTests.Mocks;

namespace Lab_MooGameTests.Models;

[TestClass]
public class MooGameTests
{
    private MockTargetGenerator _targetGenerator;
    private MooGame _mooGame;

    [TestInitialize]
    public void Setup()
    {
        _targetGenerator = new MockTargetGenerator();
        _mooGame = new MooGame(_targetGenerator);
        _mooGame.SetUpNewGame();
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(new int[] { 1, 2, 1, 3, 3, 4 }, "1234")]
    [DataRow(new int[] { 4, 5, 6, 4, 7, 8 }, "4567")]
    [DataRow(new int[] { 1, 1, 1, 2, 3, 4 }, "1234")]
    [DataRow(new int[] { 9, 8, 7, 6, 5, 4 }, "9876")]
    public void SetUpNewGame_ShouldSetUpNewGameWithTargetOfDefaultLengthAndZeroGuesses(IEnumerable<int> predefinedNumbers, string expectedTarget)
    {
        // Arrange
        var mockRandomNumbers = new Queue<int>(predefinedNumbers);
        var mockRandomNumberGenerator = new MockRandom(mockRandomNumbers);
        var targetGenerator = new MooTargetGenerator(mockRandomNumberGenerator);
        var mooGame = new MooGame(targetGenerator);
        var expectedNumberOfGuesses = 0;

        // Act
        mooGame.SetUpNewGame();

        // Assert
        Assert.AreEqual(expectedTarget, mooGame.Target);
        Assert.AreEqual(expectedNumberOfGuesses, mooGame.NumberOfGuesses);
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(6, new int[] { 1, 2, 1, 3, 4, 4, 5, 6 }, "123456")]
    [DataRow(6, new int[] { 7, 8, 9, 7, 6, 5, 4, 3 }, "789654")]
    [DataRow(5, new int[] { 1, 1, 2, 3, 4, 2, 5, 6 }, "12345")]
    [DataRow(3, new int[] { 7, 9, 9, 7, 6, 5, 2 }, "796")]
    public void SetUpNewGame_ShouldSetUpNewGameWithTargetOfSpecifiedLengthAndZeroGuesses(int targetLength, IEnumerable<int> predefinedNumbers, string expectedTarget)
    {
        // Arrange
        var mockRandomNumbers = new Queue<int>(predefinedNumbers);
        var mockRandomNumberGenerator = new MockRandom(mockRandomNumbers);
        var targetGenerator = new MooTargetGenerator(targetLength, mockRandomNumberGenerator);
        var mooGame = new MooGame(targetGenerator);
        var expectedNumberOfGuesses = 0;

        // Act
        mooGame.SetUpNewGame();

        // Assert
        Assert.AreEqual(expectedTarget, mooGame.Target);
        Assert.AreEqual(expectedNumberOfGuesses, mooGame.NumberOfGuesses);
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow("1234", "BBBB,")]
    [DataRow("4321", ",CCCC")]
    [DataRow("5678", ",")]
    [DataRow("1243", "BB,CC")]
    [DataRow("2134", "BB,CC")]
    [DataRow("1235", "BBB,")]
    [DataRow("5674", "B,")]
    [DataRow("5627", ",C")]
    [DataRow("12", "BB,")]
    [DataRow("34", ",CC")]
    [DataRow("123456", "BBBB,")]
    [DataRow("567812", ",")]
    public void CheckGuess_ShouldReturnResultBasedOnUserGuessWithTargetOfDefaultLength(string userGuess, string expectedResult)
    {
        // Arrange
        //using _mooGame from Setup()

        // Act
        var actualResult = _mooGame.CheckGuess(userGuess);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(6,"123456","BBBBBB,")]
    [DataRow(6,"654321", ",CCCCCC")]
    [DataRow(6,"789087", ",")]
    [DataRow(6,"123645", "BBB,CCC")]
    [DataRow(6,"312456", "BBB,CCC")]
    [DataRow(6,"123478", "BBBB,")]
    [DataRow(6,"789123", ",CCC")]
    [DataRow(3, "123", "BBB,")]
    [DataRow(3, "312", ",CCC")]
    [DataRow(3, "456", ",")]
    [DataRow(3, "132", "B,CC")]
    [DataRow(3, "12", "BB,")]
    [DataRow(3, "654321", ",")]
    [DataRow(3, "123456", "BBB,")]
    public void CheckGuess_ShouldReturnResultBasedOnUserGuessWithTargetOfSpecifiedLength(int targetLength, string userGuess, string expectedResult)
    {
        // Arrange
        var targetGenerator = new MockTargetGenerator(targetLength);
        var mooGame = new MooGame(targetGenerator);
        mooGame.SetUpNewGame();

        // Act
        var actualResult = mooGame.CheckGuess(userGuess);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void IsGuessCorrect_ShouldReturnTrueWithTargetOfDefaultLength()
    {
        // Arrange
        //using _mooGame from Setup()
        var resultToCheck = "BBBB,";

        // Act
        var isCorrect = _mooGame.IsGuessCorrect(resultToCheck);

        // Assert
        Assert.IsTrue(isCorrect);
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(",")]
    [DataRow(",CCCC")]
    [DataRow(",CCC")]
    [DataRow(",CC")]
    [DataRow(",C")]
    [DataRow("BBB,")]
    [DataRow("BB,")]
    [DataRow("B,")]
    [DataRow("BBB,C")]
    [DataRow("BB,CC")]
    [DataRow("B,CCC")]
    [DataRow("B,C")]
    [DataRow("BB,C")]
    [DataRow("B,CC")]
    public void IsGuessCorrect_ShouldReturnFalseWithTargetOfDefaultLength(string resultToCheck)
    {
        // Arrange
        //using _mooGame from Setup()

        // Act
        var isCorrect = _mooGame.IsGuessCorrect(resultToCheck);

        // Assert
        Assert.IsFalse(isCorrect);
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(6, "BBBBBB,")]
    [DataRow(5, "BBBBB,")]
    [DataRow(3, "BBB,")]
    public void IsGuessCorrect_ShouldReturnTrueWithTargetOfSpecifiedLength(int targetLength, string resultToCheck)
    {
        // Arrange
        var targetGenerator = new MockTargetGenerator(targetLength);
        var mooGame = new MooGame(targetGenerator);
        mooGame.SetUpNewGame();

        // Act
        var isCorrect = mooGame.IsGuessCorrect(resultToCheck);

        // Assert
        Assert.IsTrue(isCorrect);
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(5, ",")]
    [DataRow(5, ",CCCCC")]
    [DataRow(5, ",CCCC")]
    [DataRow(5, ",CCC")]
    [DataRow(5, ",CC")]
    [DataRow(5, ",C")]
    [DataRow(5, "BBBB,")]
    [DataRow(5, "BBB,")]
    [DataRow(5, "BB,")]
    [DataRow(5, "B,")]
    [DataRow(5, "BBBB,C")]
    [DataRow(5, "BBB,CC")]
    [DataRow(5, "BB,CCC")]
    [DataRow(5, "B,CCCC")]
    [DataRow(5, "B,C")]
    [DataRow(5, "BB,CC")]
    [DataRow(5, "BB,C")]
    [DataRow(5, "BBB,C")]
    [DataRow(5, "B,CC")]
    [DataRow(5, "B,CCC")]
    public void IsGuessCorrect_ShouldReturnFalseWithTargetOfSpecifiedLength(int targetLength, string resultToCheck)
    {
        // Arrange
        var targetGenerator = new MockTargetGenerator(targetLength);
        var mooGame = new MooGame(targetGenerator);
        mooGame.SetUpNewGame();

        // Act
        var isCorrect = mooGame.IsGuessCorrect(resultToCheck);

        // Assert
        Assert.IsFalse(isCorrect);
    }
}
