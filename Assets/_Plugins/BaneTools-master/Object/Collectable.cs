using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : OnTriggerEvent
{
	public virtual void Pickup()
	{
		DestroyObject();
	}
	public void DestroyObject()
  {
    Destroy(this.gameObject);
  }
}
