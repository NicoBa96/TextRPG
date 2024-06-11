public abstract class AGameEvent
{
    public List<AEventCondition> conditions;

    public List<string> eventText;

    public AGameEvent()
    {
        conditions = new List<AEventCondition>();
        eventText = new List<string>();
    }
    public AGameEvent AddCondition(AEventCondition condition)
    {
        conditions.Add(condition);
        return this;
    }



    public AGameEvent AddText(string text)
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
        foreach (string s in eventText)
        {
            RPGWriter.Green(s);
        }
    }

    #region EventAdders

    public AGameEvent OnChance(float f)
    {
        conditions.Add(new ByChanceEventCondition(f));
        return this;
    }

    public AGameEvent OnItem(AItem item, int itemCount = 1)
    {
        conditions.Add(new ItemEventCondition(item, itemCount));
        return this;
    }

    public AGameEvent OnLocationReveal(params Location[] nodes)
    {
        conditions.Add(new LocationRevealEventCondition(nodes));
        return this;
    }

    public AGameEvent OnMilestoneCompletion(params Milestone[] milestones)
    {
        conditions.Add(new MilestoneCompletionEventCondition(milestones));
        return this;
    }

    public AGameEvent OnNotRevealed(params Location[] nodes)
    {
        conditions.Add(new NotRevealedEventCondition(nodes));
        return this;
    }

    public AGameEvent OnDeath()
    {
        conditions.Add(new OnDeathEventCondition());
        return this;
    }

    public AGameEvent OnQuestStatus(Quest quest, QuestStatus questStatus)
    {
        conditions.Add(new QuestStatusEventCondition(quest, questStatus));
        return this;
    }

    public AGameEvent OnStamina(int staminaConditionValue)
    {
        conditions.Add(new StaminaEventCondition(staminaConditionValue));
        return this;
    }

    public AGameEvent OnStepCount(int stepThreshold)
    {
        conditions.Add(new StepCountEventCondition(stepThreshold));
        return this;
    }

    public AGameEvent OnStepFactor(int stepFactorThreshold)
    {
        conditions.Add(new StepCountEventCondition(stepFactorThreshold));
        return this;
    }

    #endregion 

}