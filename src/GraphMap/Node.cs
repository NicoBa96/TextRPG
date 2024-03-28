public class Node
{

    public string name;

    public string description;
    public ConsoleColor color;
    public int xPos;
    public int yPos;

    public Node(int xPos, int yPos, ConsoleColor color, string nodeName, string descriptionText)
    {
        name = nodeName;
        description = descriptionText;
        this.xPos = xPos;
        this.yPos = yPos;
        this.color = color;
    }

    
}