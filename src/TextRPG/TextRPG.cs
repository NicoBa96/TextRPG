using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using Microsoft.VisualBasic;

public class TextRPG
{
    public static TextRPG instance;
    public GameMap map;
    public Player player;
    public MainMenu _mainMenu;
    private GameMenu _gameMenu;

    public TextRPG()
    {
        if (instance == null)
        {
            instance = this;
        }

        player = new Player();
        _mainMenu = new MainMenu();
        _gameMenu = new GameMenu(player);

    }

    public bool ResetSaveGame()
    {
        player = new Player();
        map = new GameMap();
        map.SetCurrentLocation(map.GetStartLocation());
        Quest testQuest = new Quest(1).AddReachLocationGoal(Locations.mountains);
        player.questMemory.StartQuest(testQuest);
        SavegameManager.SaveGame();
        return true;
    }

    public bool ContinueGame()
    {
        player = SavegameManager.LoadSaveGame();
        map = new GameMap();
        return true;
    }

    public void Start()
    {
        SelectionMenu menu = _gameMenu.CreateGameMenu();
        menu.HandleInput();
    }

    public void HandleEvent(Location location)
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
}