using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
	public float speed = 10f;

    //public float offset;

	private Rigidbody rigid;
	private Vector3 motion;
	private Camera cam;
	public bool isTopDown;
	void Start()
	{
		rigid = GetComponent<Rigidbody>();
		cam = FindObjectOfType<Camera>().GetComponent<Camera>();
		rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
	void Update()
	{
		float inputH = Input.GetAxis("Horizontal") * speed;
		float inputZ = Input.GetAxis("Vertical") * speed;

		Vector3 moveDir = new Vector3(inputH, 0f, inputZ);
		//moveDir = transform.TransformDirection(moveDir);

		//Vector3 camEuler = cam.transform.localEulerAngles;
		//moveDir = Quaternion.AngleAxis(camEuler.y, Vector3.up) * moveDir;

		Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);

		rigid.velocity = force;

        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength = 1000f;
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 hitPoint = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(hitPoint.x, transform.position.y, hitPoint.z));
        }

    }
	//void Move(float inputH, float inputZ)
	//{
	//    Vector3 direction = new Vector3(inputH, 0f, inputZ);

	//    motion.x = direction.x * speed;
	//    motion.z = direction.z * speed;

	//}
}
