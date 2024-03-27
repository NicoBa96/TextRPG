public class Edge
{
    public Node startNode;
    public Node destinationNode;

    public bool biDirectional;

    public int stepValue;

    public Edge(Node startNode, Node destinationNode, bool directional, int stepValue)
    {
        this.startNode = startNode;
        this.destinationNode = destinationNode;
        biDirectional = directional;
        this.stepValue = stepValue;
    }


}