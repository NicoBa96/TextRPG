public class LocationRevealReward : IReward
{
    Location location;
    public LocationRevealReward(Location location)
    {
        this.location = location;
    }

    public void Grant()
    {
        TextRPG.instance.player.RevealLocation(location);
    }
}