using Lab_MooGame.Models;

namespace Lab_MooGameTests.Models;
[TestClass]
public class SystemRandomTests
{
    private readonly SystemRandom _random = new();

    [TestMethod]
    [TestCategory("Unit")]
    public void Next_ShouldGenerateNumberBetweenZeroAndMaxValueInclusiveOfZero()
    {
        // Arrange
        //using _random SystemRandom instance created above
        var maxValue = 10;

        // Act
        var result = _random.Next(maxValue);

        // Assert
        var isNumberWithinRange = result >= 0 && result < maxValue;
        Assert.IsTrue(isNumberWithinRange);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void Next_ShouldGenerateNumberBetweenMinValueAndMaxValueInclusiveOfMinValue()
    {
        // Arrange
        //using _random SystemRandom instance created above
        var minValue = 5;
        var maxValue = 10;

        // Act
        var result = _random.Next(minValue, maxValue);

        // Assert
        var isNumberWithinRange = result >= minValue && result < maxValue;
        Assert.IsTrue(isNumberWithinRange);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void Next_ShouldGenerateNumberBetweenZeroAndIntMaxInclusiveOfZero()
    {
        // Arrange
        //using _random SystemRandom instance created above

        // Act
        var result = _random.Next();

        // Assert
        var isNumberWithinRange = result >= 0 && result < int.MaxValue;
        Assert.IsTrue(isNumberWithinRange);
    }
}