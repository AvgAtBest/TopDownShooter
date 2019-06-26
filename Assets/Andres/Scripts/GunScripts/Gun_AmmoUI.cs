using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Updates Ammo Count
namespace TopDownShooter
{

    public class Gun_AmmoUI : MonoBehaviour
    {
        //public Text currentAmmoField;
        //public Text MaxReserveAmmoField;
        public Gun_Master gun_Master;
		public TextMeshProUGUI currentAmmoField;
		public TextMeshProUGUI MaxReserveAmmoField;

		void OnEnable()
        {
            SetInitialReferences();
            gun_Master.EventAmmoChanged += UpdateAmmoUI;
        }

        void OnDisable()
        {
            gun_Master.EventAmmoChanged -= UpdateAmmoUI;
        }

        void SetInitialReferences()
        {
            gun_Master = GetComponent<Gun_Master>();
        }
       public void UpdateAmmoUI(int currentAmmo, int ammoInReserve)
        {
            if(currentAmmoField !=null)
            {
                currentAmmoField.text = currentAmmo.ToString();
            }
            if(MaxReserveAmmoField !=null)
            {
                MaxReserveAmmoField.text = ammoInReserve.ToString();
            }
            
        }
      
    }
}
