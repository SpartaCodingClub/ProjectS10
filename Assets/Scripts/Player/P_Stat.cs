using System;
using UnityEngine;

public class P_Stat : StatHandler
{
    PlayerController player;
    [SerializeField] private float water;
    public float Water { get { return water; } set { water = Mathf.Clamp(value, 0, 100); } }

    [SerializeField] private float hunger;
    public float Hunger { get { return hunger; } set { hunger = Mathf.Clamp(value, 0, 100); } }
    public float PlusSpeed = 0;
    public float curSpeed { get { return Speed + PlusSpeed; } }

    public delegate void damageaction();
    public damageaction DamageAction;

    private UI_StatusBar statusBar;

    [Header("전투 관련")]
    public float InvincibleTime;
    public bool CanDamage;

    void Start()
    {
        player = GetComponent<PlayerController>();
        statusBar = Managers.UI.Show<UI_StatusBar>();
        statusBar.UpdateUI(UI_StatusBar.Type.Health, Health, 100);

        PlusSpeed = 0;
        CanDamage = true;
    }

    public override void Damage(float damage)
    {
        if (CanDamage == false)
            return;
        Health -= damage;
        statusBar.UpdateUI(UI_StatusBar.Type.Health, Health, 100);

        if (Health > 0)
            DamageAction();
        else
            player.pAnimationHandler.PlayDie();
    }
}