using System.ComponentModel;

public class CatChaseEvent : AGameEvent
{

    public CatChaseEvent() : base()
    {

    }
    public override void Action()
    {
        RPGWriter.LineBreak();
        RPGWriter.Green("You are told that a cat is missing in this area.");
        RPGWriter.Green("After suddenly encountering the cat you chase it for a while until you catch it");
        RPGWriter.Green("After returning the cat to its owners, the smile on their face is enough payment for you");
        TextRPG.instance.player.AddSteps(500);
        RPGWriter.LineBreak();
    }

}