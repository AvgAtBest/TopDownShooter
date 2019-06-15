using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;
    public bool isDead;

    void Start()
    {
        isDead = false;
        curHealth = maxHealth;
    }

    void Update()
    {
        if(curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }
    }
    public void TakeDamage(int damage)
    {
        curHealth -= damage;

        if(curHealth <= 0)
        {
            curHealth = 0;
            Dead();
        }
    }
    public void Dead()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
