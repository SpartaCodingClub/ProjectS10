using System.Linq;

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
        content[numKey - 1].Use();
    }

    public void AddItem(Item item)
    {
        // 같은 아이템이 있는지 확인
        var sameItem = content.FirstOrDefault(slot => slot.Item.Data.ID == item.Data.ID);
        if (sameItem != null)
        {
            sameItem.UpdateUI(item.amount);
            return;
        }

        // 같은 아이템이 없다면, 빈 공간을 찾아 새로 추가
        for (int i = 2; i < content.Length; i++)
        {
            var slot = content[i];
            if (slot.Item == null)
            {
                slot.UpdateUI(item);
                break;
            }
        }
    }

    public void RemoveItem(Item item)
    {
        for (int i = 0; i < content.Length; i++)
        {
            var slot = content[i];
            if (slot.Item != item)
            {
                continue;
            }

            slot.RemoveItem();
        }
    }
}