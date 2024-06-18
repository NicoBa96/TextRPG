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
        map = new GameMap();
    }

    public bool ResetSaveGame()
    {
        player = new Player();
        map.SetCurrentLocation(map.GetStartLocation());
        player.questMemory.StartQuest(QuestIdentifier.StartQuest);
        player.questMemory.StartQuest(QuestIdentifier.DeliverLetterToCoast);
        player.AddItemToInventory(Items.Letter);
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
        foreach (GameEvent e in location.locationEvents)
        {
            if (e.AllConditionsFullfilled())
            {
                if (e.conditions.Any(c => { return c is ByChanceEventCondition; }))
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