using DG.Tweening;
using UnityEngine.UI;

public class UI_Title : UI_Scene
{
    #region Close
    private Sequence Background_Close()
    {
        return DOTween.Sequence()
            .Append(canvasGroup.DOFade(0.0f, 0.3f).SetDelay(0.2f).OnComplete(Destroy));
    }

    private Sequence Button_Start_Close()
    {
        var child = Get((int)Children.Button_Start);

        return DOTween.Sequence()
            .Append(child.DOScaleY(0.0f, 0.3f).SetEase(Ease.OutSine));
    }
    #endregion

    private enum Children
    {
        Background,
        Button_Start
    }

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(UIState.Close, Button_Start_Close);
        BindSequences(UIState.Close, Background_Close);

        Get<Button>((int)Children.Background).onClick.AddListener(Close);
    }

    public override void Open()
    {
        base.Open();
        base.Opened();
    }
}