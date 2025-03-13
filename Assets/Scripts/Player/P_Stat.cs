using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class P_Stat : StatHandler
{
    public float PlusSpeed = 0;
    public float curSpeed { get { return Speed + PlusSpeed; } }

    public delegate Action damageaction();
    public damageaction DamageAction;
    // Start is called before the first frame update
    void Start()
    {
        PlusSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damage)
    {
        Health -= damage;
        DamageAction();
    }
}
