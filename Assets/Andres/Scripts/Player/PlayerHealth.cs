using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
		public TextMeshProUGUI youareDeadText;
		public TextMeshProUGUI highScoreIndicator;
        [SerializeField] public GameObject GameOverUI;
		void OnEnable()
		{

		}

		void Start()
		{
            GameOverUI.SetActive(false);
			isDead = false;
			curHealth = maxHealth;
			anim = this.GetComponentInChildren<Animator>();
		}

		void Update()
		{
			//if the curhealth is greater than maxhealth
			if (curHealth >= maxHealth)
			{
				//make sure it doesnt overflow
				curHealth = maxHealth;
			}
			//set the ui
			SetUI();
			//if the curhealth is less than 0
			if (curHealth <= 0)
			{
				//we are dead
				Dead();
			}
		}
		public void TakeDamage(int damage)
		{
			//curhealth is less than the amount of damage dealt
			curHealth -= damage;

			//if (curHealth <= 0)
			//{

			//	Dead();
			//	curHealth = 0;
			//}


		}

		void SetUI()
		{
			//if there is a health slider and its not null
			if (healthSlider != null)
			{
				//the value of the health slider is equal to the curhealth
				healthSlider.value = curHealth;
			}
			//if curhealth is greater than 0
			if (curHealth > 0)
			{
				//disable the "You are dead" text
				youareDeadText.enabled = false;
				//disable the high score text
				highScoreIndicator.enabled = false;
                GameOverUI.SetActive (false);
			}
		}

		public void Dead()
		{
			Player_Interaction updateHighScore = GetComponent<Player_Interaction>();
			Player_Movement disablePlayerMovement = GetComponent<Player_Movement>();
			//freeze the rigidbody rotation
			disablePlayerMovement.rigid.constraints = RigidbodyConstraints.FreezeRotation;
			//the player is marked as dead
			disablePlayerMovement.isDead = true;
            GameOverUI.SetActive(true);
            //enable the "You are dead" text
            youareDeadText.enabled = true;
			//enable the high score text
			highScoreIndicator.enabled = true;
			highScoreIndicator.text = "You cleared " + updateHighScore.floorsCleared.ToString() + " floors and grabbed " + updateHighScore.cashAmount.ToString() + " cash. Try again?";
			//the player is dead
			isDead = true;
			//play death animation
			anim.SetBool("IsDead", true);
            //Destroy(gameObject);

		}
	}
}
