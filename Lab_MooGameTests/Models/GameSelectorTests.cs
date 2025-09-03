using Lab_MooGame;
using Lab_MooGame.Models;
using Lab_MooGame.UI;
using Lab_MooGameTests.Mocks;

namespace Lab_MooGameTests.Models;

[TestClass]
public class GameSelectorTests
{
    private Dictionary<GameSelection, GameConfig> _gameSelectionsAvailable = new ()
    {
        { GameSelection.MooGame, new GameConfig("Moo Game",4, 9, false) },
        { GameSelection.Mastermind, new GameConfig("Mastermind", 4, 6, true) }
    };

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow("1", GameSelection.MooGame)]
    [DataRow("2", GameSelection.Mastermind)]
    [DataRow("0", GameSelection.Exit)]
    public void SelectGame_ShouldReturnTheCorrectGameSelectionForUserInput(string userInputs, GameSelection expectedGameSelection)
    {
        // Arrange
        var ui = new MockUI(userInputs);
        var gameSelector = new GameSelector(ui, _gameSelectionsAvailable);

        // Act
        var actualGameSelection = gameSelector.SelectGame();

        // Assert
        Assert.AreEqual(expectedGameSelection, actualGameSelection);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void SelectGame_ErrorMessageShouldHaveDisplayedForNonNumericalUserInput()
    {
        // Arrange
        var userInputs = "a,1";
        var ui = new MockUI(userInputs);
        var gameSelector = new GameSelector(ui, _gameSelectionsAvailable);
        var expectedErrorMessage = "Invalid input. Please enter a number.";

        // Act
        gameSelector.SelectGame();

        // Assert
        StringAssert.Contains(ui.Output, expectedErrorMessage);
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow("3,1")]
    [DataRow("-1,2")]
    public void SelectGame_ErrorMessageShouldHaveDisplayedForOutOfRangeNumericalUserInput(string userInputs)
    {
        // Arrange
        var ui = new MockUI(userInputs);
        var gameSelector = new GameSelector(ui, _gameSelectionsAvailable);
        var expectedErrorMessage = "Invalid choice. Please enter a number corresponding to the options above.";

        // Act
        gameSelector.SelectGame();

        // Assert
        StringAssert.Contains(ui.Output, expectedErrorMessage);
    }
}