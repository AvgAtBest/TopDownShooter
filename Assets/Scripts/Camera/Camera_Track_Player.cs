using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Track_Player : MonoBehaviour
{
    public Transform target;
    private Camera trackingCamera;
    public float smoothing = 0.2f;
    private Vector3 velocity = Vector3.zero;


    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetPos = target.position;
        targetPos.y = transform.position.y;
        //smooths camera movement
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothing);
    }
}
