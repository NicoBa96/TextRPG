public class RunningBootsEvent : APlayerEvent
{

    public RunningBootsEvent() : base()
    {

    }
    public override void Action()
    {
        RPGWriter.LineBreak();
        RPGWriter.Green("You find a small shoe store");
        RPGWriter.Green("Upon hearing about your plans of becoming the biggest stepper, the shop owner offers you a free shoe upgrade.");
        RPGWriter.Green("As every step takes less effort now, you can make more steps on the same distance.");
        TextRPG.instance.player.ChangeStepFactor((float)0.2);
        RPGWriter.LineBreak();
    }

}