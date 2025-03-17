using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stage : UI_SubItem
{
    #region Update
    private Sequence Text_Chapter_Update()
    {
        var child = Get((int)Children.Text_Chapter);

        return DOTween.Sequence()
            .Append(child.DOPunchScale(0.2f * Vector3.one, 0.2f));
    }
    #endregion

    private enum Children
    {
        Text_Chapter,
        Slider_Wave,
        Text_Left,
        Text_Right,
    }

    private float timer;
    private Action onComplete;

    private Slider sliderWave;
    private Sequence textChapter;

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        sliderWave = Get<Slider>((int)Children.Slider_Wave);
        textChapter = Text_Chapter_Update();
    }

    protected override void Deinitialize()
    {
        base.Deinitialize();
        textChapter.Kill();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer = Mathf.Max(timer - Time.deltaTime, 0.0f);
            Get<TMP_Text>((int)Children.Text_Chapter).text = $"다음 웨이브  :  {Utility.GetTimer(timer)}";
        }
        else if (onComplete != null)
        {
            onComplete.Invoke();
            onComplete = null;
        }
    }

    public void SetTimer(float timer, Action onComplete = null)
    {
        this.timer = timer;
        this.onComplete = onComplete;
    }

    public void UpdateUI(int kill, int goal, int currentStage, string info = "모험가 처치")
    {
        var textChapter = Get<TMP_Text>((int)Children.Text_Chapter);
        textChapter.text = $"{info} {kill}/{goal}";
        this.textChapter.Restart();

        Get<TMP_Text>((int)Children.Text_Left).text = currentStage.ToString();
        Get<TMP_Text>((int)Children.Text_Right).text = (currentStage + 1).ToString();

        if (kill == 0)
        {
            StartCoroutine(Filling());
        }
        else
        {
            sliderWave.value = (float)kill / goal;
        }
    }

    private IEnumerator Filling()
    {
        while (sliderWave.value > 0.0f)
        {
            sliderWave.value = Mathf.Lerp(sliderWave.value, 0.0f, 5.0f * Time.deltaTime);
            yield return null;
        }
    }
}