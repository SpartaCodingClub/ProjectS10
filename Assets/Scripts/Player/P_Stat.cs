using System;

public class P_Stat : StatHandler
{
    public float PlusSpeed = 0;
    public float curSpeed { get { return Speed + PlusSpeed; } }

    public delegate Action damageaction();
    public damageaction DamageAction;

    void Start()
    {
        PlusSpeed = 0;
    }

    public void Damage(float damage)
    {
        Health -= damage;
        DamageAction();
    }
}