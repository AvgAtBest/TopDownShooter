using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed = 10f;

    private CharacterController charC;
    private Vector3 motion;
    private Camera cam;
    void Start()
    {
        charC = GetComponent<CharacterController>();
        cam = FindObjectOfType<Camera>();
    }
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        Move(inputH, inputZ);
        charC.Move(motion * Time.deltaTime);

        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength = 100f;
        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 hitPoint = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(hitPoint.x, transform.position.y, hitPoint.z));
        }


    }
    void Move(float inputH, float inputZ)
    {
        Vector3 direction = new Vector3(inputH, 0f, inputZ);

        motion.x = direction.x * speed;
        motion.z = direction.z * speed;

    }
}
