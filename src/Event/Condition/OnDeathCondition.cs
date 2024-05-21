public class OnDeathCondition : APlayerEventCondition
{

    public OnDeathCondition(Player player) : base(player)
    {
        this.player = player;
    }

    public override bool IsFullfilled()
    {
        return player.GetHealth() <= 0;
    }
}