public class CatChaseEvent : APlayerEvent
{

    public CatChaseEvent(Player player) : base(player)
    {

    }
    public override void Action()
    {
        RPGWriter.LineBreak();
        RPGWriter.Green("You are told that a cat is missing in this area.");
        RPGWriter.Green("After suddenly encountering the cat you chase it for a while until you catch it");
        RPGWriter.Green("After returning the cat to its owners, the smile on their face is enough payment for you");
        player.AddSteps(500);
        RPGWriter.LineBreak();
    }

}