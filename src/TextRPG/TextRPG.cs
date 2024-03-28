using System.Drawing;
using System.Text;

public class TextRPG
{
    Map map;
    Player player;

    public TextRPG()
    {
        map = new Map();
        player = new Player();
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

    public void InGameMenu()
    {
        Console.WriteLine("Walked distance: " + player.GetWalkedSteps() + " , location: " + map.currentNode.name + ".");
        StringBuilder stringBuilder = new StringBuilder();
        List<Edge> paths = map.GetPaths();
        stringBuilder.AppendLine("Your possible destinations:");
        for (int i = 0; i < paths.Count; i++)
        {
            Edge currentEdge = paths.ElementAt(i);
            Node targetNode = currentEdge.destinationNode == map.currentNode ? currentEdge.startNode : currentEdge.destinationNode;
            stringBuilder.AppendLine("(" + (i + 1) + "): " + targetNode.name + ", Steps: " + currentEdge.stepValue);
        }
        stringBuilder.Append("Choose your destination by typing the corresponding number:");
        Console.WriteLine(stringBuilder.ToString());
        map.DrawMap();
    }

    public void Start()
    {
        Console.WriteLine(">>Game starts<<");
        Console.WriteLine("Name: " + player.GetName() + ", starting Health: " + player.GetHealth() + ".");

        while (true)
        {
            List<Edge> paths = map.GetPaths();
            int input = Program.GetUserInput(1, paths.Count, this.InGameMenu);
            Edge chosenEdge = paths.ElementAt(input - 1);
            player.AddSteps(chosenEdge.stepValue);
            map.currentNode = chosenEdge.destinationNode == map.currentNode ? chosenEdge.startNode : chosenEdge.destinationNode;
            StringBuilder destinationStringBuilder = new StringBuilder();
            destinationStringBuilder.AppendLine(map.currentNode.name);
            destinationStringBuilder.AppendLine(map.currentNode.description);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(destinationStringBuilder.ToString());
            Console.ResetColor();
        }
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

}