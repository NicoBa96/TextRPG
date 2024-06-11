public class StepFactorEventCondition : AEventCondition
{

    float stepFactorThreshold;

    public StepFactorEventCondition(float stepFactorThreshold) : base()
    {
        this.stepFactorThreshold = stepFactorThreshold;
    }

    public override bool IsFullfilled()
    {
        return TextRPG.instance.player.GetStepFactor() >= stepFactorThreshold;
    }
}