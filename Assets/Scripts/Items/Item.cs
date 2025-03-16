using UnityEngine;

public class Item
{
    public ItemData Data;
    public int amount;
    public float hpValue;
    public float hungerValue;

    public Item(ItemData Data, int amount)
    {
        this.Data = Data;
        this.amount = amount;
    }

    public void Use()
    {
        switch (Data.Type)
        {
            case ItemType.Weapon:
                Managers.Game.Player.PEquip.Equip(this);
                break;
            case ItemType.Resources:

                break;
            case ItemType.Consumtion:
                Managers.Game.Player.PStat.Health += hpValue;
                Managers.Game.Player.PStat.Hunger += hungerValue;
                break;
        }
    }
}