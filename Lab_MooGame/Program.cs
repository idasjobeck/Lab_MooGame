using System;
using System.IO;
using System.Collections.Generic;
using Lab_MooGame.Controllers;
using Lab_MooGame.Models;
using Lab_MooGame.Services;
using Lab_MooGame.UI;

namespace Lab_MooGame;

class MainClass
{
    public static void Main(string[] args)
    {
        var ui = new ConsoleUI();
        var mooGame = new MooGame();
        var dataStorage = new TextFileStrategy("", "moo_highscores.txt");
        var scoreboardService = new ScoreboardService(dataStorage);
        var gameController = new GameController(ui, mooGame, scoreboardService);

        gameController.Run();
    }
}