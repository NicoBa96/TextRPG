public class RPGWriter
{
    public static void Color(string text, ConsoleColor color, bool newLine = true)
    {
        Console.ForegroundColor = color;
        if (newLine)
        {
            Console.WriteLine(text);
        }
        else
        {
            Console.Write(text);
        }
        Console.ResetColor();
    }

    public static void Green(string text, bool newLine = true)
    {
        Color(text, ConsoleColor.Green, newLine);
    }

    public static void Yellow(string text, bool newLine = true)
    {
        Color(text, ConsoleColor.Yellow, newLine);
    }

    public static void Blue(string text, bool newLine = true)
    {
        Color(text, ConsoleColor.Blue, newLine);
    }

    public static void Red(string text, bool newLine = true)
    {
        Color(text, ConsoleColor.Red, newLine);
    }

    public static void DarkGray(string text, bool newLine = true)
    {
        Color(text, ConsoleColor.DarkGray, newLine);
    }

    public static void Default(string text, bool newLine = true)
    {
        Color(text, ConsoleColor.Gray, newLine);
    }

    public static void LineBreak()
    {
        RPGWriter.Default("");
    }

    public static void EventText(string text, bool newLine = true)
    {
        Color(text, ConsoleColor.Green, newLine);
    }

    public static void Gain(string text, bool newLine = true)
    {
        Color("+ " + text, ConsoleColor.Green, newLine);
    }

    public static void Decrease(string text, bool newLine = true)
    {
        Color("- " + text, ConsoleColor.Red, newLine);
    }
}