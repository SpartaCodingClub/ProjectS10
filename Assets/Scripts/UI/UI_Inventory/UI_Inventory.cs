public class UI_Inventory : UI_SubItem
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