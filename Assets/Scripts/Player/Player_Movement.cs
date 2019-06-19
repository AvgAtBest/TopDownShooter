using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BT;

public class Player_Movement : MonoBehaviour
{
	public float speed = 10f;

	//public float offset;

	private Rigidbody rigid;
	private Vector3 motion;
	private Camera cam;
	public bool isTopDown;
	public Transform spawnLocation;
	public bool hasPlayerSpawnedIn;
	Transform ddol;
	private Animator anim;
	private int AnimSpeed = 10;
	public GunController theGun;

	public bool debugMode;
	private void Awake()
	{

	}
	void Start()
	{
		rigid = GetComponent<Rigidbody>();
		cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		anim = GetComponentInChildren<Animator>();
		rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		isTopDown = true;
		//if (hasPlayerSpawnedIn == false)
		//{

		//	spawnLocation = GameObject.Find("SpawnNode").GetComponent<Transform>();
		//	this.transform.position = spawnLocation.transform.position;
		//	hasPlayerSpawnedIn = true;
		//}
		//ddol = GameObject.Find("DontDestroyOnLoad").GetComponent<Transform>();
		//SetParent(ddol);
		DontDestroyOnLoad(this.gameObject);

	}
	void Update()
	{

		float inputH = Input.GetAxis("Horizontal") * speed;
		float inputZ = Input.GetAxis("Vertical") * speed;
		if (isTopDown == true)
		{
			Vector3 moveDir = new Vector3(inputH, 0f, inputZ);

			Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);

			//switch (transform.rotation.y)
			//{
			//	case 
			//	default:
			//		break;
			//}

			//If the player is inputing movement...
			if (moveDir != Vector3.zero)
			{
				if (debugMode)
				{
					print(string.Format("{0} is facing {1}", transform.name, transform.eulerAngles.y));
				}

				//...and their rotation is within these ranges...
				//There's three this way because rotation does not care for negative values (they do not exist, refer to 360 minus the value for what you want)
				if ((BaneMath.NumberWithinRange(transform.eulerAngles.y, 0, 45) || 
						 BaneMath.NumberWithinRange(transform.eulerAngles.y, 315, 360)) || 
						 BaneMath.NumberWithinRange(transform.eulerAngles.y, 135, 225))
				{
					if (debugMode)
					{
						print("FACING UPWAYS");
					}

					//...set the animations to the chosen ones.
					anim.SetFloat("Horizontal", inputH);
					anim.SetFloat("Vertical", inputZ);
				}
				//If the rotation is within these ranges...
				else if (BaneMath.NumberWithinRange(transform.eulerAngles.y, 45, 135) || 
						BaneMath.NumberWithinRange(transform.eulerAngles.y, 225, 315))
				{
					if (debugMode)
					{
						print("FACING SIDEWAYS");
					}

					//...offset the animations chosen to the opposite ones.
					anim.SetFloat("Horizontal", inputZ);
					anim.SetFloat("Vertical", inputH);
				}
			}
			//If there's no input, set the animations to 0 to allow the player to idle.
			else
			{
				anim.SetFloat("Horizontal", 0);
				anim.SetFloat("Vertical", 0);
			}

			//anim.SetFloat("Horizontal", inputH);
			//anim.SetFloat("Vertical", inputZ);

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
		if (isTopDown == false)
		{
			Vector3 moveDir = new Vector3(-inputH, 0f, -inputZ);
			Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);
			rigid.velocity = force;

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 1.25f);
			transform.Translate(moveDir * Time.deltaTime, Space.World);

			anim.SetFloat("Horizontal", -inputH);
			anim.SetFloat("Vertical", -inputZ);
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
		if (theGun)
		{
			if (Input.GetMouseButton(0))
				theGun.isFiring = true;

			if (Input.GetMouseButtonUp(0))
				theGun.isFiring = false;
		}
	}
	public void SetParent(Transform newParent)
	{
		this.transform.SetParent(newParent, false);

	}

}
