public class StepCountGoal : AQuestGoal
{

    public StepCountGoal(Quest quest, string id, bool isExact, int stepAmount) : base(quest, id, isExact)
    {
        this.progressGoal = stepAmount;
    }

    public override string GetDescription()
    {
        return $"Take {progressGoal} steps. (Currently: {GetProgress()})";
    }
}