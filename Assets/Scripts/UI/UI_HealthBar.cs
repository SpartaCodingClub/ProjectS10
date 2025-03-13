using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : UI_WorldSpace
{
    #region Open
    private Sequence Slider_Open()
    {
        var child = Get((int)Children.Slider);

        return Utility.RecyclableSequence()
            .Append(child.DOScale(1.0f, 0.3f).From(0.0f).SetEase(Ease.OutBack).OnComplete(Opened));
    }
    #endregion
    #region Close
    private Sequence Slider_Close()
    {
        var child = Get((int)Children.Slider);

        return Utility.RecyclableSequence()
            .Append(child.DOScale(0.0f, 0.3f).SetEase(Ease.InBack).OnComplete(Destroy));
    }
    #endregion

    private enum Children
    {
        Slider,
        Fill_White
    }

    private Slider slider;
    private Image fillWhite;

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(UIState.Open, Slider_Open);
        BindSequences(UIState.Close, Slider_Close);

        slider = Get<Slider>((int)Children.Slider);
        fillWhite = Get<Image>((int)Children.Fill_White);

        gameObject.SetActive(false);
    }

    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Managers.Game.MainCamera;
    }

    private void Update()
    {
        transform.rotation = Managers.Game.MainCamera.transform.rotation;
    }

    public override void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void UpdateUI(float hp, float maxHP)
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            fillWhite.fillAmount = 1.0f;

            Open();
        }

        slider.value = hp / maxHP;
        StartCoroutine(Filling());
    }

    private IEnumerator Filling()
    {
        while (fillWhite.fillAmount > slider.value)
        {
            fillWhite.fillAmount = Mathf.Lerp(fillWhite.fillAmount, slider.value, 5.0f * Time.deltaTime);
            yield return null;
        }
    }
}