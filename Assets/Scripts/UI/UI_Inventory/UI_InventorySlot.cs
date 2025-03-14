using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventorySlot : UI_Base
{
    #region Open
    private Sequence Icon_Open()
    {
        var child = Get((int)Children.Icon);

        return DOTween.Sequence()
            .Append(child.DOScale(1.0f, 0.3f).From(0.0f).SetEase(Ease.OutBack));
    }
    #endregion
    #region Update
    private Sequence Text_Amount_Update()
    {
        var child = Get((int)Children.Text_Amount);

        return DOTween.Sequence()
            .Append(child.DOPunchScale(0.2f * Vector3.one, 0.2f));
    }
    #endregion

    private enum Children
    {
        Icon,
        Text_Amount
    }

    public Item Item { get; private set; }

    private Sequence icon;
    private Sequence textAmount;

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        icon = Icon_Open();
        textAmount = Text_Amount_Update();
    }

    public void UpdateUI(int amount)
    {
        if (Item == null)
        {
            return;
        }

        if (Item.Data.CanStacking == false)
        {
            return;
        }

        TMP_Text textAmount = Get<TMP_Text>((int)Children.Text_Amount);
        textAmount.text = (Item.amount += amount).ToString();
        textAmount.gameObject.SetActive(true);

        this.textAmount.Restart();
    }

    public void UpdateUI(Item item)
    {
        Image icon = Get<Image>((int)Children.Icon);
        //icon.sprite = Managers.Resource.GetSprite(SpriteType.Item, item.Data.ID);
        icon.gameObject.SetActive(true);
        this.icon.Restart();

        if (item.Data.CanStacking)
        {
            TMP_Text textAmount = Get<TMP_Text>((int)Children.Text_Amount);
            textAmount.text = item.amount.ToString();
            textAmount.gameObject.SetActive(true);
        }

        Item = item;
    }

    public void RemoveItem()
    {
        Get<Image>((int)Children.Icon).gameObject.SetActive(false);
        Get<TMP_Text>((int)Children.Text_Amount).gameObject.SetActive(false);

        Item = null;
    }

    public void Use()
    {
        if (Item == null)
        {
            return;
        }

        Item.Use();
    }
}