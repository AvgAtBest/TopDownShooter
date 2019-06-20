using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    GameObject player;
    NavMeshAgent enemy;

    // Use this for intialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<NavMeshAgent>();
        //loot = GameObject.FindGameObjectWithTag("Loot").GetComponent<Loot>();
    }

    void Update()
    {
        enemy.destination = player.transform.position;
    }

}
