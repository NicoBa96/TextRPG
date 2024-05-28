public class InventoryMenu
{
    public bool OpenInventory()
    {
        CreateInventoryMenu().HandleInput();
        return true;
    }

    private SelectionMenu CreateInventoryMenu()
    {
        SelectionMenu menu = new SelectionMenu(() =>
                {
                    RPGWriter.Blue("Your unusable items:");
                    Dictionary<AItem, int> currentItems = TextRPG.instance.player.inventory.GetCurrentItems();
                    currentItems.Where(itemPair => itemPair.Key is not AUsableItem).ToList().ForEach(itemPair => RPGWriter.DarkGray($"{itemPair.Value}x {itemPair.Key}"));
                    RPGWriter.LineBreak();
                    RPGWriter.Blue("Your usable Items:");
                    return true;
                });

        TextRPG.instance.player.inventory.GetCurrentItems().Where(itemPair => itemPair.Key is AUsableItem).ToList().ForEach(itemPair =>
        {
            menu.AddEntry(itemPair.Key.id.ToString(), $"{itemPair.Value}x {itemPair.Key}", () =>
                        {
                            if (itemPair.Key is AUsableItem aUsableItem)
                            {
                                TextRPG.instance.player.inventory.UseItem(aUsableItem);
                            }
                            return false;
                        });
        });

        menu.AddEntry("b", "Back to Menu", () => false);
        return menu;
    }
}