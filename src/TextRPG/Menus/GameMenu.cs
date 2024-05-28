public class GameMenu
{
    private MoveMenu _moveMenu;
    private InventoryMenu _inventoryMenu;
    private Player player;

    public GameMenu(Player player)
    {
        _moveMenu = new MoveMenu();
        _inventoryMenu = new InventoryMenu();
        this.player = player;
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

            RPGWriter.Blue(String.Format("Location: {0} - Steps: {1} - Health: {2}", player.currentLocationName, player.GetWalkedSteps(), player.GetHealth()));
            RPGWriter.Blue("What do you want to do? Choose!");
            return true;
        });

        menu.AddEntry("1", "Move", _moveMenu.OpenMoveMenu);
        menu.AddEntry("i", "Inventory", _inventoryMenu.OpenInventory);
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

    private bool ShowCredits()
    {
        player.GrantMilestone(Milestone.WATCH_CREDITS);
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