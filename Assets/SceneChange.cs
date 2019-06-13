using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "Player")
		{
			Scene();
		}

	}

	public void Scene()
	{
		SceneManager.LoadScene("RoomCreationTest");
		Debug.Log("Scene change");
	}
}
