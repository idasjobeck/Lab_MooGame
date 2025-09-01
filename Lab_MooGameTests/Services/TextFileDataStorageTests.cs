using System.IO.Abstractions.TestingHelpers;
using Lab_MooGame.Services;

namespace Lab_MooGameTests.Services
{
    [TestClass]
    public class TextFileDataStorageTests
    {
        private readonly MockFileSystem _mockFileSystem = new();

        [TestMethod]
        [TestCategory("NotImplemented")]
        public void SaveDataTest()
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
        [TestCategory("NotImplemented")]
        public void GetDataTest()
        {
            Assert.Fail();
        }
    }
}