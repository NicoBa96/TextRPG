public class StaminaQuestReward : IQuestReward
{
    int staminaAmount;
    public StaminaQuestReward(int staminaAmount)
    {
        this.staminaAmount = staminaAmount;
    }

    public void Grant()
    {
        TextRPG.instance.player.Replenish(staminaAmount);
    }
}