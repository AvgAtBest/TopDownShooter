using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Spin : MonoBehaviour
{

		public float speed = 40;

		public Vector3 axis;

		void Update()
		{
		//rotates the object on the axis selected by its speed (inspector set to 1 on the y, so it rotates slowly on the y axis)
			transform.Rotate(axis, speed * Time.deltaTime);
		}
	
}
