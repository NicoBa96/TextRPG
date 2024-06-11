public class ItemQuestReward : IQuestReward
{
    AItem item;
    int itemAmount;
    public ItemQuestReward(AItem item, int itemAmount)
    {
        this.item = item;
        this.itemAmount = itemAmount;
    }

    public void Grant()
    {
        TextRPG.instance.player.inventory.AddItem(item, itemAmount);
    }
}