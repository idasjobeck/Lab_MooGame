using System;
using System.IO;
using System.Collections.Generic;
using Lab_MooGame.Controllers;
using Lab_MooGame.Models;
using Lab_MooGame.Services;
using Lab_MooGame.UI;

namespace Lab_MooGame;

public class Program
{
    public static void Main(string[] args)
    {
        var ui = new ConsoleUI();
        var targetGenerator = new MooTargetGenerator();
        var mooGame = new MooGame(targetGenerator);
        var dataStorage = new TextFileDataStorage("moo_highscores.txt");
        var scoreboardService = new ScoreboardService(dataStorage);
        var gameController = new GameController(ui, mooGame, scoreboardService);

        gameController.Run();
    }
}