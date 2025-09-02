using Lab_MooGame.Services;
using Lab_MooGameTests.Mocks;

namespace Lab_MooGameTests.Services;

[TestClass]
public class TargetGeneratorTests
{
    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(4, 9, new int[] { 1, 2, 1, 3, 3, 4 }, "1234")]
    [DataRow(4, 9, new int[] { 4, 5, 6, 4, 7, 8 }, "4567")]
    [DataRow(4, 9, new int[] { 1, 1, 1, 2, 3, 4 }, "1234")]
    [DataRow(4, 9, new int[] { 9, 8, 7, 6, 5, 4 }, "9876")]
    [DataRow(6, 9, new int[] {1, 2, 1, 3, 4, 4, 5, 6}, "123456")]
    [DataRow(6, 9, new int[] {7, 8, 9, 7, 6, 5, 4, 3}, "789654")]
    [DataRow(5, 9, new int[] {1, 1, 2, 3, 4, 2, 5, 6}, "12345")]
    [DataRow(3, 9, new int[] {7, 9, 9, 7, 6, 5, 2}, "796")]
    public void GenerateTarget_ShouldGenerateTargetOfSpecifiedLengthWithoutRepeatedNumbers(int targetLength, int maxRange, IEnumerable<int> predefinedNumbers, string expectedTarget)
    {
        // Arrange
        var allowRepeats = false;
        var mockRandomNumbers = new Queue<int>(predefinedNumbers);
        var mockRandomNumberGenerator = new MockRandom(mockRandomNumbers);
        var targetGenerator = new TargetGenerator(targetLength, maxRange, allowRepeats, mockRandomNumberGenerator);

        // Act
        var actualTarget = targetGenerator.GenerateTarget();

        // Assert
        Assert.AreEqual(expectedTarget, actualTarget);
    }

    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(4, 6, new int[] { 1, 2, 1, 3 }, "1213")]
    [DataRow(4, 6, new int[] { 4, 5, 6, 4 }, "4564")]
    [DataRow(4, 6, new int[] { 1, 1, 1, 2 }, "1112")]
    [DataRow(4, 6, new int[] { 1, 2, 3, 4 }, "1234")]
    public void GenerateTarget_ShouldGenerateTargetOfSpecifiedLengthWithPossibleRepeatedNumbers(int targetLength, int maxRange, IEnumerable<int> predefinedNumbers, string expectedTarget)
    {
        // Arrange
        var allowRepeats = true;
        var mockRandomNumbers = new Queue<int>(predefinedNumbers);
        var mockRandomNumberGenerator = new MockRandom(mockRandomNumbers);
        var targetGenerator = new TargetGenerator(targetLength, maxRange, allowRepeats, mockRandomNumberGenerator);

        // Act
        var actualTarget = targetGenerator.GenerateTarget();

        // Assert
        Assert.AreEqual(expectedTarget, actualTarget);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void TargetLength_ShouldGetTargetLengthSet()
    {
        // Arrange
        var targetLength = 5;
        var maxRange = 9;
        var allowRepeats = false;
        var targetGenerator = new TargetGenerator(targetLength, maxRange,allowRepeats);
        var expectedTargetLength = 5;

        // Act
        var actualTargetLength = targetGenerator.TargetLength;

        // Assert
        Assert.AreEqual(expectedTargetLength, actualTargetLength);
    }
}
