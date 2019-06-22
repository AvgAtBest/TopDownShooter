using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHealth : MonoBehaviour
{
    public float curHealth;
    public float maxHealth = 100f;

    private bool isDead;

    void Start()
    {
        curHealth = maxHealth;
        isDead = false;
    }

    void Update()
    {
        if (curHealth >= maxHealth)
        {
            curHealth = 100f;
        }

        if (curHealth <= 0 && !isDead)
        {
            isDead = true;
            Dead();
        }
    }
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
    }

    void Dead()
    {
        gameObject.SetActive(false);

    }
}
