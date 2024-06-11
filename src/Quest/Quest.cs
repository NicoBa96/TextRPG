using System.Reflection.Metadata;
using System.Text;

public class Quest
{
    public QuestIdentifier id;
    public string name;
    public string description;
    public List<AQuestGoal> goals;
    public List<IQuestReward> rewards;

    public Quest(QuestRegistry questRegistry, QuestIdentifier id)
    {
        this.id = id;
        goals = new List<AQuestGoal>();
        rewards = new List<IQuestReward>();
        questRegistry.AddQuest(this);
    }

    public Quest SetName(string name)
    {
        this.name = name;
        return this;
    }

    public Quest SetDescription(string description)
    {
        this.description = description;
        return this;
    }

    public string GetSummary()
    {
        return $"{name} - {description}";
    }

    public bool IsFullfilled()
    {
        return goals.All(g => g.IsFullfilled());
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(GetSummary());
        foreach (AQuestGoal questGoal in goals)
        {
            if (questGoal.IsFullfilled())
            {
                stringBuilder.Append("\t[X] ");
            }
            else
            {
                stringBuilder.Append("\t[ ] ");
            }

            stringBuilder.AppendLine(questGoal.GetDescription());
        }

        return stringBuilder.ToString();
    }

    public Quest AddReachLocationGoal(string id, Location location)
    {
        ReachLocationGoal reachLocationGoal = new ReachLocationGoal(this, id, location);
        goals.Add(reachLocationGoal);
        return this;
    }

    public Quest AddStepFactorQuestReward(float stepFactorAmount)
    {
      StepFactorQuestReward stepFactorQuestReward = new StepFactorQuestReward(stepFactorAmount);
      rewards.Add(stepFactorQuestReward);
      return this;
    }


    internal void GiveCompletionRewards()
    {
        RPGWriter.Green("You recieve the following quest rewards:");
        rewards.ForEach(r => r.Grant());
        RPGWriter.LineBreak();
    }
}