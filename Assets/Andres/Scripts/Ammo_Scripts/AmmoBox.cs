using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Can carry ammo and ammo as a pickup
namespace TopDownShooter
{
    
    public class AmmoBox : MonoBehaviour
    {
        private Player_Master player_Master;

        [System.Serializable]
        public class AmmoTypes
        {
            public string ammoName;
            public int ammoClipSize = 10;
            public int ammoMaxReserve = 300;

            public AmmoTypes(string aName, int aMaxReserve, int aClipSize)
            {
                ammoName = aName;
                ammoMaxReserve = aMaxReserve;
                ammoClipSize = aClipSize;
            }
        }

        public List<AmmoTypes> typesOfAmmunition = new List<AmmoTypes>();

        void OnEnable()
        {
            SetInitialReferences();
            player_Master.EventPickedUpAmmo += PickedUpAmmo;
        }

        void OnDisable()
        {
            player_Master.EventPickedUpAmmo -= PickedUpAmmo;
        }

        void SetInitialReferences()
        {
            player_Master = GetComponent<Player_Master>();
        }

        void PickedUpAmmo(string ammoName, int quantity)
        {
            for(int i = 0; i < typesOfAmmunition.Count; i++)
            {
                if(typesOfAmmunition[i].ammoName == ammoName)
                {
                    typesOfAmmunition[i].ammoClipSize += quantity;

                    if(typesOfAmmunition[i].ammoClipSize>typesOfAmmunition[i].ammoMaxReserve)
                    {
                        typesOfAmmunition[i].ammoClipSize = typesOfAmmunition[i].ammoMaxReserve;
                    }

                    player_Master.CallEventAmmoChanged();

                    break;
                }
            }
        }
    }
}
