using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using Lab_MooGame.Controllers;
using Lab_MooGame.Models;
using Lab_MooGame.Services;
using Lab_MooGame.UI;

namespace Lab_MooGame;

public enum GameSelection
{
    Exit = 0,
    MooGame = 1,
    Mastermind = 2
}

/// <summary>
/// Represents the entry point of the application, which allows the user to select and play different guessing games.
/// </summary>
/// <remarks>The application provides a menu for selecting a game, initializes the selected game's configuration,
/// and manages the game loop. Currently, the supported games are "Moo Game" and "Mastermind". The user can also exit
/// the application from the menu.</remarks>

public class Program
{
    public static void Main(string[] args)
    {
        var ui = new ConsoleUI();
        var gameSelectionsAvailable = new Dictionary<GameSelection, GameConfig>()
        {
            { GameSelection.MooGame, new GameConfig("Moo Game",4, 9, false) },
            { GameSelection.Mastermind, new GameConfig("Mastermind", 4, 6, true) }
        };

        var gameSelector = new GameSelector(ui, gameSelectionsAvailable);
        GameSelection selectedGame;

        do
        {
            selectedGame = gameSelector.SelectGame();

            if (selectedGame == GameSelection.Exit)
                return;

            var gameConfig = gameSelectionsAvailable[selectedGame];
            IGuessingGame game = selectedGame switch
            {
                GameSelection.MooGame => new MooGame(gameConfig.TargetGenerator),
                GameSelection.Mastermind => new MastermindGame(gameConfig.TargetGenerator),
                _ => throw new InvalidEnumArgumentException("Invalid game selection.")
            };

            var highscoreFilePath = $"{gameConfig.Name.Trim().Replace(" ", "")}_highscores.txt";
            var dataStorage = new TextFileDataStorage(highscoreFilePath);
            var scoreboardService = new ScoreboardService(dataStorage);

            var gameController = new GameController(ui, game, scoreboardService);

            gameController.Run();
        } while (true);
    }
}