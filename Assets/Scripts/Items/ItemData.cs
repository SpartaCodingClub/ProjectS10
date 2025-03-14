using System;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Resources,
    Consumtion
}

[CreateAssetMenu(menuName = "아이템")]
[Serializable]
public class ItemData : ScriptableObject
{
    [Header("아이템 정보")]
    public int ID;
    public string Name;
    public string Description;
    public ItemType Type;

    [Header("스택 가능 여부")]
    public bool CanStacking;
    public int MaxStacking;

    [Header("무기 프리팹")]
    public GameObject Weapon;
}