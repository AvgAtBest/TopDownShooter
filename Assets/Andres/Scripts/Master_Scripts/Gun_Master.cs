using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class Gun_Master : MonoBehaviour
    { 
        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventPlayerInput;
        public event GeneralEventHandler EventRequestReload;

        public delegate void GunHitEventHandler(Vector3 hitPosition, Transform hitTransform);
        public event GunHitEventHandler EventShotDefault;
        public event GunHitEventHandler EventShotEnemy;

        public delegate void GunAmmoEventHandler(int currentAmmo, int carriedAmmo);
        public event GunAmmoEventHandler EventAmmoChanged;

        public bool isGunLoaded;
        public bool isReloading;
        
        public void CallEventPlayerInput()
        {
            if(EventPlayerInput !=null)
            {
                EventPlayerInput();
            }
        }

        public void CallEventRequestReload()
        {
            if(EventRequestReload != null)
            {
                EventRequestReload();
            }
        }

        public void CallEventShotDefault(Vector3 hPos, Transform hTransform)
        {
            if(EventShotDefault !=null)
            {
                EventShotDefault(hPos, hTransform);
            }
        }
        public void CallEventEnemy(Vector3 hPos, Transform hTransform)
        {
            if(EventShotEnemy !=null)
            {
                EventShotEnemy(hPos, hTransform);
            }
        }
        public void CallEventAmmoChanged(int curAmmo, int carAmmo)
        {
            if(EventAmmoChanged !=null)
            {
                EventAmmoChanged(curAmmo, carAmmo);
            }
        }
    }
}
