using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Collectable
{
	public int healAmount = 15;
	public override void Pickup()
	{
		PlayerHealth health = otherCollider.GetComponent<PlayerHealth>();
		health.TakeDamage(-healAmount);
		print("YOU HAVE BEEN GRACED BY HIS NOODLEY APPENDAGES");

		base.Pickup();
	}
}
