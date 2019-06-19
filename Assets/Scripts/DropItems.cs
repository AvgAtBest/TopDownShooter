using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    public List<GameObject> drops = new List<GameObject>();

    private void Start()
    {
        DropRandomItem();
    }

    public void DropRandomItem()
    {
        print("Dropping a random item");
        Instantiate(drops[Random.Range(0, drops.Count)], transform.position, Quaternion.identity);
    }
}
