using UnityEngine;

public enum ItemType
{
    Weapon,
    Resources,
    Consumtion,
    Building
}

[CreateAssetMenu(menuName = "아이템")]
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

    [Header("건물 데이터")]
    public GameObject Building;
    public float MaxHealth; 
    public int ResourceAmount;  // 건설 시 필요한 자원량
    public float BuildTime;
}