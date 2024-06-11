public class DesertDamageEvent : AGameEvent
{

    public DesertDamageEvent() : base()
    {

    }
    public override void Action()
    {
        RPGWriter.LineBreak();
        RPGWriter.Red("The heat is too much for you.");
        TextRPG.instance.player.Exhaust(5);
        RPGWriter.LineBreak();
    }

}