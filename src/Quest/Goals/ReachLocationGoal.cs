public class ReachLocationGoal : AQuestGoal
{
    Location goalLocation;

    public ReachLocationGoal(Location goalLocation) : base()
    {
     this.goalLocation = goalLocation;
    }

    public override string GetDescription()
    {
     return $"Reach {goalLocation.name}";
    }
}