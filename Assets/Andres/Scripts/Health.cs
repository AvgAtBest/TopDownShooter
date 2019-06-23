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
		if (curHealth >= maxHealth)
		{
			curHealth = maxHealth;
		}
		if (curHealth <= 0)
		{
			Dead();
		}
	}
	public void TakeDamage(float damage)
	{
		curHealth -= damage;
		print("HAHA " + transform.name + " GOT HIT LOL");
	}
	void Dead()
	{
		NPC_Enemy ai = this.GetComponent<NPC_Enemy>();
		NPCSensor_Sight ai2 = this.GetComponent<NPCSensor_Sight>();
		ai.enabled = false;
		ai2.enabled = false;

		if (isGeneratingLoot == false)
		{
			isGeneratingLoot = true;
			loot.CalculateLoot(this.transform);
		}
		
		print("YOU DIED");
		anim.SetBool("IsDead", true);
		Destroy(gameObject, 3);
	}
}
