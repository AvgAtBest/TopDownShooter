using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
	public float speed = 10f;

    public float offset;

	private Rigidbody rigid;
	private Vector3 motion;
	private Camera cam;
	public bool isTopDown;
	void Start()
	{
		rigid = GetComponent<Rigidbody>();
		cam = FindObjectOfType<Camera>();
		rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}
	void Update()
	{
		float inputH = Input.GetAxis("Horizontal") * speed;
		float inputZ = Input.GetAxis("Vertical") * speed;

		Vector3 moveDir = new Vector3(inputH, 0f, inputZ);
		//moveDir = transform.TransformDirection(moveDir);

		Vector3 camEuler = cam.transform.localEulerAngles;
		moveDir = Quaternion.AngleAxis(camEuler.y, Vector3.up) * moveDir;

		Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);

		rigid.velocity = force;

        Vector3 camPos = cam.WorldToScreenPoint(Input.mousePosition) - transform.position;
        camPos.Normalize();
        float rotY = Mathf.Atan2(camPos.x, camPos.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, rotY + offset, 0f);
        //transform.LookAt(new Vector3(camPos.x, transform.position.y, camPos.z));
        
		//Quaternion playerRot = Quaternion.AngleAxis(camEuler.y, Vector3.up);

		//transform.rotation = playerRot;

	}
	//void Move(float inputH, float inputZ)
	//{
	//    Vector3 direction = new Vector3(inputH, 0f, inputZ);

	//    motion.x = direction.x * speed;
	//    motion.z = direction.z * speed;

	//}
}
