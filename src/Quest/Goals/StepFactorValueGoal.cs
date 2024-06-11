public class StepFactorValueGoal : AQuestGoal
{
public StepFactorValueGoal(Quest quest, string id, bool isExact, float stepFactorValue) : base(quest, id, isExact)
    {
        this.progressGoal = stepFactorValue;
    }

    public override string GetDescription()
    {
        return $"Get your step factor to {progressGoal}. (Currently: {GetProgress()})";
    }
}