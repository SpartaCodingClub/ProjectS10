using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class P_Equipment : MonoBehaviour
{
    PlayerController player;
    [SerializeField] GameObject curEquipPrefab;
    [SerializeField] GameObject curPickAxe;
    [SerializeField] Weapon curEquipment;
    public WeaponType? curEquipmentType { get { if (curEquipment == null) return null;
                return curEquipment.type; } }
    [SerializeField] Vector3 boxOffset;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Vector3 boxSize = new Vector3(1, 1, 1);
    RaycastHit[] hits;

    [Header("장착 위치")]
    [SerializeField] Transform OneH_Sword;

    [Header("곡괭이")]
    [SerializeField] GameObject Pickaxe;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        Init();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 boxCenter = transform.position + transform.forward * (boxSize.z / 2) + transform.TransformDirection(boxOffset);
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }

    private void Init()
    {
        curPickAxe = Instantiate(Pickaxe, OneH_Sword);
        curPickAxe.SetActive(false);
    }

    public void Equip(Item iteminput)
    {
        DeEquip();
        if (iteminput == null)
            return;
        curEquipPrefab = Instantiate(iteminput.Data.Weapon, OneH_Sword);
        curEquipment = curEquipPrefab.GetComponent<Weapon>();
        if (curEquipment.type == WeaponType.Melee)
            player.PStat.Attack += curEquipment.atk;
        else if (curEquipment.type == WeaponType.Projectile)
        {
            player.projectile.Addprefab(curEquipment.projectileObject);
        }
        curEquipPrefab.SetActive(true);
    }

    public void DeEquip()
    {
        if (curEquipPrefab == null)
            return;
        if (curEquipment.type == WeaponType.Melee)
            player.PStat.Attack -= curEquipment.atk;
        else if (curEquipment.type == WeaponType.Projectile)
        {
            player.projectile.RemovePrefab();
        }
        Destroy(curEquipPrefab);
        curEquipPrefab = null;
        curEquipment = null;
    }

    public void EquipPickaxe()
    {
        if(curEquipPrefab != null)
            curEquipPrefab.SetActive(false);
        curPickAxe.SetActive(true);
    }

    public void DeEquipPickaxe()
    {
        curPickAxe.SetActive(false);
        if (curEquipPrefab != null)
            curEquipPrefab.SetActive(true);
    }

    public void Attack()
    {
        if (curEquipment.type == WeaponType.Melee)
        {
            RaycastHit[] curHits = hits = Physics.BoxCastAll(transform.position + transform.forward * (boxSize.z / 2) + transform.TransformDirection(boxOffset), boxSize / 2, transform.forward, transform.rotation, 0, enemyLayer);
            foreach (RaycastHit hit in curHits)
            {
                //데미지 입히는 메서드
                hit.transform.GetComponent<StatHandler>().Damage(player.PStat.Attack);
                Debug.Log("적 감지");
            }
        }
        else if (curEquipment.type == WeaponType.Projectile)
        {
            player.projectile.Shoot();
        }
    }
}
