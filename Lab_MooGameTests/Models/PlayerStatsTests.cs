using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab_MooGame.Models;
using Lab_MooGameTests.Mocks;

namespace Lab_MooGameTests.Models;

[TestClass]
public class PlayerStatsTests
{
    private PlayerStats? _playerStats;

    [TestInitialize]
    public void Setup()
    {
        _playerStats = new PlayerStats("TestUser", 3);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void UpdateStats_ShouldIncreaseGamesByOneAndGuessesBySetAmount()
    {
        // Arrange
        var expectedTotalNumberOfGuesses = 7;
        var expectedNumberOfGames = 2;

        // Act
        _playerStats!.UpdateStats(4);

        // Assert
        Assert.AreEqual(expectedTotalNumberOfGuesses, _playerStats.TotalNumberOfGuesses);
        Assert.AreEqual(expectedNumberOfGames, _playerStats.NumberOfGames);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void UpdateStats_ShouldIncreaseGamesByNumberOfRunsAndGuessesBySetAmounts()
    {
        // Arrange
        var expectedTotalNumberOfGuesses = 13;
        var expectedNumberOfGames = 4;

        // Act
        _playerStats!.UpdateStats(3);
        _playerStats!.UpdateStats(5);
        _playerStats!.UpdateStats(2);

        // Assert
        Assert.AreEqual(expectedTotalNumberOfGuesses, _playerStats?.TotalNumberOfGuesses);
        Assert.AreEqual(expectedNumberOfGames, _playerStats?.NumberOfGames);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void AverageNumberOfGuesses_ShouldReturnAverageGuesses()
    {
        // Arrange
        _playerStats!.UpdateStats(2);
        var expectedAverage = 2.5;

        // Act
        var actualAverage = _playerStats.AverageNumberOfGuesses();

        // Assert
        Assert.AreEqual(expectedAverage, actualAverage);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void Equals_ShouldReturnFalseAsPlayerStatsObjectsAreNotEqual()
    {
        // Arrange
        var differentPlayerStats = new PlayerStats("DifferentUser", 3);

        // Act
        var areEqual = _playerStats!.Equals(differentPlayerStats);

        // Assert
        Assert.IsFalse(areEqual);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void Equals_ShouldReturnFalseAsOneObjectIsNotPlayerStats()
    {
        // Arrange
        var notAPlayerStats = new MockTargetGenerator(4);

        // Act
        var areEqual = _playerStats!.Equals(notAPlayerStats);

        // Assert
        Assert.IsFalse(areEqual);
    }
}