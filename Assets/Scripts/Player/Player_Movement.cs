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
	public Transform spawnLocation;
	public bool hasPlayerSpawnedIn = false;
	Transform ddol;
	public Transform spine;

  public GunController theGun;

	void Start()
	{
		rigid = GetComponent<Rigidbody>();
		cam = FindObjectOfType<Camera>().GetComponent<Camera>();
		rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		isTopDown = true;
		if (hasPlayerSpawnedIn == false)
		{
			hasPlayerSpawnedIn = true;
			spawnLocation = GameObject.Find("SpawnNode").GetComponent<Transform>();
			this.transform.position = spawnLocation.transform.position;
		}
		ddol = GameObject.Find("DontDestroyOnLoad").GetComponent<Transform>();
		SetParent(ddol);

	}
	void Update()
	{
		float inputH = Input.GetAxis("Horizontal") * speed;
		float inputZ = Input.GetAxis("Vertical") * speed;
		if(isTopDown == true)
		{
			Vector3 moveDir = new Vector3(inputH, 0f, inputZ);
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
		if(isTopDown == false)
		{
			Vector3 moveDir = new Vector3(-inputH, 0f, -inputZ);
			Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);
			rigid.velocity = force;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 1.25f);
			transform.Translate(moveDir * Time.deltaTime, Space.World);
		}

		//moveDir = transform.TransformDirection(moveDir);

		//Vector3 camEuler = cam.transform.localEulerAngles;
		//moveDir = Quaternion.AngleAxis(camEuler.y, Vector3.up) * moveDir;

		//Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);

		////rigid.velocity = force;
		//if(isTopDown == true)
		//{
		//	Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
		//	Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		//	float rayLength = 1000f;
		//	if (groundPlane.Raycast(cameraRay, out rayLength))
		//	{
		//		Vector3 hitPoint = cameraRay.GetPoint(rayLength);
		//		transform.LookAt(new Vector3(hitPoint.x, transform.position.y, hitPoint.z));
		//	}
		//}
		//else if(isTopDown == false)
		//{

		//	transform.rotation = Quaternion.LookRotation(moveDir);
		//}
		if (Input.GetMouseButton(0))
			theGun.isFiring = true;

		if (Input.GetMouseButtonUp(0))
			theGun.isFiring = false;
	}
	public void SetParent(Transform newParent)
	{
		this.transform.SetParent(newParent,false);
		
	}

}
