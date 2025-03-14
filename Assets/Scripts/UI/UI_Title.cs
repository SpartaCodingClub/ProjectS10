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

    private Sequence Line_Close()
    {
        var child = Get((int)Children.Line);

        return DOTween.Sequence()
            .Append(child.DOScaleY(0.0f, 0.3f).SetEase(Ease.OutSine));
    }

    private Sequence Text_TapToStart_Close()
    {
        var graphic = Get<Graphic>((int)Children.Text_TapToStart);

        return DOTween.Sequence()
            .Append(graphic.DOFade(0.0f, 0.3f));
    }
    #endregion

    private enum Children
    {
        Background,
        Line,
        Text_TapToStart
    }

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(UIState.Close, Line_Close, Text_TapToStart_Close);
        BindSequences(UIState.Close, Background_Close);

        Get<Button>((int)Children.Background).onClick.AddListener(Close);
    }

    public override void Open()
    {
        base.Open();
        base.Opened();
    }
}