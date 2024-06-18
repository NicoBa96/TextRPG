public class GameEvent
{
    public List<AEventCondition> conditions;

    public List<string> eventText;

    List<IReward> rewards;

    public GameEvent()
    {
        conditions = new List<AEventCondition>();
        eventText = new List<string>();
        rewards = new List<IReward>();
    }
    public GameEvent AddCondition(AEventCondition condition)
    {
        conditions.Add(condition);
        return this;
    }



    public GameEvent AddText(string text)
    {
        eventText.Add(text);
        return this;
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

    public virtual void Action()
    {
        eventText.ForEach(t => RPGWriter.Green(t));
        rewards.ForEach(r => r.Grant());
    }

    #region ConditionAdders

    public GameEvent OnChance(float f)
    {
        conditions.Add(new ByChanceEventCondition(f));
        return this;
    }

    public GameEvent OnItem(AItem item, int itemCount = 1)
    {
        conditions.Add(new ItemEventCondition(item, itemCount));
        return this;
    }

    public GameEvent OnLocationReveal(params Location[] nodes)
    {
        conditions.Add(new LocationRevealEventCondition(nodes));
        return this;
    }

    public GameEvent OnMilestoneCompletion(params Milestone[] milestones)
    {
        conditions.Add(new MilestoneCompletionEventCondition(milestones));
        return this;
    }

    public GameEvent OnNotRevealed(params Location[] nodes)
    {
        conditions.Add(new NotRevealedEventCondition(nodes));
        return this;
    }

    public GameEvent OnDeath()
    {
        conditions.Add(new OnDeathEventCondition());
        return this;
    }

    public GameEvent OnQuestStatus(QuestIdentifier questIdentifier, QuestStatus questStatus)
    {
        conditions.Add(new QuestStatusEventCondition(questIdentifier, questStatus));
        return this;
    }

    public GameEvent OnStamina(int staminaConditionValue)
    {
        conditions.Add(new StaminaEventCondition(staminaConditionValue));
        return this;
    }

    public GameEvent OnStepCount(int stepThreshold)
    {
        conditions.Add(new StepCountEventCondition(stepThreshold));
        return this;
    }

    public GameEvent OnStepFactor(float stepFactorThreshold)
    {
        conditions.Add(new StepFactorEventCondition(stepFactorThreshold));
        return this;
    }

    #endregion 

    #region AddRewards

    public GameEvent GrantSteps(int stepAmount)
    {
        rewards.Add(new StepReward(stepAmount));
        return this;
    }

    public GameEvent GrantStamina(int staminaAmount)
    {
        rewards.Add(new StaminaReward(staminaAmount));
        return this;
    }

    public GameEvent GrantLocationReveal(Location location)
    {
        rewards.Add(new LocationRevealReward(location));
        return this;
    }

    public GameEvent GrantQuestStart(QuestIdentifier questIdentifier)
    {
        OnQuestStatus(questIdentifier, QuestStatus.Open);
        rewards.Add(new StartQuestReward(questIdentifier));
        return this;
    }

    public GameEvent GrantLocationSet(Location location)
    {
        rewards.Add(new SetCurrentLocationReward(location));
        return this;
    }

    public GameEvent GrantItem(AItem item, int itemAmount = 1)
    {
        rewards.Add(new ItemReward(item, itemAmount));
        return this;
    }

    #endregion

}