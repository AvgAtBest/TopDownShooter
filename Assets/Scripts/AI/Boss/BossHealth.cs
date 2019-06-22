using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 450f;
    public float curHealth;
    public bool isAlive;
    public Collider[] colliders;
	// Use this for initialization
	void Start ()
    {
        curHealth = maxHealth;
        isAlive = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0 && isAlive)
        {

            Dead();
        }
	}
    void Dead()
    {
        isAlive = !isAlive;
        if (!isAlive)
        {
            gameObject.SetActive(false);
        }

    }
    public void TakeDamage(float damage)
    {

        curHealth -= damage;
    }
}
