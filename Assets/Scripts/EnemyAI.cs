using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    GameObject player;
    NavMeshAgent enemy;
    Loot loot;
    public float curHealth;

    // Use this for intialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<NavMeshAgent>();
        loot = GameObject.FindGameObjectWithTag("Loot").GetComponent<Loot>();
    }

    void Update()
    {
        enemy.destination = player.transform.position;
        //RUN ON DEATH      curHealth is less than or equal too 0 then run death   Death();
        if(curHealth <= 0)
        {
            Death();
        }

    }

    void Death()
    {
        loot.CalculateLoot(this.transform);
        Destroy(this.gameObject);
    }
}
