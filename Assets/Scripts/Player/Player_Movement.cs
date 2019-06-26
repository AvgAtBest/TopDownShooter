using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BT;


public class Player_Movement : MonoBehaviour
{

	public float speed = 13f;

	//public float offset;

	public Rigidbody rigid;
	private Vector3 motion;
	private Camera cam;
	public bool isTopDown;
	public Transform spawnLocation;
	public bool hasPlayerSpawnedIn;
	//Transform ddol;
	private Animator anim;
	private int AnimSpeed = 10;
	public GunController theGun;
	private float slideSpeed = 20f;
	public bool debugMode;
	public bool isDead = false;

	void Start()
	{
		//getting the component boys
		rigid = GetComponent<Rigidbody>();
		cam = GameObject.Find("Main Camera")?.GetComponent<Camera>();
		anim = GetComponentInChildren<Animator>();
		//freezes the x and z rotation
		rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		isTopDown = true;
		hasPlayerSpawnedIn = true;


	}
	void Update()
	{
		//if the player isnt dead
		if (isDead != true)
		{

			//if there isnt a spawn location
			if (!spawnLocation)
			{
				//spawn the player at the spawn node
				spawnLocation = GameObject.Find("SpawnNode").GetComponent<Transform>();
				SpawnPlayer();
			}
			//gets the horizontal and vertical input
			float inputH = Input.GetAxis("Horizontal") * speed;
			float inputZ = Input.GetAxis("Vertical") * speed;
			//if the player is in top down view
			if (isTopDown == true)
			{
				//moves the player via input (inputH is horizontal movement on X, inputZ is up and down on Z)
				Vector3 moveDir = new Vector3(inputH, 0f, inputZ);
				//apply force to rigidbody
				Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);


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
				//the velocity of the rigid body is equal to the vector3 force
				rigid.velocity = force;
				//shoots a ray from the camera to where the mouse pos is to a plane
				Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
				Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
				float rayLength = 1000f;
				//if the ray hits a plane
				if (groundPlane.Raycast(cameraRay, out rayLength))
				{
					//rotate the player to where the ray hits via mouse position
					Vector3 hitPoint = cameraRay.GetPoint(rayLength);
					transform.LookAt(new Vector3(hitPoint.x, transform.position.y, hitPoint.z));

				}
				//if the player pushes C
				if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftControl))
				{
					//Start Coroutine for Slide
					//SLIDEY TIME
					StartCoroutine(Slide(1f));

				}

			}
			//if the player is in the side mode view
			if (isTopDown == false)
			{
				//sets horizontal to 0 to make sure no strafing is played
				anim.SetFloat("Horizontal", 0);
				//sets rotation to 0
				transform.Rotate(0, 0, 0);
				//move direction is now reversed
				Vector3 moveDir = new Vector3(-inputH, 0f, -inputZ);
				//apply force to rigidbody
				Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);
				rigid.velocity = force;
				//player rotates to the direction of player input
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 1.25f);
				transform.Translate(moveDir * Time.deltaTime, Space.World);


				//if the player is looking between these angles
				anim.SetFloat("Vertical", -inputH);
				if ((BaneMath.NumberWithinRange(transform.eulerAngles.y, 0, 45) ||
					 BaneMath.NumberWithinRange(transform.eulerAngles.y, 315, 360)) ||
					 BaneMath.NumberWithinRange(transform.eulerAngles.y, 135, 225))
				{
					//make sure to play the run animation regardless of direction
					anim.SetFloat("Vertical", -inputZ);
				}

			}


			//if the gun is there
			if (theGun)
			{
				//if the left mouse button is pressed
				if (Input.GetMouseButton(0))
					//Fire the gun
					theGun.Fire();


				//else if the R key is pressed
				else if (Input.GetKeyDown(KeyCode.R))
					//Reload
					theGun.Reload();

			}
		}
	}
	public void SpawnPlayer()
	{
		//current position is now equal to the spawn location position in the world
		transform.position = spawnLocation.transform.position;


	}


	public void SetSpawned()
	{
		print("Spawned in");
		//the player has spawned in
		hasPlayerSpawnedIn = true;
	}

	private IEnumerator Slide(float waitTime)
	{
		//adjust the speed of the player to the slide speed
		speed = slideSpeed;
		//play animation
		anim.SetBool("IsSliding", true);
		//wait for the amount of time (1f sec)
		yield return new WaitForSeconds(waitTime);
		//revert back to original
		anim.SetBool("IsSliding", false);
		speed = 13f;
	}
}

