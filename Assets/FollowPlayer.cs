using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	public Transform target;
	public Transform me;

	private void Update()
	{
		//print("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

		//me.localRotation = Quaternion.Euler(new Vector3(me.localRotation.x, target.localRotation.y, me.localRotation.y));
		//me.LookAt(target);
	}
}
