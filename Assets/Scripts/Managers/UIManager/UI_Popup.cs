using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public abstract class UI_Popup : UI_Base
{
    #region Birth
    private Sequence Popup_Open()
    {
        return Utility.RecyclableSequence()
            .Append(Popup.DOScale(1.0f, 0.5f).From(0.0f).SetEase(Ease.OutBack));
    }
    #endregion
    #region Death
    private Sequence Popup_Close()
    {
        return Utility.RecyclableSequence()
            .Append(Popup.DOScale(0.0f, 0.5f).SetEase(Ease.InBack));
    }
    #endregion

    public bool Interactable { get { return canvasGroup.interactable; } }
    public int SortingOrder { get { return canvas.sortingOrder; } set { canvas.sortingOrder = value; } }

    protected RectTransform Popup;

    private Canvas canvas;

    protected override void Initialize()
    {
        base.Initialize();

        Popup = gameObject.FindComponent<RectTransform>(nameof(Popup));
        canvas = GetComponent<Canvas>();

        BindSequences(UIState.Open, Popup_Open);
        BindSequences(UIState.Close, Popup_Close);
    }
}