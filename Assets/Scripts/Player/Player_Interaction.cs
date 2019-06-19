using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
	public bool hasObtainedKey;
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Key"))
		{
			hasObtainedKey = true;
			Destroy(collision.gameObject);
		}
	}

}
