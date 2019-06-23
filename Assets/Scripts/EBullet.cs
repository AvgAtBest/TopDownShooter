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
			if (col.transform.tag == "Player")
			{
				PlayerHealth health = col.transform.GetComponent<PlayerHealth>();
				if (health)
				{
					health.TakeDamage(damage);
					Destroy(gameObject);
				}

				//Destroy(col.gameObject);
			}
			if (col.gameObject.tag != "Player")
			{
				Destroy(gameObject);
			}
		}
	}
}