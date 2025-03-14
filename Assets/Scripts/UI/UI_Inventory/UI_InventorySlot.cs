using System;
using UnityEngine.UI;

public class UI_InventorySlot : UI_Base
{
    private enum Children
    {
        Icon
    }

    public event Action OnUse;

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));
    }

    public void UpdateUI(int id)
    {
        Image icon = Get<Image>((int)Children.Icon);
        icon.sprite = Managers.Resource.GetSprite(SpriteType.Item, id);
        icon.enabled = true;
    }

    public void RemoveItem()
    {
        Get<Image>((int)Children.Icon).enabled = false;
    }

    public void Use()
    {
        OnUse?.Invoke();
    }
}