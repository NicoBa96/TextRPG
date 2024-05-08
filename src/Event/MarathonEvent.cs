public class MarathonEvent : APlayerEvent
{

    public MarathonEvent(Player player) : base(player)
    {

    }
    public override void Action()
    {
        Console.WriteLine();
        RPGWriter.Green("You are participating at the 10km city marathon! Added steps to your stepcounter.");
        player.AddSteps(10000);
        Console.WriteLine("");
    }

}