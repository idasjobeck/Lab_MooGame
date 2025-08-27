using Lab_MooGame.Services;
using Lab_MooGameTests.Mocks;

namespace Lab_MooGameTests.Services;

[TestClass]
public class MooTargetGeneratorTests
{
    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow(4, new int[] { 1, 2, 1, 3, 3, 4 }, "1234")]
    [DataRow(4, new int[] { 4, 5, 6, 4, 7, 8 }, "4567")]
    [DataRow(4, new int[] { 1, 1, 1, 2, 3, 4 }, "1234")]
    [DataRow(4, new int[] { 9, 8, 7, 6, 5, 4 }, "9876")]
    [DataRow(6, new int[] {1, 2, 1, 3, 4, 4, 5, 6}, "123456")]
    [DataRow(6, new int[] {7, 8, 9, 7, 6, 5, 4, 3}, "789654")]
    [DataRow(5, new int[] {1, 1, 2, 3, 4, 2, 5, 6}, "12345")]
    [DataRow(3, new int[] {7, 9, 9, 7, 6, 5, 2}, "796")]
    public void GenerateTarget_ShouldGenerateTargetOfSpecifiedLengthWithoutRepeatedNumbers(int targetLength, IEnumerable<int> predefinedNumbers, string expectedTarget)
    {
        // Arrange
        var mockRandomNumbers = new Queue<int>(predefinedNumbers);
        var mockRandomNumberGenerator = new MockRandom(mockRandomNumbers);
        var targetGenerator = new MooTargetGenerator(targetLength, mockRandomNumberGenerator);

        // Act
        var actualTarget = targetGenerator.GenerateTarget();

        // Assert
        Assert.AreEqual(expectedTarget, actualTarget);
    }
}
