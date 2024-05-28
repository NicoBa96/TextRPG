public class ByStepFactorCondition : APlayerEventCondition
{

    float stepFactorThreshold;

    public ByStepFactorCondition(float stepFactorThreshold) : base()
    {
        this.stepFactorThreshold = stepFactorThreshold;
    }

    public override bool IsFullfilled()
    {
        return TextRPG.instance.player.GetStepFactor() >= stepFactorThreshold;
    }
}