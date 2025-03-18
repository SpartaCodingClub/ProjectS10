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
        EnemyController[] gameObjects = FindObjectsOfType<EnemyController>();

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var enemy in gameObjects)
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
        if (target == null)
        {
            return;
        }

        Vector3 direction = target.position - transform.position;

        if (direction == Vector3.zero)
        {
            return;
        }

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed * 2f);
    }
}