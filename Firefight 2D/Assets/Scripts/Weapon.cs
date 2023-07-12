using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce;
    public float fireRate = 0.1f; // Time between each shot
    private bool isFiring = false; // Flag to track if the weapon is currently firing
    private float nextFireTime = 0f; // Time of the next allowed shot

    public void StartFiring()
    {
        isFiring = true;
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    void Update()
    {
        if (isFiring && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    public void Fire()
    {
        float bulletDmg = 20f;
        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

        Bullet bulletScript = projectile.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.bulletDamage = bulletDmg;
        }
    }
}
