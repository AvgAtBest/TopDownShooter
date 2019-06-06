using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [System.Serializable]
    public class DropAmmo
    {
        public string name;
        public GameObject item;
        public int dropRarity;
    }

    public List<DropAmmo> LootTable = new List<DropAmmo>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
