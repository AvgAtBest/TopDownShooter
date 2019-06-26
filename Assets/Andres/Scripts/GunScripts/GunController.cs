using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fires Gun

public class GunController : MonoBehaviour
{
	public bool isFiring;

	public bool isReloading;
	public Bullet bullet;
	public float bulletSpeed;

	public float shootRate;
	private float shootTimer;

	public bool readyToFire;

	public Transform firePoint;
	public int ammoInClip;
	public int damage;
	public int maxClipSize = 10;
	public int ammoInReserve;
	public int ammoMaxReserve = 300;
	public bool requiresReload = false;
	//public TopDownShooter.Gun_Ammo gunAmmo;
	public TopDownShooter.Gun_AmmoUI gunAmmoUI;
	public GameObject impactEffect;
	public GameObject enemyHitEffect;
	// Start is called before the first frame update
	void Start()
	{
		ammoInClip = maxClipSize;
		ammoInReserve = ammoMaxReserve;
		gunAmmoUI.UpdateAmmoUI(ammoInClip, ammoInReserve);
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
		if (!isReloading)
		{
			if (readyToFire)
			{
				if (!readyToFire)
					return;

				if (ammoInClip <= 0)
					return;

				Vector3 rayOrigin = firePoint.transform.position;

				RaycastHit hit;

				if (Physics.Raycast(rayOrigin, firePoint.forward, out hit))
				{
					if (hit.collider.CompareTag("Enemy"))
					{
						Instantiate(enemyHitEffect, hit.point, Quaternion.identity);
					}
					if (hit.collider.tag != "Enemy")
					{
						Instantiate(impactEffect, hit.point, Quaternion.identity);
					}


					Health enemyHealth = hit.collider.GetComponent<Health>();

					if (enemyHealth != null)
					{
						enemyHealth.TakeDamage(damage);
					}
				}


				//		newBullet.bulletSpeed = bulletSpeed;
				//		newBullet.damage = damage;

				ammoInClip--;

				if (ammoInClip <= 0)
				{
					ammoInClip = 0;
				}

				// Reset shoot timer back to zero.
				shootTimer = 0;

				// Are we in a state where we COULD reload?
				requiresReload = (ammoInClip < maxClipSize);


				gunAmmoUI.UpdateAmmoUI(ammoInClip, ammoInReserve);

				//	}
				//}



			}
		}
	}
	public IEnumerator Reloading(float waitTime)
	{
		//isFiring = false;
		isReloading = true;
		readyToFire = false;

		yield return new WaitForSeconds(waitTime);
		if (ammoInReserve > 0)
		{
			if (ammoInReserve >= maxClipSize)
			{
				ammoInReserve -= maxClipSize - ammoInClip;
				ammoInClip = maxClipSize;
			}
			if (ammoInClip < maxClipSize)
			{
				int tempMag = ammoInReserve;
				ammoInClip = tempMag;
				ammoInReserve -= tempMag;
			}
		}
		isReloading = false;
		readyToFire = true;
		//isFiring = true;
		gunAmmoUI.UpdateAmmoUI(ammoInClip, ammoInReserve);

	}
	//public void Reload()
	//{
	//	if (!requiresReload)
	//		return;

	//	var ammoRequired = maxClipSize - ammoInClip;

	//	if (ammoRequired <= ammoInReserve)
	//	{
	//		ammoInClip = maxClipSize;
	//		ammoInReserve -= ammoRequired;
	//	}
	//	else if (ammoRequired > ammoInReserve)
	//	{
	//		ammoInClip += ammoInReserve;
	//		ammoInReserve = 0;
	//	}

	//	gunAmmoUI.UpdateAmmoUI(ammoInClip, ammoInReserve);

	//}
	public void Reload()
	{
		if (!isReloading)
		{
			StartCoroutine(Reloading(1.8f));
		}
	}
	public void StopFiring()
	{


	}
	public void ObtainAmmo(int ammoObtained)
	{
		ammoInReserve += ammoObtained;
		if (ammoInReserve >= ammoMaxReserve)
		{
			ammoInReserve = ammoMaxReserve;
		}
		gunAmmoUI.UpdateAmmoUI(ammoInClip, ammoInReserve);
	}
}


