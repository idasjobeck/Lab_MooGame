using Lab_MooGame.Models;
using Lab_MooGameTests.Mocks;

namespace Lab_MooGameTests.Models;

[TestClass]
public class GameConfigTests
{
    [TestMethod]
    public void GameConfig_CanConstructWithTargetGeneratorProvided()
    {
        // Arrange
        var targetLength = 4;
        var maxRange = 9;
        var allowRepeats = false;
        var targetGenerator = new MockTargetGenerator(targetLength, maxRange, allowRepeats);
        var name = "TestConfig";

        // Act
        var actualGameConfig = new GameConfig(name, targetGenerator);

        // Assert
        Assert.AreEqual(targetLength, actualGameConfig.TargetLength);
        Assert.AreEqual(maxRange, actualGameConfig.MaxRange);
        Assert.AreEqual(allowRepeats, actualGameConfig.AllowRepeats);
        Assert.AreEqual(name, actualGameConfig.Name);
        Assert.AreEqual(targetGenerator, actualGameConfig.TargetGenerator);
    }
}