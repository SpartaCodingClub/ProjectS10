using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float nextFireTime;

    public void EnableShooting()
    {
        InvokeRepeating("Shoot", 0f, fireRate); 
    }

    private void Shoot()
    {
        if (Time.time < nextFireTime) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * 10f; 

        nextFireTime = Time.time + fireRate;
    }
}
