using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDive : MonoBehaviour
{



	public Vector3 dive;
	public float force = 2.0f;
	public bool isGrounded;
	public Rigidbody rb;
	public Animator anim;
	void Start()
	{
		rb = GetComponent<Rigidbody>();

		//vector3 direction to dive/jump
		dive = new Vector3(0.0f, 2.0f, 2.0f);
		anim = this.GetComponentInChildren<Animator>();
	}
	//if player collides and stays on the collider
	void OnCollisionStay()
	{
		//the player is grounded
		isGrounded = true;

		//stop playing the jump animation
		anim.SetBool("IsJumping", false);
	}
	//if player collides and exits  the collider
	void OnCollisionExit()
	{
		//the player isnt grounded
		isGrounded = false;
	}

	void Update()
	{
		//if the player hits the space bar and is grounded
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			//adds force to the rigidbody via the dive Vector3 in start
			rb.AddForce(dive * force, ForceMode.Impulse);
			//play the jump animation
			anim.SetBool("IsJumping", true);

			//player isnt grounded
			isGrounded = false;
		}

	}


}
