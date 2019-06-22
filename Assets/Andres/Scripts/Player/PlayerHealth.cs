using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
	public class PlayerHealth : MonoBehaviour
	{
		private GameManager_Master gameManager_Master;
		private Player_Master player_Master;
		public Slider healthSlider;

		public int maxHealth = 100;

		public int curHealth;
		public bool isDead;
		public int playerHealth;
		public Animator anim;
		void OnEnable()
		{

		}

		void Start()
		{
			isDead = false;
			curHealth = maxHealth;
			anim = this.GetComponentInChildren<Animator>();
		}

		void Update()
		{
			if (curHealth >= maxHealth)
			{
				curHealth = maxHealth;
			}
			SetUI();
			if(curHealth <= 0)
			{
				Dead();
			}
		}
		public void TakeDamage(int damage)
		{
			curHealth -= damage;

			//if (curHealth <= 0)
			//{

			//	Dead();
			//	curHealth = 0;
			//}


		}

		void SetUI()
		{
			if (healthSlider != null)
			{
				healthSlider.value = curHealth;
			}
		}

		public void Dead()
		{
			
			Player_Movement disablePlayerMovement = GetComponent<Player_Movement>();
			disablePlayerMovement.rigid.constraints = RigidbodyConstraints.FreezeRotation;
			disablePlayerMovement.isDead = true;
			
			isDead = true;
			anim.SetBool("IsDead", true);
			//Destroy(gameObject);
		}
	}
}
