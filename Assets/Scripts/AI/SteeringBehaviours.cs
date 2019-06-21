using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : ScriptableObject
{
  public float weighting = 1f;
	public abstract Vector3 GetForce(AI owner);
	public virtual void OnDrawGizmosSelected(AI owner) { }
}
