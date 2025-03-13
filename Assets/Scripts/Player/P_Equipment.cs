using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;

public class P_Equipment : MonoBehaviour
{
    PlayerController player;
    [SerializeField] GameObject curEquipPrefab;
    [SerializeField] Weapon curEquipment;
    [SerializeField] Vector3 boxOffset;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Vector3 boxSize = new Vector3(1, 1, 1);
    RaycastHit[] hits;

    [Header("장착 위치")]
    [SerializeField] Transform OneH_Sword;

    [Header("장착 가능 장비")]
    [SerializeField] GameObject PrimaryWeapon;
    [SerializeField] GameObject SubWeapon;
    [SerializeField] int Weaponnum = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 boxCenter = transform.position + transform.forward * boxSize.z / 2 + transform.TransformDirection(boxOffset);
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }

    public void ChangeWeapon(int num)
    {
        if (num == Weaponnum)
            return;
        if(num == 1)
        {
            DeEquip();
            Equip(PrimaryWeapon);
            num = 1;
        }
        else if(num == 2)
        {
            DeEquip();
            Equip(SubWeapon);
            num = 2;
        }
    }
    public void Equip(GameObject prefab)
    {
        if (prefab == null)
            return;
        curEquipPrefab = Instantiate(prefab, OneH_Sword);
        curEquipment = curEquipPrefab.GetComponent<Weapon>();
        if(curEquipment.type == WeaponType.Melee)
            player.PStat.Attack += curEquipment.atk;
    }

    public void DeEquip()
    {
        if (curEquipment == null)
            return;
        if (curEquipment.type == WeaponType.Melee)
            player.PStat.Attack -= curEquipment.atk;
        Destroy(curEquipPrefab);
        curEquipment = null;
    }

    public void Attack()
    {
        if (curEquipment.type == WeaponType.Melee)
        {
            RaycastHit[] curHits = hits = Physics.BoxCastAll(transform.position + transform.forward * boxSize.z / 2 + transform.TransformDirection(boxOffset), boxSize, transform.forward, transform.rotation, 0, enemyLayer);
            foreach (RaycastHit hit in curHits)
            {
                //데미지 입히는 메서드
            }
        }
        else if (curEquipment.type == WeaponType.Projectile)
        {
            
        }
    }
}
