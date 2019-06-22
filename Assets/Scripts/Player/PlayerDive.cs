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
		dive = new Vector3(0.0f, 2.0f, 2.0f);
		anim = this.GetComponentInChildren<Animator>();
	}

	void OnCollisionStay()
	{
		isGrounded = true;
		anim.SetBool("IsJumping", false);
	}
	void OnCollisionExit()
	{
		isGrounded = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{

			rb.AddForce(dive * force, ForceMode.Impulse);
			anim.SetBool("IsJumping", true);
			isGrounded = false;
		}

	}


}
