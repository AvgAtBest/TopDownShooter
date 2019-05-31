﻿using System.Collections;
using System.Collections.Generic;
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
	void Start()
	{
		target = GameObject.Find("Player").GetComponent<Transform>();
		//offset = transform.position - target.position;
		
		trackingCamera = this.GetComponent<Camera>();
		
	}

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    //trackingCamera.transform.localRotation = Quaternion.Euler(new Vector3(0, target.localRotation.y, 0));
    //    Ray ray = trackingCamera.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        Vector3 average = (hit.point + target.position) / 2;

    //        average.y = transform.position.y;

    //        //smooths camera movement
    //        transform.position = Vector3.SmoothDamp(transform.position, average, ref velocity, smoothing);
    //    }

    //}
    private void FixedUpdate()
    {
        Vector3 averagePos = target.position;
        averagePos.y = transform.position.y;
        transform.position = Vector3.SmoothDamp(transform.position, averagePos, ref velocity, smoothing);
    }

    //private void LateUpdate()
    //{
    //    //#region rotation
    //    transform.position = target.position + offset;
    //    transform.LookAt(target.position);
    //    //#endregion
    //}
}
