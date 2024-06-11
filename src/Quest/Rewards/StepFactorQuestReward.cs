public class StepFactorQuestReward : IQuestReward
{
    float stepFactorAmount;
    public StepFactorQuestReward(float stepFactorAmount)
    {
     this.stepFactorAmount = stepFactorAmount;
    }

    public void Grant()
    {
        TextRPG.instance.player.ChangeStepFactor(stepFactorAmount);
        
    }
}