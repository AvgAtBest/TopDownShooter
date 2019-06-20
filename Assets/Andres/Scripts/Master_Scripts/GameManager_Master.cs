using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class GameManager_Master : MonoBehaviour
    {
        public delegate void GameManagementEventHandler();
        public event GameManagementEventHandler MenuToggleEvent;
        public event GameManagementEventHandler RestartLevelEvent;
        public event GameManagementEventHandler GoToMenuSceneEvent;
        public event GameManagementEventHandler GameOverEvent;

        public bool isGameOver;
        public bool isMenuOn;

        public void CallEventMenuToggle()
        {
            if(MenuToggleEvent !=null)
            {

            }
        }

        public void CallEventRestartLevel()
        {
            if(RestartLevelEvent !=null)
            {
                RestartLevelEvent();
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
