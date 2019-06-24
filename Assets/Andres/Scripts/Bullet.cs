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
		//gets the rigidbody of the bullet
		rb = this.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		//adds force to rigidbody on the z axis * the bullet speed
		rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

	}


	public void OnCollisionEnter(Collision col)
	{
		//if it hits a enemy
		if (col.transform.tag == "Enemy")
		{
			Health health = col.transform.GetComponent<Health>();
			if (health)
			{
				//take health away equal to the damage dealt
				health.TakeDamage(damage);
				//destroy the bullet
				Destroy(gameObject);
			}

			//Destroy(col.gameObject);
		}
		//if it didnt hit a enemy
		if (col.gameObject.tag != "Enemy")
		{
			//destroy the bullet
			Destroy(gameObject);
		}

	}

}
