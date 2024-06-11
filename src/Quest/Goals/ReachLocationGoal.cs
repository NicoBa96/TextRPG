public class ReachLocationGoal : AQuestGoal
{
    public Location goalLocation;

    public ReachLocationGoal(Quest quest, string id, bool isExact, Location goalLocation) : base(quest, id, isExact)
    {
     this.goalLocation = goalLocation;
    }

    public override string GetDescription()
    {
     return $"Reach {goalLocation.name}";
    }
}