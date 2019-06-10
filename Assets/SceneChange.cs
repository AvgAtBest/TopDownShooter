using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		Scene();
	}

	public void Scene()
	{
		SceneManager.LoadScene("RoomCreationTest");
	}
}
