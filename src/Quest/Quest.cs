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

    #region AddGoals
    public Quest AddReachLocationGoal(string id, Location location, bool isExact = false)
    {
        ReachLocationGoal reachLocationGoal = new ReachLocationGoal(this, id, isExact, location);
        goals.Add(reachLocationGoal);
        return this;
    }

    public Quest AddRecieveItemGoal(string id, AItem item, int itemAmount, bool isExact = false)
    {
        RecieveItemGoal recieveItemGoal = new RecieveItemGoal(this, id, isExact, item, itemAmount);
        goals.Add(recieveItemGoal);
        return this;
    }

    public Quest AddStaminaAmountGoal(string id, int staminaAmount, bool isExact = false)
    {
        StaminaAmountGoal staminaAmountGoal = new StaminaAmountGoal(this, id, isExact, staminaAmount);
        goals.Add(staminaAmountGoal);
        return this;
    }

    public Quest AddStepCountGoal(string id, int stepCount, bool isExact = false)
    {
        StepCountGoal stepCountGoal = new StepCountGoal(this, id, isExact, stepCount);
        goals.Add(stepCountGoal);
        return this;
    }

    public Quest AddStepFactorValueGoal(string id, float factorValue, bool isExact = false)
    {
        StepFactorValueGoal stepFactorValueGoal = new StepFactorValueGoal(this, id, isExact, factorValue);
        goals.Add(stepFactorValueGoal);
        return this;
    }
    #endregion

    #region AddRewards
    public Quest AddStepFactorQuestReward(float stepFactorAmount)
    {
        StepFactorQuestReward stepFactorQuestReward = new StepFactorQuestReward(stepFactorAmount);
        rewards.Add(stepFactorQuestReward);
        return this;
    }

    public Quest AddItemQuestReward(AItem item, int amount)
    {
        ItemQuestReward itemQuestReward = new ItemQuestReward(item, amount);
        rewards.Add(itemQuestReward);
        return this;
    }

    public Quest AddStaminaQuestReward(int staminaAmount)
    {
        StaminaQuestReward staminaQuestReward = new StaminaQuestReward(staminaAmount);
        rewards.Add(staminaQuestReward);
        return this;
    }

    public Quest AddStepQuestReward(int stepAmount)
    {
        StepQuestReward stepQuestReward = new StepQuestReward(stepAmount);
        rewards.Add(stepQuestReward);
        return this;
    }
    #endregion

    internal void GiveCompletionRewards()
    {
        RPGWriter.Green("You recieve the following quest rewards:");
        rewards.ForEach(r => r.Grant());
        RPGWriter.LineBreak();
    }
}