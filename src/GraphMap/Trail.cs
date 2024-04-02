public class Trail
{
    public Location startNode;
    public Location destinationNode;

    public bool biDirectional;

    public int stepValue;

    public Trail(Location startNode, Location destinationNode, bool biDirectional, int stepValue)
    {
        this.startNode = startNode;
        this.destinationNode = destinationNode;
        this.biDirectional = biDirectional;
        this.stepValue = stepValue;
    }


}