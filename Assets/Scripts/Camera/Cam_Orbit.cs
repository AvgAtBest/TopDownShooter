using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Orbit : MonoBehaviour
{
    private Vector3 offset;
    public Transform target;
    public float turnSpeed = 5f;
    void Start()
    {
        offset = new Vector3(target.position.x, target.position.y + 8.0f, target.position.z + 7.0f);
    }

    // Update is called once per frame
    void Update()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }
}
