using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Build : UI_Scene, IPointerExitHandler
{
    #region Open
    private Sequence Deco_Open()
    {
        var child = Get((int)Children.Deco);

        return Utility.RecyclableSequence()
            .Append(child.DOLocalRotate(Vector3.zero, 0.3f).OnComplete(Opened));
    }

    private Sequence Roulette_Back_Open()
    {
        var child = Get((int)Children.Roulette_Back);

        return Utility.RecyclableSequence()
            .Append(child.DOLocalRotate(180.0f * Vector3.forward, 0.3f));
    }
    #endregion
    #region Close
    private Sequence Deco_Close()
    {
        var child = Get((int)Children.Deco);

        return Utility.RecyclableSequence()
            .Append(child.DOLocalRotate(180.0f * Vector3.forward, 0.3f).OnComplete(() => canvasGroup.interactable = true));
    }

    private Sequence Roulette_Back_Close()
    {
        var child = Get((int)Children.Roulette_Back);

        return Utility.RecyclableSequence()
            .Append(child.DOLocalRotate(Vector3.zero, 0.3f));
    }
    #endregion
    #region Events
    public void Button_Spin()
    {
        switch (state)
        {
            case UIState.Opened:
                Close();
                break;
            case UIState.Close:
                Open();
                break;
        }
    }
    #endregion

    private enum Children
    {
        Deco,
        Roulette_Back,
        Button_Spin
    }

    private bool isInitialized;

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(UIState.Open, Deco_Open, Roulette_Back_Open);
        BindSequences(UIState.Close, Deco_Close, Roulette_Back_Close);

        Get<Button>((int)Children.Button_Spin).onClick.AddListener(Button_Spin);
    }

    public override void Open()
    {
        if (isInitialized)
        {
            base.Open();
            return;
        }

        canvasGroup.interactable = true;
        state = UIState.Close;

        isInitialized = true;
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