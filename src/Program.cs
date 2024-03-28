using System.Runtime.InteropServices;
using System.Text;

public class Program
{
  public static void Main(string[] args)
  {
    TextRPG game = new TextRPG();

    while (true)
    {
      int startInput = 1;//todo change back GetUserInput(1, 3, game.ShowMenu);
      if (startInput == 1)
      {
        game.Start();

        game.Exit();
      }
      else if (startInput == 2)
      {
        game.ShowCredits();
      }
      else if (startInput == 3)
      {
        game.Exit();
      }
      else
      {
        Console.WriteLine("Invalid Input!");
      }
    }
  }

  public delegate void ShowMenu();

  public static int GetUserInput(int min, int max, ShowMenu callback)
  {
    while (true)
    {
      callback.Invoke();
      string userInput = Console.ReadLine()!;
      try
      {
        int choice = Int32.Parse(userInput);
        if (choice >= min && choice <= max)
        {
          return choice;
        }
        throw new Exception();
      }
      catch (System.Exception)
      {

        Console.WriteLine("Invalid Input! Try again.");
      }
    }
  }


}