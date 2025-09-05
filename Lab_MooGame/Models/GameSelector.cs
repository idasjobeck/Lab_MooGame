using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.UI;

namespace Lab_MooGame.Models;

/// <summary>
/// Provides functionality for selecting a game from a predefined list of available games.
/// </summary>
/// <remarks>This class allows users to interact with a user interface to select a game from a collection of
/// available games. The selection process includes displaying the list of games, prompting the user for input, and
/// returning the selected game.</remarks>

public class GameSelector
{
    public IUserInterface UserInterface { get; }
    public Dictionary<GameSelection, GameConfig> Games { get; }

    public GameSelector(IUserInterface ui, Dictionary<GameSelection, GameConfig> games)
    {
        UserInterface = ui;
        Games = games;
    }

    public GameSelection SelectGame()
    {
        var keys = Games.Keys.ToList();

        DisplayGameSelections();
        var userSelection = MakeSelection();

        if (userSelection == 0)
        {
            UserInterface.Write("\nExiting...");
            return GameSelection.Exit;
        }

        return keys[userSelection - 1];
    }

    private void DisplayGameSelections()
    {
        UserInterface.Clear();
        UserInterface.Write("Select which game to play.\n");

        var keys = Games.Keys.ToList();
        var index = 1;

        foreach (var key in keys)
        {
            UserInterface.Write($"{index}. {Games[key].Name}");
            index++;
        }

        UserInterface.Write("0. Exit");
    }

    private int MakeSelection()
    {
        var keys = Games.Keys.ToList();
        string userInput;
        int choice;
        var validSelection = false;

        do
        {
            UserInterface.Write("\nEnter your choice:");
            userInput = UserInterface.Read()!;

            if (int.TryParse(userInput, out choice))
            {
                if (choice >= 0 && choice <= keys.Count)
                    validSelection = true;
                else
                    UserInterface.Write("Invalid choice. Please enter a number corresponding to the options above.");
            }
            else
            {
                UserInterface.Write("Invalid input. Please enter a number.");
            }
        } while (!validSelection);

        return choice;
    }
}