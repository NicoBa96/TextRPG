public abstract class AQuestGoal
{

    public Quest quest;
    protected float progressGoal;

    public string id;

    public AQuestGoal(Quest quest, string id)
    {
        this.quest = quest;
        this.id = id;
        progressGoal = 1;

    }
    public bool IsFullfilled()
    {
        return GetProgress() >= progressGoal;
    }

    public abstract string GetDescription();

    public void AddProgress(float increment)
    {
        bool questFullfilledBeforeIncrement = quest.IsFullfilled();
        AddGoalProgress(increment);
        if (!questFullfilledBeforeIncrement && quest.IsFullfilled())
        {
            OnQuestCompletion();
        }
    }

    private void OnQuestCompletion()
    {
        TextRPG.instance.player.questMemory.questStatus[quest.id] = QuestStatus.Finished;
        RPGWriter.Green($"You completed the quest {quest.GetSummary()}");
        quest.GiveCompletionRewards();
    }

    private void AddGoalProgress(float increment)
    {
        bool goalFullfilledBeforeIncrement = IsFullfilled();
        float newProgress = GetProgress() + increment;
        if (newProgress >= progressGoal)
        {
            newProgress = progressGoal;
        }
        TextRPG.instance.player.questMemory.SetQuestGoalProgress(this, newProgress);

        if (!goalFullfilledBeforeIncrement && newProgress >= progressGoal)
        {
            RPGWriter.Green($"You completed a subgoal of the quest {quest.name}: {this.GetDescription()}");
        }
    }



    public float GetProgress()
    {
        return TextRPG.instance.player.questMemory.GetQuestGoalProgress(this);
    }
}