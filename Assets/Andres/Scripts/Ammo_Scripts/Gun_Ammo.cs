using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class Gun_Ammo : MonoBehaviour
    {
        private Player_Master player_Master;
        private Gun_Master gun_Master;
        private AmmoBox ammoBox;

        public int clipSize;
        public int CurrentAmmo;
        public string ammoName;
        public float reloadTime;

        void OnEnable()
        {
            SetInitialReferences();
            StartingSanityCheck();

            gun_Master.EventPlayerInput +=DeductAmmo;
            gun_Master.EventRequestReload += TryToReload;

            if (player_Master != null)
            {
                player_Master.EventAmmoChanged += UIAmmoUpdateRequest;
            }

            if(ammoBox !=null)
            {
                StartCoroutine(UpdateAmmoUIWhenEnabling());
            }
        }


        void OnDisable()
        {
            gun_Master.EventPlayerInput -= DeductAmmo;
            gun_Master.EventRequestReload -= TryToReload;

            if(player_Master !=null)
            {
                player_Master.EventAmmoChanged += UIAmmoUpdateRequest;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            SetInitialReferences();
            StartCoroutine(UpdateAmmoUIWhenEnabling());


        }

        void SetInitialReferences()
        {
            gun_Master = GetComponent<Gun_Master>();

            if(GameManager_References._player !=null)
            {
                player_Master = GameManager_References._player.GetComponent<Player_Master>();
                ammoBox = GameManager_References._player.GetComponent<AmmoBox>();

            }
                
        }
        void DeductAmmo()
        {
            CurrentAmmo--;
            UIAmmoUpdateRequest();
        }

        void TryToReload()
        {
            for(int i=0;i<ammoBox.typesOfAmmunition.Count;i++)
            {
                if(ammoBox.typesOfAmmunition[i].ammoName == ammoName)
                {
                    if(ammoBox.typesOfAmmunition[i].ammoMaxReserve > 0&&
                        CurrentAmmo != clipSize&&
                        gun_Master.isReloading)
                    {
                        gun_Master.isReloading = true;
                        gun_Master.isGunLoaded = false;
                    }
                    StartCoroutine(ReloadWithoutAnimation());
                }
            }
        }

        void StartingSanityCheck()
        {
            if(CurrentAmmo > clipSize)
            {
                CurrentAmmo = clipSize;
            }
        }

        void UIAmmoUpdateRequest()
        {
            for(int i=0; i<ammoBox.typesOfAmmunition.Count;i++)
            {
                if(ammoBox.typesOfAmmunition[i].ammoName == ammoName)
                {
                    gun_Master.CallEventAmmoChanged
                }
            }
        }
        public void OnReloadComplete()
        {

        }
        IEnumerator ReloadWithoutAnimation()
        {
            yield return new WaitForSeconds(reloadTime);
            OnReloadComplete();
        }

        IEnumerator UpdateAmmoUIWhenEnabling()
        {
            yield return new WaitForSeconds(0.05f);
            UIAmmoUpdateRequest();
        }
    }
}


