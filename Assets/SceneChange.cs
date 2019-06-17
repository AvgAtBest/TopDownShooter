using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	public Transform startPoint;
	private void Start()
	{
		//startPoint = GameObject.Find("SpawnNode").GetComponent<Transform>();
	}
	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "Player")
		{
			startPoint = GameObject.Find("SpawnNode").GetComponent<Transform>();
			other.gameObject.transform.position = startPoint.transform.position;
			Scene();
		}

	}

	public void Scene()
	{
		SceneManager.LoadScene("RoomCreationTest");
		Debug.Log("Scene change");
	}
}
