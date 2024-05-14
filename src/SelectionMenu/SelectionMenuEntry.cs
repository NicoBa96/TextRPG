public class SelectionMenuEntry
{
    public string name;
    public string trigger;
    public Func<bool> onTrigger;

    public SelectionMenuEntry(string trigger, string name, Func<bool> onTrigger)
    {
      this.trigger = trigger;
      this.name = name;
      this.onTrigger = onTrigger;
    }

}