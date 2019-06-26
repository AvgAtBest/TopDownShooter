using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Rigidbody rb;
	public float bulletSpeed = 12f;

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

	private void Update()
	{
		Destroy(gameObject, 3);
	}

	public void OnCollisionEnter(Collision col)
	{
		//if it hits a enemy
		{
			if (col.gameObject)
			{

				//destroy the bullet
				Destroy(gameObject);
			}

		}


	}
}