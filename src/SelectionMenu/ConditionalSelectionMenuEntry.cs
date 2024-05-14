
public class ConditionalSelectionMenuEntry : SelectionMenuEntry
{
    Func<bool> condition;
    public ConditionalSelectionMenuEntry(string trigger, string name, Func<bool> onTrigger, Func<bool> condition) : base(trigger, name, onTrigger)
    {
        this.condition = condition;
    }

    public bool IsAvailable()
    {
      return condition.Invoke();
    }
}