public class ByStepFactorCondition : APlayerEventCondition
{

    float stepFactorThreshold;

    public ByStepFactorCondition(Player player, float stepFactorThreshold) : base(player)
    {
        this.stepFactorThreshold = stepFactorThreshold;
    }

    public override bool IsFullfilled()
    {
        return player.GetStepFactor() >= stepFactorThreshold;
    }
}