public class Location
{

    public string name;

    public string description;
    public ConsoleColor color;
    public int xPos;
    public int yPos;

    public Location(int xPos, int yPos, ConsoleColor color, string name, string description)
    {
        this.name = name;
        this.description = description;
        this.xPos = xPos;
        this.yPos = yPos;
        this.color = color;
    }


}