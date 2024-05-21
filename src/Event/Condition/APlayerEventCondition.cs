public abstract class APlayerEventCondition : AEventCondition
{
    protected Player player;

    public APlayerEventCondition(Player player)
    {
     this.player = player;
    }
}