using System.Runtime.InteropServices;
using System.Text;

public class TextRPG
{

  LinkedList<Node> map;
  TextRPG()
  {
    map = new LinkedList<Node>();
    CreateNodes();
  }
  public static void Main(string[] args)
  {
    TextRPG game = new TextRPG();

    while (true)
    {
      game.ShowMenu();
      string startInput = Console.ReadLine()!;
      if (startInput == "1")
      {
        game.Start();
        game.Exit();
      }
      else if (startInput == "2")
      {
        game.ShowCredits();
      }
      else if (startInput == "3")
      {
        game.Exit();
      }
      else
      {
        Console.WriteLine("Invalid Input!");
      }
    }
  }

  void ShowMenu()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("Welcome to [The Big Step]! Please type:");
    stringBuilder.AppendLine("1 - Play the Game");
    stringBuilder.AppendLine("2 - Show Credits");
    stringBuilder.AppendLine("3 - Exit the Game");
    Console.WriteLine(stringBuilder.ToString());
  }

  void Start()
  {
    Player currentPlayer = new Player();
    Console.WriteLine(">>Game starts<<");
    Console.WriteLine("Name: " + currentPlayer.GetName() + ", starting Health: " + currentPlayer.GetHealth() + ", walked distance: " + currentPlayer.GetWalkedSteps() + ".");
  }

  void Exit()
  {
    Environment.Exit(0);
  }

  void ShowCredits()
  {
    Console.WriteLine("Credits");
    Console.WriteLine("Lead Developer: Nico B.");
    Console.WriteLine("Assistant: Joshua S.");
    Console.WriteLine("");
  }

  void CreateNodes()
  {
    Node cityCentre = new Node("City Centre", "The main square of a large city");
    map.Append(cityCentre);
    Node forest = new Node("Forest", "A dark forest filled with trees");
    map.Append(forest);
    Node mountains = new Node("Mountains", "Large mountain peaks with a cold climate surrounding them");
    map.Append(mountains);
    Node coast = new Node("Coast", "Vast coastline seperating the land from the endless sea");
    map.Append(coast);

  }
}