using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Rigidbody rb;
	public float bulletSpeed = 3f;

	public float damage;

	// Start is called before the first frame update
	void Start()
	{
		rb = this.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

	}


	public void OnCollisionEnter(Collision col)
	{
		if (col.transform.tag == "Enemy")
		{
			Health health = col.transform.GetComponent<Health>();
			if (health)
			{
				health.TakeDamage(damage);
				Destroy(gameObject);
			}

			//Destroy(col.gameObject);
		}
		if (col.gameObject.tag != "Enemy")
		{
			Destroy(gameObject);
		}

	}
	public void DestroyBullet()
	{
		Destroy(gameObject, 0.5f);
	}

}
