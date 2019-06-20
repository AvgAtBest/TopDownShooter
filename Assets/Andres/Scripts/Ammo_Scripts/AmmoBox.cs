using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class AmmoBox : MonoBehaviour
    {
        private Player_Master player_Master;

        [System.Serializable]
        public class AmmoTypes
        {
            public string ammoName;
            public int ammoCurrentClip;
            public int ammoMaxReserve;

            public AmmoTypes(string aName, int aMaxReserve, int aCurrentClip)
            {
                ammoName = aName;
                ammoMaxReserve = aMaxReserve;
                ammoCurrentClip = aCurrentClip;
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
                    typesOfAmmunition[i].ammoCurrentClip += quantity;

                    if(typesOfAmmunition[i].ammoCurrentClip>typesOfAmmunition[i].ammoMaxReserve)
                    {
                        typesOfAmmunition[i].ammoCurrentClip = typesOfAmmunition[i].ammoMaxReserve;
                    }

                    player_Master.CallEventAmmoChanged();

                    break;
                }
            }
        }
    }
}
