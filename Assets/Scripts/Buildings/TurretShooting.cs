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

        GameObject bullet = Managers.Pool.TryPop("Bullet");

        if (bullet == null)
        {
            bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.SetActive(true);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * 10f;
        bullet.GetComponent<Projectile>().Init();

        nextFireTime = Time.time + fireRate;
    }
}
