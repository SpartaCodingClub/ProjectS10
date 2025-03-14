using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData Data;
    public float amount;
    public float hpValue;
    public float hungerValue;

    public void Use()
    {
        switch(Data.Type)
        {
            case ItemType.Weapon:
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