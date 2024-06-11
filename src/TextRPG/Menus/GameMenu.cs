using System.Data.Common;

public class GameMenu
{
    private MoveMenu _moveMenu;
    private InventoryMenu _inventoryMenu;

    public GameMenu(Player player)
    {
        _moveMenu = new MoveMenu();
        _inventoryMenu = new InventoryMenu();
    }

    public SelectionMenu CreateGameMenu()
    {
        SelectionMenu menu = new SelectionMenu(() =>
        {
            if (TextRPG.instance.player.IsDead())
            {
                PrintDeathMenu();
                return false;
            }

            RPGWriter.Blue(String.Format("Location: {0} - Steps: {1} - Stamina: {2}", TextRPG.instance.player.currentLocationName, TextRPG.instance.player.GetWalkedSteps(), TextRPG.instance.player.GetStamina()));
            RPGWriter.Blue("What do you want to do? Choose!");
            return true;
        });

        menu.AddEntry("1", "Move", _moveMenu.OpenMoveMenu);
        menu.AddEntry("c", "Character Stats", WatchCharacterStats);
        menu.AddEntry("i", "Inventory", _inventoryMenu.OpenInventory);
        menu.AddEntry("j", "Quests", WatchQuests);
        menu.AddEntry("2", "Milestones", WatchMilestones);
        menu.AddEntry("3", "Watch Credits", ShowCredits);
        menu.AddEntry("4", "Save & Exit", BackToMainMenu);

        return menu;
    }

    private bool WatchCharacterStats()
    {
        RPGWriter.Default("Stats:");
        RPGWriter.Default($"- Walked Steps: {TextRPG.instance.player.GetWalkedSteps()}");
        RPGWriter.Default($"- Step Factor: {TextRPG.instance.player.GetStepFactor()}");
        RPGWriter.Default($"- Stamina: {TextRPG.instance.player.GetStamina()}");

        int visitedLocations = Locations.ALL.Count(TextRPG.instance.player.IsLocationRevealed);
        RPGWriter.Default($"- Visited Locations: {visitedLocations}/{Locations.ALL.Count()}");

        int completedMilestoneAmount = Milestone.ALL.Count(TextRPG.instance.player.IsGrantedMilestone);
        RPGWriter.Default($"- Completed Milestones: {completedMilestoneAmount}/{Milestone.ALL.Count()}");

        int completedQuestAmount = TextRPG.instance.player.questMemory.GetAllQuestsByStatus(QuestStatus.Finished).Count();
        RPGWriter.Default($"- Completed Quests: {completedQuestAmount}/{TextRPG.instance.questRegistry.allQuests.Count()}");

        RPGWriter.LineBreak();
        return true;
    }

    private void PrintDeathMenu()
    {
        RPGWriter.Red("You are dead!");
        RPGWriter.Red("Your Savegame has been deleted!");
        RPGWriter.LineBreak();
        SavegameManager.DeleteSaveGame();
    }

    private bool WatchMilestones()
    {
        RPGWriter.Blue("Milestones:");
        foreach (Milestone m in Milestone.ALL)
        {
            if (TextRPG.instance.player.IsGrantedMilestone(m))
            {
                RPGWriter.Default("[X] " + m.name + " - " + m.description);

            }
            else
            {
                RPGWriter.DarkGray("[ ] " + m.name + " - " + m.description);
            }
        }

        RPGWriter.LineBreak();
        return true;
    }

    private bool WatchQuests()
    {
        RPGWriter.Blue("Your active Quests:");
        List<Quest> questList = TextRPG.instance.player.questMemory.GetAllQuestsByStatus(QuestStatus.InProgress);
        if (questList.Count() == 0)
        {
            RPGWriter.Yellow("You have no active quests");
        }

        foreach (Quest q in questList)
        {
            RPGWriter.Default("- " + q.ToString());
        }

        RPGWriter.LineBreak();
        RPGWriter.Blue("Your completed Quests:");
        List<Quest> completedQuestList = TextRPG.instance.player.questMemory.GetAllQuestsByStatus(QuestStatus.Finished);
        if (completedQuestList.Count() == 0)
        {
            RPGWriter.Yellow("You have no completed quests");
        }

        foreach (Quest q in completedQuestList)
        {
            RPGWriter.DarkGray("- " + q.GetSummary());
        }

        RPGWriter.LineBreak();
        return true;
    }

    private bool ShowCredits()
    {
        TextRPG.instance.player.GrantMilestone(Milestone.WATCH_CREDITS);
        RPGWriter.Default("Credits");
        RPGWriter.Default("Lead Developer: Nico B.");
        RPGWriter.Default("Assistant: Joshua S.");
        RPGWriter.Default("");
        return true;
    }

    private bool BackToMainMenu()
    {
        SavegameManager.SaveGame();
        return false;
    }
}