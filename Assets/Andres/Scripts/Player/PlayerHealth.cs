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

        public int maxHealth = 100;
        public Text healthText;
        public int curHealth;
        public bool isDead;
        public int playerHealth;

        void OnEnable()
        {
            
        }

        void Start()
        {
            isDead = false;
            curHealth = maxHealth;
        }

        void Update()
        {
            if (curHealth >= maxHealth)
            {
                curHealth = maxHealth;
            }
        }
        public void TakeDamage(int damage)
        {
            curHealth -= damage;

            if (curHealth <= 0)
            {
                curHealth = 0;
                Dead();
            }

            SetUI();
        }

        void SetUI()
        {
            if(healthText !=null)
            {
                healthText.text = playerHealth.ToString();
            }
        }

        public void Dead()
        {
            isDead = true;
            Destroy(gameObject);
        }
    }
}
