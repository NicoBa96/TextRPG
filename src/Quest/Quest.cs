using System.Reflection.Metadata;
using System.Text;

public class Quest
{
    public QuestIdentifier id;
    public string name;
    public string description;
    public List<string> startQuestText;
    public List<string> endQuestText;
    public List<AQuestGoal> goals;
    public List<IReward> rewards;

    public Quest(QuestRegistry questRegistry, QuestIdentifier id)
    {
        this.id = id;
        goals = new List<AQuestGoal>();
        rewards = new List<IReward>();
        startQuestText = new List<string>();
        endQuestText = new List<string>();
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

    public Quest AddStartQuestText(string questTextLine)
    {
        startQuestText.Add(questTextLine);
        return this;
    }

    public Quest AddEndQuestText(string questTextLine)
    {
        endQuestText.Add(questTextLine);
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

    public Quest AddRecieveItemGoal(string id, AItem item, int itemAmount, bool consumeItemOnCompletion, bool isExact = false)
    {
        RecieveItemGoal recieveItemGoal = new RecieveItemGoal(this, id, isExact, item, itemAmount, consumeItemOnCompletion);
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
        StepFactorReward stepFactorQuestReward = new StepFactorReward(stepFactorAmount);
        rewards.Add(stepFactorQuestReward);
        return this;
    }

    public Quest AddItemQuestReward(AItem item, int amount)
    {
        ItemReward itemQuestReward = new ItemReward(item, amount);
        rewards.Add(itemQuestReward);
        return this;
    }

    public Quest AddStaminaQuestReward(int staminaAmount)
    {
        StaminaReward staminaQuestReward = new StaminaReward(staminaAmount);
        rewards.Add(staminaQuestReward);
        return this;
    }

    public Quest AddStepQuestReward(int stepAmount)
    {
        StepReward stepQuestReward = new StepReward(stepAmount);
        rewards.Add(stepQuestReward);
        return this;
    }
    #endregion

    internal void GiveCompletionRewards()
    {
        IEnumerable<RecieveItemGoal> recieveItemGoals = goals.OfType<RecieveItemGoal>().Where(g => g.consumeItemOnCompletion);
        foreach (RecieveItemGoal goal in recieveItemGoals)
        {
            TextRPG.instance.player.inventory.RemoveItem(goal.desiredItem, goal.desiredItemAmount);
        }

        RPGWriter.Green("You recieve the following quest rewards:");
        rewards.ForEach(r => r.Grant());
        RPGWriter.LineBreak();
    }
}