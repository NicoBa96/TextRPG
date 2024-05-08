public class ByChanceCondition : AEventCondition
{

    float chanceValue;

    public ByChanceCondition(float chanceValue)
    {
        this.chanceValue = chanceValue;
    }

    public override bool IsFullfilled()
    {
        return Program.GetRandomNumber() <= chanceValue;
    }
}