using DG.Tweening;
using UnityEngine.UI;

public class UI_PlayerCondition : UI_SubItem
{
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
        Get<Image>((int)Children.Fill_Health).DOFillAmount(hp / maxHP, 0.2f);
    }
}