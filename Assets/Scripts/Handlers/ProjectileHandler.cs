using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] List<GameObject> projectileprefab;
    [SerializeField] Vector3 offset;
    Vector3 offsetPos;


    private void Update()
    {
        offsetPos = transform.forward * offset.z + transform.right * offset.x + transform.up * offset.y;
    }

    public void Shoot(int index = 0)
    {
        if (index >= projectileprefab.Count)
        {
            Debug.Log("잘못된 투사체 접근입니다.");
            return;
        }
        GameObject prefab = Instantiate(projectileprefab[index], transform.position + offsetPos, transform.rotation);
        prefab.GetComponent<Projectile>().Init();
    }

    public void Addprefab(GameObject prefab)
    {
        projectileprefab.Add(prefab);
    }

    public void RemovePrefab(int index = 0)
    {
        projectileprefab.RemoveAt(index);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + offsetPos, 0.2f);
    }
}
