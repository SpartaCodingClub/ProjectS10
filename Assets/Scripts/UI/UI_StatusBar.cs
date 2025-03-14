using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatusBar : UI_SubItem
{
    private readonly int FILL_LEVEL = Shader.PropertyToID("_FillLevel");

    public enum Type
    {
        Health,
        Water,
        Food
    }

    private enum Children
    {
        Slider_Top,
        Slider_Bottom,
        Fill_Health
    }

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));
    }

    public void UpdateUI(Type type, float value, float maxValue)
    {
        switch (type)
        {
            case Type.Health:
                Get<Image>((int)Children.Fill_Health).material.DOFloat(value / maxValue, FILL_LEVEL, 0.2f);
                break;
            case Type.Water:
                Get<Slider>((int)Children.Slider_Top).DOValue(value / maxValue, 0.2f);
                break;
            case Type.Food:
                Get<Slider>((int)Children.Slider_Bottom).DOValue(value / maxValue, 0.2f);
                break;
        }
    }
}