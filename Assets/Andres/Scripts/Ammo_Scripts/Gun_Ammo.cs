using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Reloads Gun
namespace TopDownShooter
{
    public class Gun_Ammo : MonoBehaviour
    {
        public Player_Master player_Master;
        public Gun_Master gun_Master;
        public AmmoBox ammoBox;

        public int clipSize = 10;
        public int maxAmmoReserve = 300;
        public int currentAmmo;
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
            GunController theGun = GetComponent<GunController>();
            currentAmmo = theGun.curAmmo;
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
            currentAmmo--;
            UIAmmoUpdateRequest();
        }

        void TryToReload()
        {
            for(int i=0;i<ammoBox.typesOfAmmunition.Count;i++)
            {
                if(ammoBox.typesOfAmmunition[i].ammoName == ammoName)
                {
                    if(ammoBox.typesOfAmmunition[i].ammoMaxReserve > 0&&
                        currentAmmo != clipSize&&
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
            if(currentAmmo > clipSize)
            {
                currentAmmo = clipSize;
            }
        }

        void UIAmmoUpdateRequest()
        {
            for(int i=0; i<ammoBox.typesOfAmmunition.Count;i++)
            {
                if(ammoBox.typesOfAmmunition[i].ammoName == ammoName)
                {
                    gun_Master.CallEventAmmoChanged(currentAmmo, ammoBox.typesOfAmmunition[i].ammoMaxReserve);
                    break;
                }
            }
        }

        public void OnReloadComplete()
        {
            for(int i=0; i<ammoBox.typesOfAmmunition.Count;i++)
            {
                if(ammoBox.typesOfAmmunition[i].ammoName==ammoName)
                {
                    int ammoTopUP = clipSize = currentAmmo;

                    if(ammoBox.typesOfAmmunition[i].ammoMaxReserve>=ammoTopUP)
                    {
                        currentAmmo += ammoTopUP;
                        ammoBox.typesOfAmmunition[i].ammoMaxReserve = ammoTopUP;
                    }
                        else if(ammoBox.typesOfAmmunition[i].ammoMaxReserve<ammoTopUP&&
                        ammoBox.typesOfAmmunition[i].ammoMaxReserve!=0)
                             {
                                currentAmmo += ammoBox.typesOfAmmunition[i].ammoMaxReserve;
                                ammoBox.typesOfAmmunition[i].ammoMaxReserve = 0;
                             }
                    break;
                }       
            }
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


