public class ShoeLackCream : AUsableItem
{
    float stepFactorIncrease;
    public ShoeLackCream(int id, string name, string description, float stepFactorIncrease) : base(id, name, description)
    {
        this.stepFactorIncrease = stepFactorIncrease;
    }

    public override void OnUse()
    {
        TextRPG.instance.player.ChangeStepFactor(stepFactorIncrease);
    }
}