public class StaminaAmountGoal : AQuestGoal
{
  public StaminaAmountGoal(Quest quest, string id, bool isExact, int staminaAmount) : base(quest, id, isExact)
    {
        this.progressGoal = staminaAmount;
    }

    public override string GetDescription()
    {
        return $"Get your stamina to {progressGoal}. (Currently: {GetProgress()})";
    }
}