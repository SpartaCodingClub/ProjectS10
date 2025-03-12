using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Range(1, 100)][SerializeField]private float health;
    public float Health {  get =>  health; set => health = Mathf.Clamp(value, 0, 100 ); }

    [Range(1, 30)][SerializeField] private float speed;
    public float Speed { get => speed; set => speed = Mathf.Clamp(value, 1, 30); }

    [Range(1, 30)][SerializeField] private float stamina;
    public float Stamina { get => stamina; set => stamina = Mathf.Clamp(value, 0, 400); }
}
