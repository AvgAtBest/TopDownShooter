using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour //Andres
{
    [System.Serializable]
    public class DropAmmo
    {
        public string name;
        public GameObject item;
        public int dropRarity;
    }

    public List<DropAmmo> LootTable = new List<DropAmmo>();
    public int dropChance;

    public void CalculateLoot(Transform enemy)
    {


        int itemWeight = 0;

        for (int i = 0; i < LootTable.Count; i++)
        {
            itemWeight += LootTable[i].dropRarity;
        }

        int randomValue = Random.Range(0, itemWeight);//80

        for (int i = 0; i < LootTable.Count; i++)
        {
            if (randomValue <= LootTable[i].dropRarity)
            {
                Instantiate(LootTable[i].item, enemy.position, Quaternion.identity);
                return;
            }
            randomValue -= LootTable[i].dropRarity;
        }

    }
}
