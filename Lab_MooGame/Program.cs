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

        //Moo game setup
        var mooTargetLength = 4;
        var mooMaxRange = 9;
        var mooAllowRepeats = false;
        var mooTargetGenerator = new TargetGenerator(mooTargetLength, mooMaxRange, mooAllowRepeats);
        var mooGame = new MooGame(mooTargetGenerator);
        var mooDataStorage = new TextFileDataStorage("moo_highscores.txt");
        var mooScoreboardService = new ScoreboardService(mooDataStorage);


        //Mastermind setup
        var mastermindTargetLength = 4;
        var mastermindMaxRange = 6;
        var mastermindAllowRepeats = true;
        var mastermindTargetGenerator = new TargetGenerator(mastermindTargetLength, mastermindMaxRange, mastermindAllowRepeats);
        var mastermindGame = new MastermindGame(mastermindTargetGenerator);
        var mastermindDataStorage = new TextFileDataStorage("mastermind_highscores.txt");
        var mastermindScoreboardService = new ScoreboardService(mastermindDataStorage);


        //var gameController = new GameController(ui, mooGame, mooScoreboardService);
        var gameController = new GameController(ui, mastermindGame, mastermindScoreboardService);

        gameController.Run();
    }
}