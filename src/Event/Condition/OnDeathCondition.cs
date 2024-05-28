public class OnDeathCondition : APlayerEventCondition
{

    public OnDeathCondition() : base()
    {
    }

    public override bool IsFullfilled()
    {
        return TextRPG.instance.player.GetHealth() <= 0;
    }
}