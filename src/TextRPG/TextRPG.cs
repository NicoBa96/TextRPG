using System.Drawing;
using System.Text;
using System.Text.Json;
using Microsoft.VisualBasic;

public class TextRPG
{
    public GameMap map;
    public Player player;
    bool gameloop = true;

    public TextRPG()
    {
        player = new Player();
    }


    public SelectionMenu CreateMainMenu()
    {
        SelectionMenu menu = new SelectionMenu();

        if (SavegameManager.HasSaveGame())
        {
            menu.AddEntry("c", "Continue", ContinueGame);
        }
        menu.AddEntry("n", "New Game", StartNewGame);
        menu.AddEntry("w", "Watch Credits", ShowCredits);
        menu.AddEntry("e", "Exit Game", Exit);

        return menu;
    }

    private void StartNewGame()
    {
        player = new Player();
        map = new GameMap(player);
        map.SetCurrentLocation(map.GetStartLocation());
        SavegameManager.SaveGame(player);
        Start();
    }

    private void ContinueGame()
    {
        player = SavegameManager.LoadSaveGame();
        map = new GameMap(player);
        Start();
    }

    public void ShowGameMenu()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("What do you want to do? Choose!");
        stringBuilder.AppendLine("1 - Move");
        stringBuilder.AppendLine("2 - Milestones");
        stringBuilder.AppendLine("3 - Main Menu");
        RPGWriter.Default(stringBuilder.ToString());
    }


    public void MovePlayerMenu()
    {
        RPGWriter.Default("Location: " + player.currentLocationName + ", you can travel to:");
        StringBuilder stringBuilder = new StringBuilder();
        List<Trail> paths = map.GetPaths();
        stringBuilder.AppendLine("0 - Back to Menu");
        for (int i = 0; i < paths.Count; i++)
        {
            Trail currentEdge = paths.ElementAt(i);
            Location targetNode = currentEdge.destinationNode == map.GetCurrentLocation() ? currentEdge.startNode : currentEdge.destinationNode;
            if (player.IsLocationRevealed(targetNode))
            {
                stringBuilder.AppendLine(String.Format("{0} - {1} [{2} steps]", i + 1, targetNode.name, currentEdge.stepValue));
            }
            else
            {
                stringBuilder.AppendLine(String.Format("{0} - {1} [{2} steps]", i + 1, "???", "???"));
            }
        }
        stringBuilder.AppendLine(paths.Count + 1 + " - Show Map");
        RPGWriter.Default(stringBuilder.ToString());
    }

    public void Start()
    {
        RPGWriter.Default(">>Game starts<<");
        gameloop = true;

        while (gameloop)
        {
            RPGWriter.Default(String.Format("Location: {0} - Steps: {1} - Health: {2}", player.currentLocationName, player.GetWalkedSteps(), player.GetHealth()));
            int input = Program.GetUserInput(1, 3, ShowGameMenu);

            switch (input)
            {
                case 1:
                    MovingPlayer();
                    break;

                case 2:
                    WatchMilestones();
                    break;

                case 3:
                    BacktoMainMenu();
                    break;

            }

        }
    }


    private void WatchMilestones()
    {
        RPGWriter.LineBreak();
        RPGWriter.Default("Milestones:");
        foreach (Milestone m in Milestone.ALL)
        {
            if (player.IsGrantedMilestone(m))
            {
                RPGWriter.Green("[X] " + m.name + " - " + m.description);

            }
            else
            {
                RPGWriter.DarkGray("[ ] " + m.name + " - " + m.description);
            }
        }
        RPGWriter.LineBreak();

    }

    private void BacktoMainMenu()
    {
        SavegameManager.SaveGame(player);
        gameloop = false;
    }


    private void MovingPlayer()
    {
        List<Trail> paths = map.GetPaths();
        int input = Program.GetUserInput(0, paths.Count + 1, this.MovePlayerMenu);
        Console.Clear();

        while (input == paths.Count + 1)
        {
            map.DrawMap();
            input = Program.GetUserInput(0, paths.Count + 1, this.MovePlayerMenu);
            Console.Clear();
        }

        if (input == 0)
        {
            return;
        }

        Trail chosenEdge = paths.ElementAt(input - 1);
        player.AddSteps(chosenEdge.stepValue);
        map.SetCurrentLocation(chosenEdge.destinationNode == map.GetCurrentLocation() ? chosenEdge.startNode : chosenEdge.destinationNode);
        StringBuilder destinationStringBuilder = new StringBuilder();
        destinationStringBuilder.AppendLine(player.currentLocationName);
        destinationStringBuilder.AppendLine(map.GetCurrentLocation().description);
        Console.ForegroundColor = ConsoleColor.Cyan;
        RPGWriter.Blue(destinationStringBuilder.ToString());
        Console.ResetColor();

        OnLocationEnter(map.GetCurrentLocation());
    }


    private void OnLocationEnter(Location location)
    {
        player.RevealLocation(location);
        HandleEvent(location);
    }

    private void HandleEvent(Location location)
    {
        foreach (AGameEvent e in location.locationEvents)
        {
            if (e.AllConditionsFullfilled())
            {
                e.Action();
            }
        }
    }

    public void Exit()
    {
        Environment.Exit(0);
    }

    public void ShowCredits()
    {
        player.GrantMilestone(Milestone.WATCHCREDITS);
        RPGWriter.Default("Credits");
        RPGWriter.Default("Lead Developer: Nico B.");
        RPGWriter.Default("Assistant: Joshua S.");
        RPGWriter.Default("");
    }

}