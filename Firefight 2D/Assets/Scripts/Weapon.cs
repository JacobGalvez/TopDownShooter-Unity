using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce;
    public float fireRate = 0.1f; // Time between each shot
    private bool isFiring = false; // Flag to track if the weapon is currently firing
    private float nextFireTime = 0f; // Time of the next allowed shot
    private float magazineCapacity;
    private float maxMagazine = 30f;
    private float reloadTime = 2f; // Time it takes to reload the magazine
    private bool isReloading = false; // Flag to track if the weapon is currently being reloaded

    [SerializeField] private Slider magazineSlider;
    [SerializeField] private AudioSource gunshotSound;
    [SerializeField] private AudioSource reloadSound;

    private void Start()
    {
        magazineCapacity = maxMagazine;
        magazineSlider.maxValue = maxMagazine;
        magazineSlider.minValue = 0f;
    }

    public void StartFiring()
    {
        isFiring = true;
    }

    public void StopFiring()
    {
        isFiring = false;
    }

   private void Update()
    {
        if(!PauseMenu.isPaused)
        {
            if (isReloading)
            {
                // Do not allow firing while reloading
                return;
            }

            if (isFiring && Time.time >= nextFireTime && magazineCapacity > 0)
            {
                Fire();
                nextFireTime = Time.time + fireRate;
                magazineCapacity -= 1f;
                gunshotSound.Play();

                if (magazineCapacity <= 0)
                {
                    // Start reloading if the magazine is empty
                    StartCoroutine(Reload());
                }
            }

            if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            {
                StartCoroutine(Reload());
            }
        }
    }

    private IEnumerator Reload()
    {
        reloadSound.Play();
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        magazineCapacity = maxMagazine;
        isReloading = false;
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

    private void OnGUI()
    {
        magazineSlider.value = magazineCapacity;
    }
}
