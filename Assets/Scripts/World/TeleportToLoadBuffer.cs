using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToLoadBuffer : MonoBehaviour
{
	Transform finalRoomExitTrigger;
	public Transform loadBufferZoneEntrace;
	// Start is called before the first frame update
	void Start()
	{
		finalRoomExitTrigger = GameObject.Find("TriggerZone").GetComponent<Transform>();
		loadBufferZoneEntrace = GameObject.Find("LoadBufferSpawnPoint").GetComponent<Transform>();
	}

	private void OnTriggerEnter(Collider other)
	{
		//if the other gameobject that is collider isnt null
		if (other.gameObject == true)
		{
			//and if its the player
			if (other.gameObject.name == "Player")
			{
				//gets the camera manager
				Camera_Manager camMang = GameObject.Find("CameraManager").GetComponent<Camera_Manager>();
				//switches to the side view camera, and turns off main camera
				camMang.SendMessage("LoadCamSwitchTo");
				//teleport the player
				other.transform.position = loadBufferZoneEntrace.transform.position;
				//changes the players movement to side scroll view
				other.GetComponent<Player_Movement>().isTopDown = false;
			}


		}

	}
}
