public class StepCountCondition : APlayerEventCondition
{

    int stepThreshold;

    public StepCountCondition(int stepFactorThreshold) : base()
    {
        this.stepThreshold = stepFactorThreshold;
    }

    public override bool IsFullfilled()
    {
        return TextRPG.instance.player.GetStepFactor() >= stepThreshold;
    }
}