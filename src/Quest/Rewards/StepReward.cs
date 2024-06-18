public class StepReward : IReward
{
    int stepAmount;
    public StepReward(int stepAmount)
    {
        this.stepAmount = stepAmount;
    }

    public void Grant()
    {
        TextRPG.instance.player.AddSteps(stepAmount);
    }
}