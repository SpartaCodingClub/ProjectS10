public class UI_PlayerCondition : UI_SubItem
{
    private enum Children
    {

    }

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));
    }
}