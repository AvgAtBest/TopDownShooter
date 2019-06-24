using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : Collectable
{
	//public int amount;
	public int minAmount = 1;
	public int maxAmount = 500;

	// Start is called before the first frame update
	public override void Pickup()
	{
		//generates a random number between 1 and 500
		int r = Random.Range(minAmount, maxAmount);
		
		Player_Interaction playerWallet = otherCollider.GetComponent<Player_Interaction>();
		//adds the random cash number
		playerWallet.AddCash(r);
		print(r);
		base.Pickup();
	}
}
