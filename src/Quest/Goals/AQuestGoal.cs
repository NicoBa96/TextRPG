public abstract class AQuestGoal
{

    public Quest quest;
    public bool isExact;
    public string id;

    protected float progressGoal;

    public AQuestGoal(Quest quest, string id, bool isExact)
    {
        this.quest = quest;
        this.id = id;
        progressGoal = 1;
        this.isExact = isExact;
    }

    public bool IsFullfilled()
    {
        return GetProgress() >= progressGoal;
    }

    public abstract string GetDescription();

    public void ChangeProgress(float delta)
    {
        bool questFullfilledBeforeDelta = quest.IsFullfilled();
        ChangeGoalProgress(delta);
        if (!questFullfilledBeforeDelta && quest.IsFullfilled())
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

    private void ChangeGoalProgress(float delta)
    {
        bool goalFullfilledBeforeIncrement = IsFullfilled();
        if (goalFullfilledBeforeIncrement)
        {
            return;
        }

        float newProgress = GetProgress() + delta;
        if (!isExact && newProgress >= progressGoal)
        {
            newProgress = progressGoal;
        }
        else if (newProgress >= 0)
        {
            newProgress = 0;
        }



        TextRPG.instance.player.questMemory.SetQuestGoalProgress(this, newProgress);

        if (!goalFullfilledBeforeIncrement && ((!isExact && newProgress >= progressGoal) || (isExact && newProgress == progressGoal)))
        {
            RPGWriter.Green($"You completed a subgoal of the quest {quest.name}: {this.GetDescription()}");
        }
    }



    public float GetProgress()
    {
        return TextRPG.instance.player.questMemory.GetQuestGoalProgress(this);
    }
}