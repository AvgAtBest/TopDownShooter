using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Updates Ammo Count
namespace TopDownShooter
{

    public class Gun_AmmoUI : MonoBehaviour
    {
        public Text currentAmmoField;
        public Text MaxReserveAmmoField;
        public Gun_Master gun_Master;
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
       public void UpdateAmmoUI(int currentAmmo, int carriedAmmo)
        {
            if(currentAmmoField !=null)
            {
                currentAmmoField.text = currentAmmo.ToString();
            }
            if(MaxReserveAmmoField !=null)
            {
                MaxReserveAmmoField.text = carriedAmmo.ToString();
            }
            
        }
      
    }
}
