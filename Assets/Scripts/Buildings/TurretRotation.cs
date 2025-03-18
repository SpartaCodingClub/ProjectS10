using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private Transform target;

    public void EnableRotation()
    {
        InvokeRepeating("FindTarget", 0f, 0.5f); 
    }

    private void FindTarget()
    {
        // Enemy 태그 인식 후 회전
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        target = closestEnemy;
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed * 2f);
    }
}
