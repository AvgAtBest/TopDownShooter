using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    GameObject player;
    NavMeshAgent enemy;
    public Loot loot;
    public float curHealth;
    public float maxHealth = 100f;
    // Use this for intialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<NavMeshAgent>();
        //loot = GameObject.FindGameObjectWithTag("Loot").GetComponent<Loot>();
        curHealth = maxHealth;
    }

    

    void Update()
    {
        enemy.destination = player.transform.position;
        //RUN ON DEATH      curHealth is less than or equal too 0 then run death   Death();
        if(curHealth <= 0)
        {
            DropLoot();
            Death();
        }

    }

    public GameObject Drop;

    public void DropLoot()
    {
        
        loot.CalculateLoot(this.transform);
        
    }
    public void Death()
    {

        Destroy(this.gameObject);
        //Instantiate(Drop, transform.position, transform.rotation);
    }
}
