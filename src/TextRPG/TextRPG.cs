using System.Text;

public class TextRPG
{

    LinkedList<Node> map;
    LinkedList<Edge> edges;
    public TextRPG()
    {
        map = new LinkedList<Node>();
        edges = new LinkedList<Edge>();
        CreateMap();
    }


    public void ShowMenu()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Welcome to [The Big Step]! Please type:");
        stringBuilder.AppendLine("1 - Play the Game");
        stringBuilder.AppendLine("2 - Show Credits");
        stringBuilder.AppendLine("3 - Exit the Game");
        Console.WriteLine(stringBuilder.ToString());
    }

    public void Start()
    {
        Player currentPlayer = new Player();
        Console.WriteLine(">>Game starts<<");
        Console.WriteLine("Name: " + currentPlayer.GetName() + ", starting Health: " + currentPlayer.GetHealth() + ", walked distance: " + currentPlayer.GetWalkedSteps() + ".");
    }

    public void Exit()
    {
        Environment.Exit(0);
    }

    public void ShowCredits()
    {
        Console.WriteLine("Credits");
        Console.WriteLine("Lead Developer: Nico B.");
        Console.WriteLine("Assistant: Joshua S.");
        Console.WriteLine("");
    }

    public void CreateMap()
    {
        Node cityCentre = CreateNode("cityCentre", "The main square of a large city");
        Node forest = CreateNode("Forest", "A dark forest filled with trees");
        Node mountains = CreateNode("Mountains", "Large mountain peaks with a cold climate surrounding them");
        Node coast = CreateNode("Coast", "Vast coastline seperating the land from the endless sea");

        CreatEdge(cityCentre, forest, true);
        CreatEdge(cityCentre, mountains, false);
        CreatEdge(cityCentre, coast, false);
        CreatEdge(forest, mountains, true);
        CreatEdge(forest, coast, false);
        CreatEdge(mountains, coast, true);
    }

    public Node CreateNode(string name, string description)
    {
        Node node = new Node(name, description);
        map.Append(node);
        return node;
    }

    public void CreatEdge(Node a, Node b, bool biDirectional)
    {
        Edge edge = new Edge(a, b, biDirectional);
        edges.Append(edge);
    }

}