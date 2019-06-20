using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Camera_Track_Player : MonoBehaviour
{
	[Header("Follow")]
	public Transform target;
	public Camera trackingCamera;
	public Vector3 offset;
	[Header("Smoothing")]
	public float smoothing = 0.2f;
	public Vector3 velocity = Vector3.zero;
	public float turnSpeed = 2f;
	public Vector3 startPos;
	public float xRot = 90f;
	//Transform ddol;
	void Start()
	{
		//startPos = new Vector3(0, 20f, 0);
		//this.transform.position = startPos;
		//this.gameObject.transform.eulerAngles = new Vector3(xRot, 0, 0); 
		target = GameObject.Find("Player").GetComponent<Transform>();
		//if (!target)
		//{
		//	print("You look like you're trying to spawn a player. Would you like help with that?");
		//	DungeonGenerator dunGen = GameObject.Find("DungeonTest").GetComponent<DungeonGenerator>();
		//	target = GameObject.Find("Player").GetComponent<Transform>();
		//}
		//else
		//{
		//	print("You already have a player you greedy shit");
		//}

		//offset = transform.position - target.position;


		trackingCamera = Camera.main.GetComponent<Camera>();
		if(target != null)
		{
			TopDownCamView();
		}


		//target = GameObject.Find("Player").GetComponent<Transform>();
		////offset = transform.position - target.position;


		//trackingCamera = Camera.main.GetComponent<Camera>();
		//if (target == true)
		//{
		//	TopDownCamView();
		//}
		//DontDestroyOnLoad(this);
	}


	private void FixedUpdate()
	{
		if(target != null)
		{
			Vector3 averagePos = target.localPosition;
			averagePos.y = transform.position.y;
			transform.position = Vector3.SmoothDamp(transform.position, averagePos, ref velocity, smoothing);
		}

		

	}

	private void TopDownCamView()
	{
		startPos = new Vector3(0, 20f, 0);
		trackingCamera.transform.position = startPos;
		trackingCamera.transform.eulerAngles = new Vector3(xRot, 0, 0);
	}
}
