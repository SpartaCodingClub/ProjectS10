using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Resources,
    Consumtion
}

[CreateAssetMenu(menuName = "아이템")]
public class Item : ScriptableObject
{
    [Header("아이템 정보")]
    public int id;
    public string name;
    public string description;
    public ItemType type;

    [Header("스택 가능 여부")]
    public bool canStacking;
    public int maxStacking;

    [Header("무기 프리팹")]
    public GameObject weapon;
}
