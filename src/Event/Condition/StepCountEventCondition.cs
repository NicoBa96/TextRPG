public class StepCountEventCondition : AEventCondition
{

    int stepThreshold;

    public StepCountEventCondition(int stepThreshold) : base()
    {
        this.stepThreshold = stepThreshold;
    }

    public override bool IsFullfilled()
    {
        return TextRPG.instance.player.GetStepFactor() >= stepThreshold;
    }
}