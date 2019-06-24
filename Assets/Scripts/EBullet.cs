using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDownShooter
{
	public class EBullet : MonoBehaviour
	{
		public float bulletSpeed = 3f;

		public int damage;
		public void OnCollisionEnter(Collision col)
		{
			//if the enemy bullet hits the player
			if (col.transform.tag == "Player")
			{
				PlayerHealth health = col.transform.GetComponent<PlayerHealth>();
				if (health)
				{
					//take health away from the player = to the damage
					health.TakeDamage(damage);
					//destroy the bullet
					Destroy(gameObject);
				}

				//Destroy(col.gameObject);
			}
			//if it doesnt hit the player
			if (col.gameObject.tag != "Player")
			{
				//still destroy the gameobject
				Destroy(gameObject);
			}
		}
	}
}