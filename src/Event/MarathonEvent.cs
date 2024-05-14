public class MarathonEvent : APlayerEvent
{

    public MarathonEvent(Player player) : base(player)
    {

    }
    public override void Action()
    {
        RPGWriter.LineBreak();
        RPGWriter.Green("You are participating at the 10km marathon! Added steps to your stepcounter.");
        player.AddSteps(10000);
        RPGWriter.LineBreak();
    }

}