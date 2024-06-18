public class StepFactorReward : IReward
{
    float stepFactorAmount;
    public StepFactorReward(float stepFactorAmount)
    {
     this.stepFactorAmount = stepFactorAmount;
    }

    public void Grant()
    {
        TextRPG.instance.player.ChangeStepFactor(stepFactorAmount);
        
    }
}