using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class Item_Master : MonoBehaviour
    {
        private Player_Master player_Master;

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventObjectPickup;

        public delegate void PickupActionEventHandler(Transform item);
        public event PickupActionEventHandler EventPickupAction;

        void OnEnable()
        {
            SetInitialReferences();
        }

        void OnDisable()
        {

        }

        void Start()
        {
			player_Master = GameObject.Find("Player").GetComponent<Player_Master>();
        }

        public void CallEventObjectPickup()
        {
            if(EventObjectPickup !=null)
            {
                EventObjectPickup();
            }
        }

        public void CallEventPickupAction(Transform item)
        {
            if(EventPickupAction !=null)
            {
                EventPickupAction(item);
            }
        }

        void SetInitialReferences()
        {
            if (GameManager_References._player!=null)
            {
                player_Master = GameManager_References._player.GetComponent<Player_Master>();
            }
        }
    } 
}
