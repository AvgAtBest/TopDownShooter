using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fires Gun

public class GunController : MonoBehaviour
{
    public bool isFiring;

    public Bullet bullet;
    public float bulletSpeed;

    public float shootRate;
    private float shootTimer;

    private bool readyToFire = false;

    public Transform firePoint;
    public int ammoInClip;
    public int damage;
    public int maxClipSize = 10;
    public int ammoInReserve = 300;
    public bool requiresReload = false;
    //public TopDownShooter.Gun_Ammo gunAmmo;
    public TopDownShooter.Gun_AmmoUI gunAmmoUI;

    // Start is called before the first frame update
    void Start()
    {
        ammoInClip = maxClipSize;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isFiring)
        //{
        //    shotCounter -= Time.deltaTime;
        //    if (shotCounter <= 0)
        //    {
        //        shotCounter = timeBetweenShots;
        //        Bullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as Bullet;
        //        newBullet.bulletSpeed = bulletSpeed;
        //        newBullet.damage = damage;
        //    }
        //    curAmmo--;
        //}
        //else
        //{
        //    shotCounter = 0;
        //}
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootRate)
            readyToFire = true;
        else
            readyToFire = false;

        //if (isFiring)
        //{
        //    Fire();
        //}

    }

    public void Fire()
    {
        if (!readyToFire)
            return;

        if (ammoInClip <= 0)
            return;

        // Fire a bullet
        Bullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as Bullet;
        newBullet.bulletSpeed = bulletSpeed;
        newBullet.damage = damage;

        ammoInClip--;

        if (ammoInClip <= 0) {
            ammoInClip = 0;
        }

        // Reset shoot timer back to zero.
        shootTimer = 0;

        // Are we in a state where we COULD reload?
        requiresReload = (ammoInClip < maxClipSize);


        gunAmmoUI.UpdateAmmoUI(ammoInClip, ammoInReserve);


    }

    public void Reload()
    {
        if (!requiresReload)
            return;

        var ammoRequired = maxClipSize - ammoInClip;

        if (ammoRequired <= ammoInReserve) {
            ammoInClip = maxClipSize;
            ammoInReserve -= ammoRequired;
        }
        else if (ammoRequired > ammoInReserve)
        {
            ammoInClip += ammoInReserve;
            ammoInReserve = 0;
        }

        gunAmmoUI.UpdateAmmoUI(ammoInClip, ammoInReserve);

    }

    public void StopFiring()
    {


    }
}


