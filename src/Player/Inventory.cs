using System.Text.Json.Serialization;
public class Inventory
{

    [JsonInclude]
    public Dictionary<int, int> inventory;

    public Inventory()
    {
        inventory = new Dictionary<int, int>();
        AddItemInternal(Items.BasicLack, 2);
        AddItemInternal(Items.AdvancedLack);
        AddItemInternal(Items.GoldenKey);
    }

    public void AddItem(AItem i, int increase = 1)
    {
        AddItemInternal(i, increase);
        RPGWriter.Gain($"{increase}x {i.name}");
        TextRPG.instance.player.UpdateRecieveItemGoal(i, increase);
    }

    private void AddItemInternal(AItem i, int increase = 1)
    {
        if (inventory.TryGetValue(i.id, out int value))
        {
            inventory[i.id] = value + increase;
        }
        else
        {
            inventory[i.id] = increase;
        }
    }

    public void RemoveItem(AItem i, int decrease = 1)
    {
        if (inventory.TryGetValue(i.id, out int value))
        {
            inventory[i.id] = value - decrease;
            if (inventory[i.id] <= 0)
            {
                inventory.Remove(i.id);
            }

            int removedItemAmount = decrease <= value ? decrease : value;
            RPGWriter.Decrease($"{removedItemAmount}x {i.name}");
        }
    }

    public bool HasItem(AItem i, int count = 1)
    {
        inventory.TryGetValue(i.id, out int value);
        return value >= count;
    }

    public void UseItem(AUsableItem i, int amount = 1)
    {
        for (int r = 0; r < amount; r++)
        {
            if (inventory.TryGetValue(i.id, out int value))
            {
                i.Use();
            }
            else
            {
                RPGWriter.Red("You cant use " + i.name + ", because you dont have any in your inventory");
            }
        }
    }

    public Dictionary<AItem, int> GetCurrentItems()
    {
        Dictionary<AItem, int> currentItems = new Dictionary<AItem, int>();
        foreach (int key in inventory.Keys)
        {
            currentItems.Add(Items.GetItembyId(key), inventory[key]);
        }

        return currentItems;
    }
}