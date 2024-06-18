public class StaminaReward : IReward
{
    int staminaAmount;
    public StaminaReward(int staminaAmount)
    {
        this.staminaAmount = staminaAmount;
    }

    public void Grant()
    {
        TextRPG.instance.player.ChangeStamina(staminaAmount);
    }
}