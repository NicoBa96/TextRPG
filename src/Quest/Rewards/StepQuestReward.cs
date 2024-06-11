public class StepQuestReward : IQuestReward
{
    int stepAmount;
    public StepQuestReward(int stepAmount)
    {
        this.stepAmount = stepAmount;
    }

    public void Grant()
    {
        TextRPG.instance.player.AddSteps(stepAmount);
    }
}