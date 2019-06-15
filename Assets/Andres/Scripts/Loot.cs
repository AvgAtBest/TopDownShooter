﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [System.Serializable]
    public class DropLoot
    {
        public string name;
        public GameObject item;
        public int dropRarity;
    }

    public List<DropLoot> LootTable = new List<DropLoot>();

    public void CalculateLoot(Transform enemy)
    {
        int itemWeight = 0;

        for (int i = 0; i < LootTable.Count; i++)
        {
            itemWeight += LootTable[i].dropRarity;
        }
        int randomValue = Random.Range(0, itemWeight);//80

        for (int i = 0; i <= LootTable.Count; i++)
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
