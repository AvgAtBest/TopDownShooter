using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float curHealth;
    public float maxHealth = 100f;
    // Start is called before the first frame update
    public Loot loot;
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            loot.CalculateLoot(this.transform);
            Dead();
        }
    }
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
        print("HAHA " + transform.name + " GOT HIT LOL");
        if (curHealth <= 0)
        {
            Dead();
            loot.CalculateLoot(this.transform);
        }
    }
    void Dead()
    {
        print("YOU DIED");
        Destroy(gameObject);
    }
}
