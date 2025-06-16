using System;
using System.IO;
using System.Collections.Generic;

namespace Lab_MooGame;

class MainClass
{
    public static void Main(string[] args)
    {
        var ui = new ConsoleUI();
        var mooGame = new MooGame(ui);
        var gameController = new GameController(ui, mooGame);

        gameController.Run();
    }
}