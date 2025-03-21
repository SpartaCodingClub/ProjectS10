using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Melee,
    Projectile
}
public class Weapon : MonoBehaviour
{
    public WeaponType type;
    public float atk;
    public int projectilenum;
    public float atkDelay;
    public Vector3 range;
    public GameObject projectileObject;
}
