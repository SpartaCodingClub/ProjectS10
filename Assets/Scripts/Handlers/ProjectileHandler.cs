using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] List<GameObject> projectileprefab;
    [SerializeField] Vector3 offset;

    public void Shoot(int index = 0)
    {
        if (index >= projectileprefab.Count)
        {
            Debug.Log("잘못된 투사체 접근입니다.");
            return;
        }
        GameObject prefab = Instantiate(projectileprefab[index], transform.position+ offset, transform.rotation);
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
        Gizmos.DrawWireSphere(transform.position + offset, 0.2f);
    }
}
