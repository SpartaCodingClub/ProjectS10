using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float nextFireTime;
    private bool isShooting = false;

    public void EnableShooting()
    {
        isShooting = true;
        InvokeRepeating("Shoot", 0f, fireRate); 
    }

    private void Shoot()
    {
        if (!isShooting || Time.time < nextFireTime) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * 10f;

        nextFireTime = Time.time + fireRate;

        Destroy(bullet, 5f);
    }
}
