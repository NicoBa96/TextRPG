using System.Runtime.InteropServices;
using System.Text;

public class Program
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
}