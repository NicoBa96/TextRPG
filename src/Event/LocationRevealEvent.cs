public class LocationRevealEvent : APlayerEvent
{
    Location[] locations;

    public LocationRevealEvent(params Location[] locations) : base()
    {
        this.locations = locations;
        conditions.Add(new NotRevealedCondition(locations));
    }

    public override void Action()
    {
        base.Action();
        foreach (Location l in locations)
        {
            TextRPG.instance.player.RevealLocation(l);
        }
    }
}