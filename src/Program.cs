using System.Text;

public class TextRPG
{
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
    Console.WriteLine("Name: " + currentPlayer.playerName + ", starting Health: " + currentPlayer.playerHealth + ", step length: " + currentPlayer.playerStepLength + ".");
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
    Console.WriteLine("Fehler Test 123");
    Console.WriteLine("");
  }
}