using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class Item_MasterV2 : MonoBehaviour
    {
        private Player_Master player_Master;

        public delegate void PickupEventHandler(Transform item);
        public event PickupEventHandler EventPickUpAction;

        void OnEnable()
        {

        }

        void OnDisable()
        {

        }

        void Start()
        {
            
        }

        void SetInitialReferences()
        {
            //if (GameManager_References)
        }
    } 
}
