public class ItemManager
{
    private UI_Inventory inventoryUI;

    public void Start()
    {
        inventoryUI = Managers.UI.Show<UI_Inventory>();
    }
}