public class LocationRevealEvent : APlayerEvent
{
    Location[] locations;
    public LocationRevealEvent(Player player, params Location[] locations) : base(player)
    {
        this.locations = locations;

    }

    public override void Action()
    {
        base.Action();
        foreach (Location l in locations)
        {
            player.RevealLocation(l);
        }
    }
}