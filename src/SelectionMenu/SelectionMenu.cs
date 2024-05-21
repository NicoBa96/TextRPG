using System.Collections.Frozen;
using System.Text;

public class SelectionMenu
{

    public delegate bool PrePrint();

    PrePrint prePrint;

    const string exitTrigger = "abort";

    List<SelectionMenuEntry> menuEntries;

    public SelectionMenu(PrePrint p)
    {
        prePrint = p;
        menuEntries = new List<SelectionMenuEntry>();
    }

    public void AddEntry(string trigger, string name, Func<bool> onTrigger)
    {
        SelectionMenuEntry entry = new SelectionMenuEntry(trigger, name, onTrigger);
        menuEntries.Add(entry);
    }
    public void AddConditionalEntry(string trigger, string name, Func<bool> onTrigger, Func<bool> condition)
    {
        ConditionalSelectionMenuEntry entry = new ConditionalSelectionMenuEntry(trigger, name, onTrigger, condition);
        menuEntries.Add(entry);
    }

    public bool PrintMenu()
    {
        bool prePrintContinueBool = prePrint.Invoke();
        if (!prePrintContinueBool)
        {
            return false;
        }

        StringBuilder stringBuilder = new StringBuilder();
        foreach (SelectionMenuEntry e in AvailableEntries())
        {
            stringBuilder.AppendLine(String.Format("{0} - {1}", e.trigger, e.name));
        }

        RPGWriter.Default(stringBuilder.ToString());
        return true;
    }

    public List<SelectionMenuEntry> AvailableEntries()
    {
        return menuEntries.Where(e =>
         {
             if (e is ConditionalSelectionMenuEntry csme)
             {
                 return csme.IsAvailable();
             }
             return true;
         }).ToList();
    }

    public void HandleInput()
    {
        bool loop = true;

        while (loop)
        {
            string input = GetUserInput();
            if (input == exitTrigger)
            {
                return;
            }

            loop = AvailableEntries().First(e => e.trigger == input).onTrigger.Invoke();
        }
    }

    private bool IsValidTrigger(string trigger)
    {
        return AvailableEntries().Any(e => e.trigger == trigger);
    }

    public string GetUserInput()
    {
        while (true)
        {
            bool prePrintContinueBool = PrintMenu();
            if (!prePrintContinueBool)
            {
                return exitTrigger;
            }
            
            string userInput = Console.ReadLine()!;
            RPGWriter.LineBreak();
            Console.Clear();

            if (IsValidTrigger(userInput))
            {
                return userInput;
            }

            RPGWriter.Red("Invalid Input! Try again.");
            RPGWriter.LineBreak();
        }
    }
}