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
        var gameUserScore = new CurrentGameUserScore();
        gameUserScore.UserName = "TestUser";
        gameUserScore.NumberOfGuesses = 3;
        var expectedSavedData = new List<string> { "TestUser#&#3" };

        // Act
        scoreboardService.UpdateScoreBoard(gameUserScore);

        // Assert
        CollectionAssert.AreEqual(expectedSavedData, dataStorage.GetData());
    }

    [TestMethod]
    [TestCategory("NotImplemented")]
    public void GetTopScoresTest()
    {
        Assert.Fail();
    }
}