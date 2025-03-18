using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private Transform target;

    public void EnableRotation()
    {
        Debug.Log("Turret Rotation Enabled!");
        InvokeRepeating("FindTarget", 0f, 0.5f); 
    }

    private void FindTarget()
    {
        // Enemy 태그 인식 후 회전
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        Debug.Log($"적 개수: {enemies.Length}");

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
        if (target == null)
        {
            Debug.Log("타겟 없음");
            return;
        }

        Vector3 direction = target.position - transform.position;

        if (direction == Vector3.zero) 
        {
            Debug.LogWarning("Look rotation viewing vector is zero. 회전할 방향이 없습니다!");
            return;
        }

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed * 2f);
    }
}
