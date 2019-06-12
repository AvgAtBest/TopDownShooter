using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToLoadBuffer : MonoBehaviour
{
    Transform finalRoomExitTrigger;
    public Transform loadBufferZoneEntrace;
    // Start is called before the first frame update
    void Start()
    {
        finalRoomExitTrigger = GameObject.Find("TriggerZone").GetComponent<Transform>();
        loadBufferZoneEntrace = GameObject.Find("LoadBufferSpawnPoint").GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == true)
        {
            other.transform.position = loadBufferZoneEntrace.transform.position;
        }

    }
}
