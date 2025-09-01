using System.IO.Abstractions.TestingHelpers;
using Lab_MooGame.Services;

namespace Lab_MooGameTests.Services
{
    [TestClass]
    public class TextFileDataStorageTests
    {
        private readonly MockFileSystem _mockFileSystem = new();

        [TestMethod]
        [TestCategory("Unit")]
        public void SaveData_ShouldSaveDataToFile()
        {
            // Arrange
            _mockFileSystem.AddEmptyFile("testfile.txt");
            var textFileDataStorage = new TextFileDataStorage(_mockFileSystem, "testfile.txt");
            var currentGameUserScore = new Lab_MooGame.Models.CurrentGameUserScore
            {
                UserName = "TestUser",
                NumberOfGuesses = 5
            };
            var separator = "#&#";

            // Act
            textFileDataStorage.SaveData(currentGameUserScore, separator);

            // Assert
            var testfileContents = _mockFileSystem.GetFile("testfile.txt");
            StringAssert.Contains(testfileContents.TextContents, "TestUser#&#5");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetData_ShouldGetDataFromFile()
        {
            // Arrange
            var mockHighscores = new MockFileData("TestUser#&#5\r\nTestUser2#&#4\r\n");
            _mockFileSystem.AddFile("testfile.txt", mockHighscores);
            var textFileDataStorage = new TextFileDataStorage(_mockFileSystem, "testfile.txt");
            var expectedData = new List<string> { "TestUser#&#5", "TestUser2#&#4" };

            // Act
            var actualData = textFileDataStorage.GetData();

            // Assert
            CollectionAssert.AreEqual(expectedData, actualData);
        }
    }
}