public class MarathonEvent : APlayerEvent
{

    public override void Action()
    {
        Console.WriteLine();
        RPGWriter.Green("You are participating at the 10km city marathon! Added steps to your stepcounter.");
        int steps = player.AddSteps(10000);
        RPGWriter.Yellow("+ " + steps + " steps.");
        Console.WriteLine("");
    }

}