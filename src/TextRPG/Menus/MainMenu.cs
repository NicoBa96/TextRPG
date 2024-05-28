public class MainMenu
{
 public SelectionMenu CreateMainMenu()
    {
        SelectionMenu menu = new SelectionMenu(() =>
        {
            RPGWriter.Blue("Welcome to the Big Stepper!");
            return true;
        });
        menu.AddConditionalEntry("c", "Continue", () =>
        {
            TextRPG.instance.ContinueGame();
            TextRPG.instance.Start();
            return true;
        }, SavegameManager.HasSaveGame);

        menu.AddEntry("n", "New Game", () =>
        {
            TextRPG.instance.ResetSaveGame();
            TextRPG.instance.Start();
            return true;
        });
        menu.AddEntry("e", "Exit Game", TextRPG.instance.Exit);

        return menu;
    }
}