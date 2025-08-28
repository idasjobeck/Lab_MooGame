using Lab_MooGame.Models;

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
}