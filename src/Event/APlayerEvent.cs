public abstract class APlayerEvent : AGameEvent
{

    public APlayerEvent(Player player) : base()
    {
     this.player = player; 
    }
    protected Player player;

}