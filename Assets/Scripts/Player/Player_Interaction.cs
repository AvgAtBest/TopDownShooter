using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player_Interaction : MonoBehaviour
{
	public bool hasObtainedKey;
	public int cashAmount;
	public TextMeshProUGUI moneyIndicator;
	public TextMeshProUGUI floorsClearedIndicator;
	public int floorsCleared;
	public void Start()
	{
		floorsCleared = 0;
		cashAmount = 0;
		moneyIndicator.text = cashAmount.ToString();
		floorsClearedIndicator.text = floorsCleared.ToString();
	}
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
	public void FloorsCleared(int floors)
	{

		//adds the amount of floors cleared
		floorsCleared += floors;
		floorsClearedIndicator.text = floorsCleared.ToString();
	}
	public void AddCash(int pickedUpCash)
	{
		//adds pickupcash value to cash amount
		cashAmount += pickedUpCash;
		//updates money indicator text
		moneyIndicator.text = cashAmount.ToString();
	}
}
