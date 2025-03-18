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

    [Header("수분,배고픔")]
    [SerializeField] private float waterDecreaseSpeed;
    [SerializeField] private float hungerDecreaseSpeed;
    [SerializeField] private float hpDecreaseSpeedByWater;
    [SerializeField] private float hpDecreaseSpeedByHunger;
    bool isStart = false;

    [Header("전투 관련")]
    public float InvincibleTime;
    public bool CanDamage;

    void Start()
    {
        player = GetComponent<PlayerController>();
        statusBar = Managers.UI.Show<UI_StatusBar>();
        statusBar.UpdateUI(UI_StatusBar.Type.Health, Health, 100);

        water = 100;
        hunger = 100;
        PlusSpeed = 0;
        CanDamage = true;
        isStart = false;
    }

    public void Init()
    {
        isStart = true;
    }

    private void Update()
    {
        if (!isStart)
            return;
        water -= waterDecreaseSpeed * Time.deltaTime;
        hunger -= hungerDecreaseSpeed * Time.deltaTime;
        statusBar.UpdateUI(UI_StatusBar.Type.Water, water, 100);
        statusBar.UpdateUI(UI_StatusBar.Type.Food, hunger, 100);
        if (water <= 0)
        {
            water = 0;
            player.PStat.Health -= hpDecreaseSpeedByWater * Time.deltaTime;
            statusBar.UpdateUI(UI_StatusBar.Type.Health, Health, 100);
        }
        if (hunger <= 0)
        {
            hunger = 0;
            player.PStat.Health -= hpDecreaseSpeedByHunger * Time.deltaTime;
            statusBar.UpdateUI(UI_StatusBar.Type.Health, Health, 100);
        }
        if (Health > 0)
            DamageAction();
        else
            player.pAnimationHandler.PlayDie();
    }

    public override void Damage(float damage)
    {
        if (CanDamage == false)
            return;
        Health -= damage;
        statusBar.UpdateUI(UI_StatusBar.Type.Health, Health, 100);
    }
}