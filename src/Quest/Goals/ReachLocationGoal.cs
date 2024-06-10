public class ReachLocationGoal : AQuestGoal
{
    public Location goalLocation;

    public ReachLocationGoal(Quest quest, string id,Location goalLocation) : base(quest, id)
    {
     this.goalLocation = goalLocation;

    }

    public override string GetDescription()
    {
     return $"Reach {goalLocation.name}";
    }
}