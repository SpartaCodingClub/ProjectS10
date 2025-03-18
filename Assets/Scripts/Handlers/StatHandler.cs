using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Range(0, 100)][SerializeField]private float health;
    public float Health {  get =>  health; set => health = Mathf.Clamp(value, 0, 100); }

    [Range(0, 30)][SerializeField] private float speed;
    public float Speed { get => speed; set => speed = Mathf.Clamp(value, 1, 30); }

    [Range(0, 1000)][SerializeField] private float stamina;
    public float Stamina { get => stamina; set => stamina = Mathf.Clamp(value, 0, 1000); }

    [Range(0, 30)][SerializeField] private float attack;
    public float Attack { get => attack; set => attack = Mathf.Clamp(value, 1, 30); }

    [Range(0, 30)][SerializeField] public float defense;
    public float Defense { get => defense; set => defense = Mathf.Clamp(value, 1, 30); }

    public virtual void Damage(float damage) { }
}
