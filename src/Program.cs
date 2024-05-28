using System.Text;

public class Program
{
  public static Random randomGenerator;
  public static void Main(string[] args)
  {
    Console.OutputEncoding = Encoding.UTF8;
    TextRPG game = new TextRPG();
    game._mainMenu.CreateMainMenu().HandleInput();
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