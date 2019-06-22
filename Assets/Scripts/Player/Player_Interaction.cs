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
			FadeyBoi fade = FindObjectOfType<FadeyBoi>();
			if (fade)
			{
				fade.SetSpriteTransparency(1);
			}
			hasObtainedKey = true;
			Destroy(collision.gameObject);
		}
	}

}
