using Lab_MooGame.Controllers;
using Lab_MooGame.Models;
using Lab_MooGame.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_MooGame.Controllers.Tests;

[TestClass]
public class GameControllerTests
{
    [DataTestMethod]
    [TestCategory("Unit")]
    [DataRow("TestUser,1234,n")]
    [DataRow("TestUser,4321,1234,n")]
    [DataRow("Ida,1234,y,1234,n")]
    [DataRow("Ida,2143,1234,y,2134,1234,n")]
    public void Run_ShouldCompleteGameSuccessfully(string userInputs)
    {
        // Arrange
        var userInterface = new MockUI(userInputs);
        var targetGenerator = new MockTargetGenerator();
        var guessingGame = new MooGame(targetGenerator);
        var scoreboardService = new ScoreboardService(new TextFileDataStorage("test_highscores.txt"));
        var gameController = new GameController(userInterface, guessingGame, scoreboardService);

        // Act
        gameController.Run();

        // Assert
        StringAssert.Contains(userInterface.Output, "Correct");
    }
}