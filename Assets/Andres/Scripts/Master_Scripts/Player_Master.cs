using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class Player_Master : MonoBehaviour
    {
        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventAmmoChanged;

        public delegate void AmmoPickupEventHandler(string ammoName, int quantity);
        public AmmoPickupEventHandler EventPickedUpAmmo;

        public delegate void PlayerHealthEventHandler(float healthChange);
        public event PlayerHealthEventHandler EventPlayerHealthDeduction;
        public PlayerHealthEventHandler EventPlayerHealthIncrease;

        public void CallEventAmmoChanged()
        {
            if(EventAmmoChanged !=null)
            {
                EventAmmoChanged();
            }
        }

        public void CallEventPickedUpAmmo(string ammoName, int quantity)
        {
            if (EventPickedUpAmmo != null)
            {
                EventPickedUpAmmo(ammoName, quantity);
            }
        }

        public void CallEventPlayerHealthDeduction(int dmg)
        {
            if (EventPlayerHealthDeduction != null)
            {
                EventPlayerHealthDeduction(dmg);
            }
        }

        public void CallEventPlayerHealthIncrease(int increase)
        {
            if (EventPlayerHealthIncrease != null)
            {
                EventPlayerHealthIncrease(increase);
            }
        }
    }
}
