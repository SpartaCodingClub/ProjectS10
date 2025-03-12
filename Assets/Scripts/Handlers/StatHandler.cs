using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [SerializeField] float hp;
    public float Hp => hp;
    [SerializeField] float maxHp;
    public float MaxHp => maxHp;
    [SerializeField] float speed;
    public float Speed => speed;
    [SerializeField] float maxSpeed;
    public float MaxSpeed => maxSpeed;
}
