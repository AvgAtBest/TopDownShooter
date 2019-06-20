using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{ 
public class Item_Ammo : MonoBehaviour
{
    private Item_Master
    private string ammoName;
    public int quantity;
    public bool isTriggerPickup;

    void OnDisable()
    {
        SetInitialReferences();
    }

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {

    }

    void SetInitialReferences()
    {
        if(isTriggerPickup)
        {
            if(GetComponent<Collider>())
            {
                GetComponent<Collider>().isTrigger = true;
            }
        }

        if(GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void takeAmmo()
    {
        
    }
}
