public class StaminaEventCondition : AEventCondition
{
    int staminaConditionValue;

    public StaminaEventCondition(int staminaConditionValue) : base()
    {
     this.staminaConditionValue = staminaConditionValue;
    }

    public override bool IsFullfilled()
    {
        return TextRPG.instance.player.GetStamina() >= staminaConditionValue;
    }
}