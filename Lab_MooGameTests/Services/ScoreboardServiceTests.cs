using Lab_MooGame.Models;
using Lab_MooGame.Services;
using Lab_MooGameTests.Mocks;

namespace Lab_MooGameTests.Services;

[TestClass]
public class ScoreboardServiceTests
{
    [TestMethod]
    [TestCategory("Unit")]
    public void UpdateScoreBoard_ShouldSaveCurrentGameScoreForUser()
    {
        // Arrange
        var dataStorage = new MockDataStorage();
        var scoreboardService = new ScoreboardService(dataStorage);
        var currentGameUsernameAndScore = new CurrentGameUserScore();
        currentGameUsernameAndScore.UserName = "TestUser";
        currentGameUsernameAndScore.NumberOfGuesses = 3;
        var expectedSavedData = new List<string> { "TestUser#&#3" };

        // Act
        scoreboardService.UpdateScoreBoard(currentGameUsernameAndScore);

        // Assert
        CollectionAssert.AreEqual(expectedSavedData, dataStorage.GetData());
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void GetTopScores_ShouldDisplayTopScoresSortedByAverageGuessesInAscendingOrder()
    {
        // Arrange
        var mockData = new List<string>
        {
            "UserA#&#5",
            "UserB#&#3",
            "UserC#&#2",
            "UserC#&#1",
            "UserC#&#1",
            "UserC#&#1",
            "UserB#&#4"
        };
        var dataStorage = new MockDataStorage(mockData);
        var scoreboardService = new ScoreboardService(dataStorage);
        var expectedTopScores = new List<PlayerStats>
        {
            new PlayerStats("UserC", 2), // Will have an average of 1.25 guesses
            new PlayerStats("UserB", 3), // Will have an average of 3.5 guesses
            new PlayerStats("UserA", 5)  // Will have an average of 5 guesses
        };
        expectedTopScores[0].UpdateStats(1);
        expectedTopScores[0].UpdateStats(1);
        expectedTopScores[0].UpdateStats(1);
        expectedTopScores[1].UpdateStats(4);

        // Act
        var actualTopScores = scoreboardService.GetTopScores();

        // Assert
        for (int i = 0; i < expectedTopScores.Count; i++)
        {
            Assert.AreEqual(expectedTopScores[i].UserName, actualTopScores[i].UserName);
            Assert.AreEqual(expectedTopScores[i].NumberOfGames, actualTopScores[i].NumberOfGames);
            Assert.AreEqual(expectedTopScores[i].TotalNumberOfGuesses, actualTopScores[i].TotalNumberOfGuesses);
            Assert.AreEqual(expectedTopScores[i].AverageNumberOfGuesses(), actualTopScores[i].AverageNumberOfGuesses());
        }
    }
}