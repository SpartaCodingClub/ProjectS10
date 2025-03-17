public class UI_Build : UI_SubItem
{
    #region Open 
    #endregion
    #region Close
    #endregion

    private enum Children
    {
        Deco,
        Roulette_Back,
        Button_Spin
    }

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));


    }
}