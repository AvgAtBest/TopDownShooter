using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Pickup : Collectable
{
    public int ammoQuantity = 15;

    public override void Pickup()
    {
        GunController ammoMaster = otherCollider.GetComponentInChildren<GunController>();
        ammoMaster.ObtainAmmo(ammoQuantity);
        base.Pickup();
    }

}
