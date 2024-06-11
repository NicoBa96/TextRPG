public class MarathonEvent : AGameEvent
{

    public MarathonEvent() : base()
    {

    }
    public override void Action()
    {
        RPGWriter.LineBreak();
        RPGWriter.Green("You are participating at the 10km marathon! Added steps to your stepcounter.");
        TextRPG.instance.player.AddSteps(10000);
        RPGWriter.LineBreak();
    }

}