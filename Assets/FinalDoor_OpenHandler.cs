using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor_OpenHandler : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Player_Interaction playerInteractionCheck = other.GetComponent<Player_Interaction>();
			if(playerInteractionCheck.hasObtainedKey == true)
			{
				gameObject.SetActive(false);
				playerInteractionCheck.hasObtainedKey = false;
				//playerInteractionCheck.hasObtainedKey = false;
			}
		}
	}
}
