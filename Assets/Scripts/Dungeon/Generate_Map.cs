using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate_Map : MonoBehaviour
{
    public List<GameObject> corridorsToSpawn;
    public List<GameObject> roomsToSpawn;
    public GameObject spawnRoom;
    public GameObject exitRoom;
    Vector3 dungeonSpawnLocation = Vector3.zero;

    private int mapSeed = 123456789;
    private bool isMapGenerated;
    int maxRoomsToGen = 25;
    public Transform player_spawn_point;

    
    void Start()
    {
        Random.InitState(mapSeed);
        CreateRooms();
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateRooms()
    {
        //spawn the spawn room at zero vector
        Instantiate(spawnRoom, dungeonSpawnLocation, Quaternion.identity);
        this.roomsToSpawn = new List<GameObject>();
        this.corridorsToSpawn = new List<GameObject>();
    }
    void SpawnPlayer()
    {
        player_spawn_point = GameObject.Find("Player_Spawn_Point").GetComponentInChildren<Transform>();
        GameObject player = GameObject.Find("Player");
        player.transform.position = player_spawn_point.position;
    }
}
