using System.Collections.Generic;
using UnityEngine;

public enum ItemID
{
    MeleeWeapon = 10001,
    RangeWeapon = 10002,

    BlueStone = 20001,
    PurpleStone = 20002,
    RedStone = 20003,
}

public class ItemManager
{
    public UI_ItemPopup ItemPopup { get; set; }

    private UI_Inventory inventoryUI;

    private readonly Dictionary<int, Item> inventory = new();

    public void Start()
    {
        inventoryUI = Managers.UI.Show<UI_Inventory>();
        inventoryUI.AddItem(CreateItem(ItemID.MeleeWeapon));
        inventoryUI.AddItem(CreateItem(ItemID.RangeWeapon));
    }

    public void AddItem(Item item)
    {
        if (inventoryUI.AddItem(item) == false)
        {
            return;
        }

        int id = item.Data.ID;
        if (inventory.ContainsKey(id))
        {
            inventory[id].amount += item.amount;
            return;
        }

        inventory.Add(id, item);
    }

    public bool ContainsItem(int id, int amount)
    {
        if (inventory.TryGetValue(id, out var item) == false)
        {
            return false;
        }

        if (item.amount < amount)
        {
            return false;
        }

        return true;
    }

    public Item CreateItem(ItemID type, int amount = 1)
    {
        ItemData data = Resources.Load<ItemData>($"{Define.PATH_ITEM}/{(int)type}");
        return new(data, amount);
    }

    public void RemoveItem(int id, int amount)
    {
        if (inventory.TryGetValue(id, out var inventoryItem) == false)
        {
            return;
        }

        inventoryItem.amount -= amount;
        if (inventoryItem.amount <= 0)
        {
            inventoryUI.RemoveItem(inventoryItem);
        }
    }

    public void Use(int keyNum)
    {
        inventoryUI.Use(keyNum);
    }
}