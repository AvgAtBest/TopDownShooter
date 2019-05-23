using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 20f;
    public float speed = 10f;
    public float maxAmmo = 20f;
    public float ammo = 20f;
    public float range = 10f;
    public Transform shootPoint;
 
    public void Start()
    {
        ammo = maxAmmo;
    }

    public void Reload()
    {
        ammo = maxAmmo;
    }

    public void Shoot()
    {
        
    }
}
