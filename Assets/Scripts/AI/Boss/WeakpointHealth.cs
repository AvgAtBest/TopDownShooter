using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakpointHealth : MonoBehaviour
{
    public float maxHealth = 20f;
    public float curHealth;
    private BossHealth bossHealth;
    public ParticleSystem explosion;

    // Use this for initialization
    void Start()
    {
        curHealth = maxHealth;
        bossHealth = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }

    }
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
        bossHealth.curHealth -= 1000;
        if (curHealth <= 0)
        {
            Destroy();
            Explosion();

        }
    }
    public void Explosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);

    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
