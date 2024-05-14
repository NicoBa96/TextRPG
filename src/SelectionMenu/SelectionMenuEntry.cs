public class SelectionMenuEntry
{
    public string name;
    public string trigger;
    public SelectionMenu.OnTrigger onTrigger;

    public SelectionMenuEntry(string trigger, string name, SelectionMenu.OnTrigger onTrigger)
    {
      this.trigger = trigger;
      this.name = name;
      this.onTrigger = onTrigger;
    }

}