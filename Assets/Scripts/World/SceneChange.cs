using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	public Transform startPoint;
	public int floorsCleared = 1;
	private void Start()
	{
		//startPoint = GameObject.Find("SpawnNode").GetComponent<Transform>();
	}
	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		//if the other collider is the players collider
		if(other.gameObject.name == "Player")
		{
			//teleport the player back to the start
			startPoint = GameObject.Find("SpawnNode").GetComponent<Transform>();
			other.gameObject.transform.position = startPoint.transform.position;
			Player_Interaction floorCheck = other.GetComponent<Player_Interaction>();
			floorCheck.FloorsCleared(floorsCleared);
			//reloads the scene
			Scene();
		}

	}

	public void Scene()
	{
		Player_Movement topDownCheck = GameObject.Find("Player").GetComponent<Player_Movement>();
		//player is now in top down view
		topDownCheck.isTopDown = true;
		Camera_Manager camMan = GameObject.Find("CameraManager").GetComponent<Camera_Manager>();
		//switches back to the main camera
		camMan.SendMessage("MainCamSwitchTo");
		//reloads the scene (and thus the dungeon)
		SceneManager.LoadScene("RoomCreationTest");
		Debug.Log("Scene change");
	}
}
