public abstract class AQuestGoal
{

    protected float progress;
    protected float progressGoal;

    public AQuestGoal()
    {
        progress = 0;
        progressGoal = 1;
    }
    public bool IsFullfilled()
    {
        return progress == progressGoal;
    }

    public abstract string GetDescription();
}