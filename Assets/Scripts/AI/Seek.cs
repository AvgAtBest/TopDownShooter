using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
	public float stoppingDistance = 1f;

	public override void OnDrawGizmosSelected(AI owner)
	{
		Gizmos.color = Color.blue;
		float distance = Vector3.Distance(owner.target.position, owner.transform.position);
		Gizmos.DrawWireSphere(owner.transform.position, distance - stoppingDistance);
	}
	public override Vector3 GetForce(AI owner)
	{
		//Create a value to return later
		Vector3 force = Vector3.zero;
		float distance = Vector3.Distance(owner.transform.position, owner.target.position);
		//Modify value here
		if (distance > stoppingDistance)
		{
			if (owner.hasTarget)
			{
				//Get direction to target from ai agent
				force += owner.target.position - owner.transform.position;
			}
		}

		//Return value
		return force.normalized;
	}
}
