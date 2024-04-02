public class RPGWriter
{
    public static void Color(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void Green(string text)
    {
        Color(text, ConsoleColor.Green);
    }

    public static void Yellow(string text)
    {
      Color(text, ConsoleColor.Yellow);
    }
}