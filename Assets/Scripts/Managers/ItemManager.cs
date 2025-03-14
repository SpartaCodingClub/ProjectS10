using UnityEngine;

public enum ItemID
{
    MeleeWeapon = 10001,
    RangeWeapon = 10002,


}

public class ItemManager
{
    private UI_Inventory inventoryUI;

    public void Start()
    {
        inventoryUI = Managers.UI.Show<UI_Inventory>();
        inventoryUI.AddItem(CreateItem(ItemID.MeleeWeapon));
        inventoryUI.AddItem(CreateItem(ItemID.RangeWeapon));
    }

    public void AddItem(Item item)
    {
        inventoryUI.AddItem(item);
    }

    public Item CreateItem(ItemID type)
    {
        ItemData data = Resources.Load<ItemData>($"{Define.PATH_ITEM}/{(int)type}");
        return new(data, 1);
    }

    public void RemoveItem(Item item)
    {
        inventoryUI.RemoveItem(item);
    }

    public void Use(int keyNum)
    {
        inventoryUI.Use(keyNum);
    }
}