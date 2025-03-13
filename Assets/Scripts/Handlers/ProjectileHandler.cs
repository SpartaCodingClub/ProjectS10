using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] List<GameObject> projectileprefab;

    public void Shoot(int index = 0)
    {
        if (index >= projectileprefab.Count)
        {
            Debug.Log("잘못된 투사체 접근입니다.");
            return;
        }
        GameObject prefab = Instantiate(projectileprefab[index], transform.position + transform.forward + new Vector3(0, 0.5f, 0), transform.rotation);
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
}
