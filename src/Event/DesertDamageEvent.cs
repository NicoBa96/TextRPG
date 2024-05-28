public class DesertDamageEvent : APlayerEvent
{

    public DesertDamageEvent() : base()
    {

    }
    public override void Action()
    {
        RPGWriter.LineBreak();
        RPGWriter.Red("The heat is too much for you.");
        TextRPG.instance.player.Damage(5);
        RPGWriter.LineBreak();
    }

}