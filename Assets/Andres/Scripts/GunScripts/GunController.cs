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
        public float shootTimer;
        public float timeBetweenShots;
        private float shotCounter;

        public Transform firePoint;
        public int curAmmo;
        public int damage;
        public int clipsize = 10;
        public int ammoMaxReserve = 300;
        public bool canReload = false;
        public TopDownShooter.Gun_Ammo gunAmmo;
        public TopDownShooter.Gun_AmmoUI gunAmmoUI;

        // Start is called before the first frame update
        void Start()
        {

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
            {
                //ReloadChamber();
                //readyToFire = true;
            }

            if (isFiring)
            {
                Fire();
            }

            if (Input.GetButtonDown("Fire1") && curAmmo > 0)
            {
                curAmmo -= 1;
                if (curAmmo <= 0)
                {
                    canReload = true;
                }
            }
            if (curAmmo == 0 && canReload)
            {
                canReload = false;
                Debug.Log("Press R to Reload");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                curAmmo = clipsize;
            }
        }

        public void Fire()
        {
            if (isFiring)
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;
                    Bullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as Bullet;
                    newBullet.bulletSpeed = bulletSpeed;
                    newBullet.damage = damage;
                }
                curAmmo--;
                if (curAmmo < 0)
                {
                    curAmmo = 0;
                }
                gunAmmoUI.UpdateAmmoUI(curAmmo, gunAmmo.clipSize);

            }
            else
            {
                shotCounter = 0;
            }
        }      
    }


