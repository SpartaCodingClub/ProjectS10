using System;
using UnityEngine;

public class P_Stat : StatHandler
{
    [SerializeField] private float water;
    public float Water { get { return water; } set { water = Mathf.Clamp(value, 0, 100); } }

    [SerializeField] private float hunger;
    public float Hunger { get { return hunger; } set { hunger = Mathf.Clamp(value, 0, 100); } }
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
        statusBar.UpdateUI(UI_StatusBar.Type.Health, Health, 100);

        DamageAction();
    }
}