using UnityEngine.EventSystems;

public class UI_Inventory : UI_SubItem, IPointerExitHandler
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

    public void UpdateUI()
    {
        for (int i = 0; i <content.Length; i++)
        {
            if (content[i].Item != null)
            {
                content[i].UpdateUI(content[i].Item);
            }
        }
    }

    public void Use(int numKey)
    {
        content[numKey - 1].Use();
    }

    public bool AddItem(Item item)
    {
        // 같은 아이템이 있는지 확인
        for (int i = 0; i < content.Length; i++)
        {
            Item slotItem = content[i].Item;
            if (slotItem == null)
            {
                continue;
            }

            if (slotItem.Data.ID == item.Data.ID)
            {
                content[i].UpdateUI(item.amount);
                return true;
            }
        }

        // 같은 아이템이 없다면, 빈 공간을 찾아 새로 추가
        for (int i = 0; i < content.Length; i++)
        {
            var slot = content[i];
            if (slot.Item == null)
            {
                slot.UpdateUI(item);
                return true;
            }
        }

        return false;
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

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Managers.Item.ItemPopup == null)
        {
            return;
        }

        Managers.Item.ItemPopup.Close();
        Managers.Item.ItemPopup = null;
    }
}