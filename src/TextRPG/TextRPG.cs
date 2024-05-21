using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using Microsoft.VisualBasic;

public class TextRPG
{
    public GameMap map;
    public Player player;

    public TextRPG()
    {
        player = new Player();
    }


    public SelectionMenu CreateMainMenu()
    {
        SelectionMenu menu = new SelectionMenu(() =>
        {
            RPGWriter.Blue("Welcome to the Big Stepper!");
            return true;
        });
        menu.AddConditionalEntry("c", "Continue", () =>
        {
            ContinueGame();
            Start();
            return true;
        }, SavegameManager.HasSaveGame);

        menu.AddEntry("n", "New Game", () =>
        {
            ResetSaveGame();
            Start();
            return true;
        });
        menu.AddEntry("e", "Exit Game", Exit);

        return menu;
    }

    private bool ResetSaveGame()
    {
        player = new Player();
        map = new GameMap(player);
        map.SetCurrentLocation(map.GetStartLocation());
        SavegameManager.SaveGame(player);
        return true;
    }

    private bool ContinueGame()
    {
        player = SavegameManager.LoadSaveGame();
        map = new GameMap(player);
        return true;
    }

    public SelectionMenu CreateGameMenu()
    {
        SelectionMenu menu = new SelectionMenu(() =>
        {
            if (player.IsDead())
            {
                PrintDeathMenu();
                return false;
            }

            RPGWriter.Blue(String.Format("Location: {0} - Steps: {1} - Health: {2}", player.currentLocationName, player.GetWalkedSteps(), player.GetHealth()));
            RPGWriter.Blue("What do you want to do? Choose!");
            return true;
        });

        menu.AddEntry("1", "Move", () =>
        {
            CreateMoveMenu().HandleInput();
            return true;
        });
        menu.AddEntry("2", "Milestones", WatchMilestones);
        menu.AddEntry("3", "Watch Credits", ShowCredits);
        menu.AddEntry("4", "Save & Exit", BackToMainMenu);

        return menu;
    }

    private void PrintDeathMenu()
    {
        RPGWriter.Red("You are dead!");
        RPGWriter.Red("Your Savegame has been deleted!");
        RPGWriter.LineBreak();
        SavegameManager.DeleteSaveGame();
    }

    public SelectionMenu CreateMoveMenu()
    {
        SelectionMenu menu = new SelectionMenu(() =>
        {
            RPGWriter.Blue("Location: " + player.currentLocationName + ", you can travel to:");
            return true;
        });

        menu.AddEntry("0", "Back to Menu", () => false);

        List<Trail> paths = map.GetPaths();
        for (int i = 0; i < paths.Count; i++)
        {
            Trail currentEdge = paths.ElementAt(i);
            Location targetNode = currentEdge.destinationNode == map.GetCurrentLocation() ? currentEdge.startNode : currentEdge.destinationNode;
            if (player.IsLocationRevealed(targetNode))
            {
                string entryname = String.Format("{0} [{1} steps]", targetNode.name, currentEdge.stepValue);
                menu.AddEntry("" + (i + 1), entryname, () => MovePlayerTo(currentEdge));
            }
            else
            {
                string entryname = (String.Format("{0} [{1} steps]", "???", "???"));
                menu.AddEntry("" + (i + 1), entryname, () => MovePlayerTo(currentEdge));
            }
        }

        menu.AddEntry("m", "Show Map", () =>
        {
            player.GrantMilestone(Milestone.FIRST_MAP_USAGE);
            map.DrawMap();
            return true;
        });



        return menu;
    }

    public bool MovePlayerTo(Trail trail)
    {
        player.AddSteps(trail.stepValue);
        map.SetCurrentLocation(trail.destinationNode == map.GetCurrentLocation() ? trail.startNode : trail.destinationNode);
        OnLocationEnter(map.GetCurrentLocation());
        return false;
    }


    public void Start()
    {
        RPGWriter.Default(">>Game starts<<");
        SelectionMenu menu = CreateGameMenu();
        menu.HandleInput();
    }


    private bool WatchMilestones()
    {
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
        return true;

    }

    private bool BackToMainMenu()
    {
        SavegameManager.SaveGame(player);
        return false;
    }

    private void OnLocationEnter(Location location)
    {
        player.RevealLocation(location);
        if (map.nodes.All(player.IsLocationRevealed))
        {
            player.GrantMilestone(Milestone.EVERYTHING_REVEALED);
        }
        RPGWriter.Yellow("You have enetered: " + location.name + ", " + location.description + ".");
        RPGWriter.LineBreak();
        HandleEvent(location);
    }



    private void HandleEvent(Location location)
    {
        foreach (AGameEvent e in location.locationEvents)
        {
            if (e.AllConditionsFullfilled())
            {
                if (e.conditions.Any(c => { return c is ByChanceCondition; }))
                {
                    player.AddChanceConditionTriggerCount();
                }
                e.Action();
            }
        }
    }

    public bool Exit()
    {
        Environment.Exit(0);
        return false;
    }

    public bool ShowCredits()
    {
        player.GrantMilestone(Milestone.WATCH_CREDITS);
        RPGWriter.Default("Credits");
        RPGWriter.Default("Lead Developer: Nico B.");
        RPGWriter.Default("Assistant: Joshua S.");
        RPGWriter.Default("");
        return true;
    }

}