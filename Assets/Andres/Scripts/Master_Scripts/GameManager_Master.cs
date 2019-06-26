using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager_Master : MonoBehaviour
{
    public delegate void GameManagementEventHandler();
    public event GameManagementEventHandler MenuToggleEvent;
    public event GameManagementEventHandler TogglePauseEvent;
    public event GameManagementEventHandler ResumeSceneEvent;
    public event GameManagementEventHandler GoToMenuSceneEvent;
    public event GameManagementEventHandler GameOverEvent;

    public bool isGameOver;
    public bool isMenuOn;

    public void TogglePause()
    {
        if (TogglePauseEvent != null)
        {
            TogglePauseEvent();
        }
    }

    public void CallEventMenuToggle()
    {
        if (MenuToggleEvent != null)
        {
            MenuToggleEvent();
        }
    }

    public void CallEventResumeLevel()
    {
        if (ResumeSceneEvent != null)
        {
            ResumeSceneEvent();
        }
    }

    public void CallEventGoToMenuScene()
    {
        if (GoToMenuSceneEvent != null)
        {

            GoToMenuSceneEvent();

        }
		GameObject player = GameObject.Find("Player");
		PlayerHealth pHealth = player.GetComponent<PlayerHealth>();
		Player_Movement pMovement = player.GetComponent<Player_Movement>();
		GunController gGun = player.GetComponentInChildren<GunController>();
		gGun.ammoInClip = gGun.maxClipSize;
		gGun.ammoInReserve = gGun.ammoMaxReserve;
		pHealth.curHealth = pHealth.maxHealth;
		pHealth.isDead = false;
		Player_Interaction pinteraction = player.GetComponent<Player_Interaction>();
		pinteraction.cashAmount = 0;
		pinteraction.floorsCleared = 0;
		pinteraction.hasObtainedKey = false;
	}

    public void CallEventGameOver()
    {
        if (GameOverEvent != null)
        {
            isGameOver = true;
            GameOverEvent();
        }
    }
}


