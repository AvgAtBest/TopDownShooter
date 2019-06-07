using System.Collections;
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
    public Vector3 startPos;
    public float xRot = 90f;
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        //offset = transform.position - target.position;


        trackingCamera = Camera.main.GetComponent<Camera>();
        TopDownCamView();
    }


    private void FixedUpdate()
    {
        Vector3 averagePos = target.localPosition;
        averagePos.y = transform.position.y;
        transform.position = Vector3.SmoothDamp(transform.position, averagePos, ref velocity, smoothing);
    }
    private void Update()
    {

        
    }
    private void TopDownCamView()
    {
        startPos = new Vector3(0, 20f, 0);
        trackingCamera.transform.position = startPos;
        trackingCamera.transform.eulerAngles = new Vector3(xRot, 0, 0);
    }
    private void SideView()
    {

    }


}
