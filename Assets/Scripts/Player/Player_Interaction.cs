using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    private Camera cam;
    public Transform muzzleSpawn;
    public GameObject bulletTest;
    void Start()
    {
        muzzleSpawn = GameObject.Find("Muzzle").GetComponentInChildren<Transform>();
        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        //Look at wherre mouse is
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength = 100f;
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 hitPoint = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(hitPoint.x, transform.position.y, hitPoint.z));
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        Debug.Log("Shooting");
        
        //GameObject clone = Instantiate(bulletTest, muzzleSpawn.position, muzzleSpawn.rotation);
        //clone.AddComponent<Rigidbody>();

    }
}
