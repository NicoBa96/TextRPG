public class LocationRevealEvent : AGameEvent
{
    Location[] locations;

    public LocationRevealEvent(params Location[] locations) : base()
    {
        this.locations = locations;
        conditions.Add(new NotRevealedEventCondition(locations));
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