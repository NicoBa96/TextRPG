public class DesertDamageEvent : APlayerEvent
{

    public DesertDamageEvent(Player player) : base(player)
    {

    }
    public override void Action()
    {
        RPGWriter.LineBreak();
        RPGWriter.Red("The heat is too much for you.");
        player.Damage(5);
        RPGWriter.LineBreak();
    }

}