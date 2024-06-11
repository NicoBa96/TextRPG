public class ItemEventCondition : AEventCondition
{
    AItem conditionItem;
    int conditionItemCount;

    public ItemEventCondition(AItem conditionItem, int conditionItemCount) : base()
    {
        this.conditionItem = conditionItem;
        this.conditionItemCount = conditionItemCount;
    }

    public override bool IsFullfilled()
    {
     return TextRPG.instance.player.inventory.inventory[conditionItem.id] >= conditionItemCount;
    }
}