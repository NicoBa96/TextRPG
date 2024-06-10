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
        float newProgress = GetProgress() + increment;
        if (newProgress >= progressGoal)
        {
            newProgress = progressGoal;
        }
        TextRPG.instance.player.questMemory.SetQuestGoalProgress(this, newProgress);
    }

    public float GetProgress()
    {
    return TextRPG.instance.player.questMemory.GetQuestGoalProgress(this);
    }
}