using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatusBar : UI_SubItem
{
    private readonly int FILL_LEVEL = Shader.PropertyToID("_FillLevel");

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

    public void UpdateUI(float water, float maxWater, float food, float maxFood)
    {
        Get<Slider>((int)Children.Slider_Top).DOValue(water / maxWater, 0.2f);
        Get<Slider>((int)Children.Slider_Bottom).DOValue(food / maxFood, 0.2f);
    }

    public void UpdateUI(float hp, float maxHP)
    {
        Get<Image>((int)Children.Fill_Health).material.DOFloat(hp / maxHP, FILL_LEVEL, 0.2f);
    }
}