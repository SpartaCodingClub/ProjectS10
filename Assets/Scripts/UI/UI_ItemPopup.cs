using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UI_ItemPopup : UI_SubItem
{
    #region Birth
    private Sequence ScreenDimmed_Open()
    {
        var graphic = Get<Graphic>((int)Children.ScreenDimmed);

        return Utility.RecyclableSequence()
            .Append(graphic.DOFade(0.8f, 0.2f).From(0.0f).OnComplete(Opened));
    }

    private Sequence Popup_Open()
    {
        var child = Get((int)Children.Popup);

        return Utility.RecyclableSequence()
            .Append(child.DOScale(0.8f, 0.2f).From(0.0f).SetEase(Ease.OutBack));
    }
    #endregion
    #region Death
    private Sequence ScreenDimmed_Close()
    {
        var graphic = Get<Graphic>((int)Children.ScreenDimmed);

        return Utility.RecyclableSequence()
            .Append(graphic.DOFade(0.0f, 0.3f).OnComplete(Destroy));
    }

    private Sequence Popup_Close()
    {
        var child = Get((int)Children.Popup);

        return Utility.RecyclableSequence()
            .Append(child.DOScale(0.0f, 0.3f).SetEase(Ease.InBack));
    }
    #endregion

    private enum Children
    {
        ScreenDimmed,
        Popup,
        Text_Type,
        Icon,
        Text_ItemName,
        Text_Info
    }

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(UIState.Open, ScreenDimmed_Open, Popup_Open);
        BindSequences(UIState.Close, ScreenDimmed_Close, Popup_Close);
    }

    public void UpdateUI(Item item)
    {
        switch (item.Data.Type)
        {
            case ItemType.Weapon:
                Get<TMP_Text>((int)Children.Text_Type).text = "장비 아이템";
                break;
            case ItemType.Resources:
                Get<TMP_Text>((int)Children.Text_Type).text = "자원 아이템";
                break;
            case ItemType.Consumtion:
                Get<TMP_Text>((int)Children.Text_Type).text = "소비 아이템";
                break;
            default:
                Get<TMP_Text>((int)Children.Text_Type).text = item.Data.Type.ToString();
                break;
        }

        Get<Image>((int)Children.Icon).sprite = Managers.Resource.GetSprite(SpriteType.Item, item.Data.ID);
        Get<TMP_Text>((int)Children.Text_ItemName).text = item.Data.Name;
        Get<TMP_Text>((int)Children.Text_Info).text = item.Data.Description;
    }
}