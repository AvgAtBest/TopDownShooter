using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
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
            if(TogglePauseEvent !=null)
            {
                TogglePauseEvent();
            }
        }

        public void CallEventMenuToggle()
        {
            if(MenuToggleEvent !=null)
            {
                MenuToggleEvent();
            }
        }

        public void CallEventResumeLevel()
        {
            if(ResumeSceneEvent !=null)
            {
                ResumeSceneEvent();
            }
        }

        public void CallEventGoToMenuScene()
        {
            if(GoToMenuSceneEvent !=null)
            {
                GoToMenuSceneEvent();
            }
        }

        public void CallEventGameOver()
        {
            if(GameOverEvent !=null)
            {
                isGameOver = true;
                GameOverEvent();
            }
        }
    }
}
