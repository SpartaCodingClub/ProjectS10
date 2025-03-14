using System;

public class P_Stat : StatHandler
{
    public float PlusSpeed = 0;
    public float curSpeed { get { return Speed + PlusSpeed; } }

    public delegate Action damageaction();
    public damageaction DamageAction;

    private UI_StatusBar statusBar;

    void Start()
    {
        statusBar = Managers.UI.Show<UI_StatusBar>();

        PlusSpeed = 0;
    }

    public void Damage(float damage)
    {
        Health -= damage;
        statusBar.UpdateUI(Health, 100);

        DamageAction();
    }
}