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

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		dive = new Vector3(0.0f, 2.0f, 2.0f);
	}

	void OnCollisionStay()
	{
		isGrounded = true;
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
			isGrounded = false;
		}
	}


}
