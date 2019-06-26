using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenuUI;

    [SerializeField] public bool isPaused;
    public GameObject playerUIPanel;
    
    // Update is called once per frame
    private void Start()
    {
         
        //playerUIPanel = GameObject.Find("PlayerUIPanel").GetComponent<GameObject>();
        pauseMenuUI.SetActive(false);
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

        }

        if (isPaused)
        {
            ActivateMenu();
        }

        else if(!isPaused)
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {

        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        playerUIPanel.SetActive(false);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        playerUIPanel.SetActive(true);
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }
}
