public class StepCountCondition : APlayerEventCondition
{

    int stepThreshold;

    public StepCountCondition(Player player, int stepFactorThreshold) : base(player)
    {
        this.stepThreshold = stepFactorThreshold;
    }

    public override bool IsFullfilled()
    {
        return player.GetStepFactor() >= stepThreshold;
    }
}