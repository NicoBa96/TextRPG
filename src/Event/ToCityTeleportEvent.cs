public class ToCityTeleportEvent : APlayerEvent
{

    public ToCityTeleportEvent(Player player) : base(player)
    {

    }
    public override void Action()
    {
        RPGWriter.LineBreak();
        RPGWriter.Green("You suddenly feel strange magic surrounding you");
        RPGWriter.Green("IN the next momnent, you realize that you have been sent back to the city in an instant.");
        player.currentLocationName = "City Centre";
        RPGWriter.LineBreak();
        RPGWriter.Yellow("You are now at: City Centre");
        RPGWriter.LineBreak();
    }

}