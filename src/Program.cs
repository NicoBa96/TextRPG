using System.Text;

public class Program
{
  public static Random randomGenerator;
  public static void Main(string[] args)
  {
    Console.OutputEncoding = Encoding.UTF8;
    TextRPG game = new TextRPG();

    while (true)
    {

      int startInput = GetUserInput(1, 4, game.ShowMainMenu);
      Console.Clear();

      if (startInput == 1)
      {
        game.Start();

      }
      else if (startInput == 2)
      {
        game.player.GrantMilestone(Milestone.WATCHCREDITS);
        game.ShowCredits();
      }
      else if (startInput == 3)
      {
        game.Exit();
      }
      else
      {
        RPGWriter.Red("Invalid Input!");
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
      RPGWriter.LineBreak();
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

        RPGWriter.Red("Invalid Input! Try again.");
      }
    }
  }

  public static float GetRandomNumber()
  {
    if (randomGenerator == null)
    {
      randomGenerator = new Random();
    }
    return (float)randomGenerator.NextDouble();
  }


}