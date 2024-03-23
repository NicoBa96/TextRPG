public class Edge
{
    Node a;
    Node b;

    bool biDirectional;

    public Edge(Node a, Node b, bool directional)
    {
     this.a = a;
     this.b = b;
     biDirectional = directional;
    }


}