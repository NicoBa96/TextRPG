using System.Text;

public class SelectionMenu
{

    public delegate void OnTrigger();

    List<SelectionMenuEntry> menuEntries;

    public SelectionMenu()
    {
        menuEntries = new List<SelectionMenuEntry>();
    }

    public void AddEntry(string trigger, string name, OnTrigger onTrigger)
    {
        SelectionMenuEntry entry = new SelectionMenuEntry(trigger, name, onTrigger);
        menuEntries.Add(entry);
    }

    public void PrintMenu()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (SelectionMenuEntry e in menuEntries)
        {
            stringBuilder.AppendLine(String.Format("{0} - {1}", e.trigger, e.name));
        }

        RPGWriter.Default(stringBuilder.ToString());
    }

    public void HandleInput()
    {
        string input = GetUserInput();
        menuEntries.First(e => e.trigger == input).onTrigger.Invoke();
    }

    private bool IsValidTrigger(string trigger)
    {
        return menuEntries.Any(e => e.trigger == trigger);
    }

    public string GetUserInput()
    {
        while (true)
        {
            PrintMenu();
            string userInput = Console.ReadLine()!;
            RPGWriter.LineBreak();

            if (IsValidTrigger(userInput))
            {
                return userInput;
            }

            RPGWriter.Red("Invalid Input! Try again.");
            RPGWriter.LineBreak();
        }
    }

}