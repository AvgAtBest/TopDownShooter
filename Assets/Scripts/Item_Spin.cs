using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Spin : MonoBehaviour
{

		public float speed = 40;

		public Vector3 axis;

		void Update()
		{

			transform.Rotate(axis, speed * Time.deltaTime);
		}
	
}
