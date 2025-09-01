using Lab_MooGame.Models;

namespace Lab_MooGameTests.Models;
[TestClass]
public class SystemRandomTests
{
    [TestMethod]
    public void Next_ShouldGenerateNumberBetweenZeroAndMaxValue()
    {
        // Arrange
        var random = new SystemRandom();
        var maxValue = 10;

        // Act
        var result = random.Next(maxValue);

        // Assert
        var isNumberWithinRange = result > 0 && result < maxValue;
        Assert.IsTrue(isNumberWithinRange);
    }
}