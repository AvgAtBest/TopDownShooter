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

		//assigns the player as the target
		target = GameObject.Find("Player").GetComponent<Transform>();


		//gets the component from the main cam
		trackingCamera = Camera.main.GetComponent<Camera>();
		//if there is a target
		if(target != null)
		{
			//switch to top down view
			TopDownCamView();
		}

	}


	private void FixedUpdate()
	{
		//if there is a target
		if(target != null)
		{
			//gets the local position of the player
			Vector3 averagePos = target.localPosition;
			averagePos.y = transform.position.y;
			//tracks the player with a smooth tracking look to it
			transform.position = Vector3.SmoothDamp(transform.position, averagePos, ref velocity, smoothing);
		}

		

	}

	private void TopDownCamView()
	{
		//the start position of the camera is 20 on the y axis
		startPos = new Vector3(0, 25f, 0);
		//sets the start pos of the camera
		trackingCamera.transform.position = startPos;
		//sets rotation to be 90 down on the x axis
		trackingCamera.transform.eulerAngles = new Vector3(xRot, 0, 0);
	}
}
