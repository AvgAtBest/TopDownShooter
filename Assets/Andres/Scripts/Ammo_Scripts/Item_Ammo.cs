using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class Item_Ammo : MonoBehaviour
    {
        private Item_Master item_Master;
        private GameObject player_Movement;
        public string ammoName;
        public int quantity;
        public bool isTriggerPickup;

        void OnEnable()
        {
            SetInitialReferences();
            item_Master.EventObjectPickup += TakeAmmo;
        }

        void OnDisable()
        {
            item_Master.EventObjectPickup -= TakeAmmo;
        }

        void Start()
        {
            SetInitialReferences();
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(GameManager_References._playerTag)&&isTriggerPickup)
            {
                TakeAmmo();
            }
        }

        void SetInitialReferences()
        {
            item_Master = GetComponent<Item_Master>();
            player_Movement = GameManager_References._player;

            if (isTriggerPickup)
            {
                if (GetComponent<Collider>())
                {
                    GetComponent<Collider>().isTrigger = true;
                }
            }

            if (GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        void TakeAmmo()
        {
            player_Movement.GetComponent<Player_Master>().CallEventPickedUpAmmo(ammoName, quantity);
            Destroy(gameObject);
        }
    }
}
    
