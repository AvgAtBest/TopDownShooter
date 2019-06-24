using UnityEngine.SceneManagement;
using UnityEngine;

namespace TopDownShooter
{
    public class GoToMainMenu : MonoBehaviour
    {
        private GameManager_Master gameManager_Master;

        void OnEnable()
        {
            SetInitialReferences();
            gameManager_Master.GoToMenuSceneEvent += GoToMenuScene;
        }
        void OnDisable()
        {
            gameManager_Master.GoToMenuSceneEvent -= GoToMenuScene;
        }
        void SetInitialReferences()
        {
            gameManager_Master = GetComponent<GameManager_Master>();
        }
        void GoToMenuScene()
        {
            SceneManager.LoadScene(0); 
        }
    }
}


