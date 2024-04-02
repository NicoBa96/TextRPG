using System.Text;

public class Program
{
  public static void Main(string[] args)
  {
    Console.OutputEncoding = Encoding.UTF8;
    TextRPG game = new TextRPG();

    while (true)
    {

      int startInput = GetUserInput(1, 4, game.ShowMenu);
      Console.Clear();

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
      else if (startInput == 4)
      {
        MarathonEvent marathon = new MarathonEvent();
        marathon.Setup(game.player);
        marathon.Action();
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