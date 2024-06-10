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
    public MainMenu mainMenu;
    public QuestRegistry questRegistry;

    private GameMenu _gameMenu;

    public TextRPG()
    {
        if (instance == null)
        {
            instance = this;
        }

        player = new Player();
        mainMenu = new MainMenu();
        _gameMenu = new GameMenu(player);
        questRegistry = new QuestRegistry(this);
    }

    public bool ResetSaveGame()
    {
        player = new Player();
        map = new GameMap();
        map.SetCurrentLocation(map.GetStartLocation());
        player.questMemory.StartQuest(questRegistry.GetQuest(QuestIdentifier.StartQuest));
        SavegameManager.SaveGame();
        return true;
    }

    public bool ContinueGame()
    {
        player = SavegameManager.LoadSaveGame();

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