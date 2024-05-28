public abstract class AUsableItem : AItem
{

    public AUsableItem(int id, string name, string description) : base(id, name, description)
    {

    }

    public void Use()
    {
        OnUse();
        TextRPG.instance.player.inventory.RemoveItem(this);
    }

    public abstract void OnUse();
}