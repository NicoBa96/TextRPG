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

    public AGameEvent AddByChanceCondition(float f)
    {
        conditions.Add(new ByChanceCondition(f));
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

}