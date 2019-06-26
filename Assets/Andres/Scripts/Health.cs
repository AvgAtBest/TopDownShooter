using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	public float curHealth;
	public float maxHealth = 100f;
	public Animator anim;
	// Start is called before the first frame update
	public Loot loot;
	bool isGeneratingLoot;
	public RectTransform healthBar;
	public NPC_Enemy aiState;
	void Start()
	{
		aiState = this.GetComponent<NPC_Enemy>();
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
		if(curHealth < maxHealth)
		{
			aiState.SetState(NPC_EnemyState.INSPECT);
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
		//change the rect transform size of the foreground health bar to be shorter when taking damage
		healthBar.sizeDelta = new Vector2(curHealth * 2, healthBar.sizeDelta.y);
	}
	void Dead()
	{

		//turns off the ai state

		NPCSensor_Sight ai2 = this.GetComponent<NPCSensor_Sight>();
		aiState.enabled = false;
		aiState.navMeshAgent.speed = 0f;
		aiState.navMeshAgent.velocity = Vector3.zero;
		aiState.navMeshAgent.angularSpeed = 0f;
		ai2.enabled = false;


		if (isGeneratingLoot == false)
		{
			//calculates the loot to drop
			isGeneratingLoot = true;
			loot.CalculateLoot(this.transform);
		}
		
		print("YOU DIED");
		//play death animation
		anim.SetFloat("Speed", 0);
		anim.SetBool("Attack", false);
		anim.SetBool("IsDead", true);

		//destroy the gameobject
		Destroy(gameObject, 4);
	}
	//private void OnGUI()
	//{
	//	Vector2 targetPos;
	//	targetPos = Camera.main.WorldToViewportPoint(transform.position);

	//	GUI.Box(new Rect(targetPos.x, Screen.height - transform.position.y, 60, 20), curHealth + "/" + maxHealth);
	//}
}
