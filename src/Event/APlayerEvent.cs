public abstract class APlayerEvent : AGameEvent
{

    protected Player player;
    public void Setup(Player player)
    {
     this.player = player;
    }

}