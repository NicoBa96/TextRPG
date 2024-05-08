public abstract class AGameEvent
{
    List<AEventCondition> conditions;

    public AGameEvent()
    {
        conditions = new List<AEventCondition>();
    }
    public void AddCondition(AEventCondition condition)
    {
        conditions.Add(condition);
    }

    public bool AllConditionsFullfilled()
    {
        foreach (AEventCondition c in conditions)
        {
            if (!c.IsFullfilled())
            {
                return false;
            }
        }
        return true;
    }

    public abstract void Action();

}