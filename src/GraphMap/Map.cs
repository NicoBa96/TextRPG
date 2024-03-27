public class Map
{

    List<Node> map;
    List<Edge> edges;

    public Node currentNode;


    public Map()
    {
        map = new List<Node>();
        edges = new List<Edge>();
        CreateMap();
    }

    public void CreateMap()
    {
        Node cityCentre = CreateNode("cityCentre", "The main square of a large city");
        Node forest = CreateNode("Forest", "A dark forest filled with trees");
        Node mountains = CreateNode("Mountains", "Large mountain peaks with a cold climate surrounding them");
        Node coast = CreateNode("Coast", "Vast coastline seperating the land from the endless sea");

        currentNode = cityCentre;

        CreatEdge(cityCentre, forest, true, 10);
        CreatEdge(cityCentre, mountains, false, 30);
        CreatEdge(cityCentre, coast, false, 60);
        CreatEdge(forest, mountains, true, 20);
        CreatEdge(forest, coast, false, 50);
        CreatEdge(mountains, coast, true, 30);
    }

    public Node CreateNode(string name, string description)
    {
        Node node = new Node(name, description);
        map.Add(node);
        return node;
    }

    public void CreatEdge(Node startNode, Node destinationNode, bool biDirectional, int stepValue)
    {
        Edge edge = new Edge(startNode, destinationNode, biDirectional, stepValue);
        edges.Add(edge);
    }

    public List<Edge> GetPaths()
    {
        List<Edge> paths = new List<Edge>();
        foreach (Edge edge in edges)
        {
            if (edge.startNode == currentNode || (edge.destinationNode == currentNode && edge.biDirectional))
            {
              paths.Add(edge);
            }

        }
        return paths;
    }

}