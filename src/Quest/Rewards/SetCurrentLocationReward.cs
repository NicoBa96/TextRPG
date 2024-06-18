public class SetCurrentLocationReward : IReward
{
    Location location;
    public SetCurrentLocationReward(Location location)
    {
        this.location = location;
    }

    public void Grant()
    {
        TextRPG.instance.map.SetCurrentLocation(location);
    }
}