using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public float curHealth;
	public float maxHealth = 100f;
	public Animator anim;
	// Start is called before the first frame update
	public Loot loot;
	bool isGeneratingLoot;
	void Start()
	{
		anim = this.GetComponentInChildren<Animator>();
		curHealth = maxHealth;
	}

	// Update is called once per frame
	void Update()
	{
		//if the curhealth is greater than maxhealth
		if (curHealth >= maxHealth)
		{
			//makes sure it doesnt overfill
			curHealth = maxHealth;
		}
		//if curhealth is less than 0
		if (curHealth <= 0)
		{
			//Call dead function
			Dead();
		}
	}
	//take damage function
	public void TakeDamage(float damage)
	{
		curHealth -= damage;
		print("HAHA " + transform.name + " GOT HIT LOL");
	}
	void Dead()
	{

		//turns off the ai state
		NPC_Enemy ai = this.GetComponent<NPC_Enemy>();
		NPCSensor_Sight ai2 = this.GetComponent<NPCSensor_Sight>();
		ai.enabled = false;
		ai2.enabled = false;

		if (isGeneratingLoot == false)
		{
			//calculates the loot to drop
			isGeneratingLoot = true;
			loot.CalculateLoot(this.transform);
		}
		
		print("YOU DIED");
		//play death animation
		anim.SetBool("IsDead", true);
		//destroy the gameobject
		Destroy(gameObject, 3);
	}
}
