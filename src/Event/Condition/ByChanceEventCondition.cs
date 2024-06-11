public class ByChanceEventCondition : AEventCondition
{

    float chanceValue;

    public ByChanceEventCondition(float chanceValue)
    {
        this.chanceValue = chanceValue;
    }

    public override bool IsFullfilled()
    {
        return Program.GetRandomNumber() <= chanceValue;
    }
}