public class ItemReward : IReward
{
    AItem item;
    int itemAmount;
    public ItemReward(AItem item, int itemAmount)
    {
        this.item = item;
        this.itemAmount = itemAmount;
    }

    public void Grant()
    {
        TextRPG.instance.player.inventory.AddItem(item, itemAmount);
    }
}