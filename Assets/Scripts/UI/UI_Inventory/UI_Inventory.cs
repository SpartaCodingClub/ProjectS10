using System;

public class UI_Inventory : UI_SubItem
{
    private enum Children
    {
        Content
    }

    private UI_InventorySlot[] content;

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        content = Get((int)Children.Content).GetComponentsInChildren<UI_InventorySlot>();
    }

    public void Use(int numKey)
    {
        int index = numKey - 1;
        content[index].Use();
    }

    public void AddItem(int index, int id, Action onUse = null)
    {
        UI_InventorySlot slot = content[index];
        slot.UpdateUI(id);
        slot.OnUse += onUse;
    }

    public void RemoveItem(int index, ItemData item, Action onUse = null)
    {
        content[index].OnUse -= onUse;
    }
}