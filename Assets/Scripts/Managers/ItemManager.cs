public class ItemManager
{
    private UI_Inventory inventoryUI;

    public void Start()
    {
        inventoryUI = Managers.UI.Show<UI_Inventory>();
    }

    public void AddItem(Item item)
    {
        inventoryUI.AddItem(item);
    }

    public void RemoveItem(Item item)
    {
        inventoryUI.RemoveItem(item);
    }
}