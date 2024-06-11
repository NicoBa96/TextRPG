public class OnDeathEventCondition : AEventCondition
{

    public OnDeathEventCondition() : base()
    {
    }

    public override bool IsFullfilled()
    {
        return TextRPG.instance.player.GetStamina() <= 0;
    }
}