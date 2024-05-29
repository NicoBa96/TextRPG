public class Quest
{
    public static List<Quest> allQuests = new List<Quest>();
    public int id;
    string name;
    string description;
    public List<AQuestGoal> goals;

    public Quest(int id)
    {
        this.id = id;
        allQuests.Add(this);
        goals = new List<AQuestGoal>();
    }

    public Quest AddReachLocationGoal(Location location)
    {
        ReachLocationGoal reachLocationGoal = new ReachLocationGoal(location);
        goals.Add(reachLocationGoal);
        return this;
    }
}