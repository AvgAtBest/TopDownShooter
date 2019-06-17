using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    GameObject player;
	bool hasPlayerSpawned = false;
	// Start is called before the first frame update
	void Awake()
	{
		
		if (hasPlayerSpawned == false)
		{
			hasPlayerSpawned = true;
			player = GameObject.Find("Player");
			DontDestroyOnLoad(player);
		}
		

	}
}
