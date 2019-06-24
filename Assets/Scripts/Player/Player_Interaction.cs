using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
	public bool hasObtainedKey;
	
	private void OnCollisionEnter(Collision collision)
	{
		//if the player collides with the key
		if (collision.gameObject.CompareTag("Key"))
		{
			//gets fadeyboi, a script used to change the Alpha on a ui image
			FadeyBoi fade = FindObjectOfType<FadeyBoi>();
			if (fade)
			{
				//set the sprite transparency to be full alpha (opaque)
				fade.SetSpriteTransparency(1);
			}
			//the player has got the key
			hasObtainedKey = true;
			//remove the gameobject from the scene
			Destroy(collision.gameObject);
		}
	}

}
