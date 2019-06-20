using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash_Pickup : Collectable
{
	public int cashMin = 50;
	public int cashMax = 500;
	public int cashAmount;
	public override void Pickup()
	{
		int r = Random.Range(cashMin, cashMax);
		cashAmount = r;
		//score.AddScore(cashAmount) or something like this, score being the nickname, addscore being the function
		//Add score here
		base.Pickup();
	}
}
