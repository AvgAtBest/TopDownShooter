using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{
    public int[] table =
     {
        60, //Ammo
        40, //Health
    };
    private int total;
    public int randomNumber;
    private void Start()
    {
        foreach(var item in table)
        {
            total += item;
        }
        randomNumber = Random.Range(0, total);
        //randomNumber = 49
        //is 49 <= 60
        //Health

        //randomNumber = 61
        //is 61 < 60
        //no = ?
        //61 - 60 = 1
        //1 <= 30?
        //Ammo
    }
}
