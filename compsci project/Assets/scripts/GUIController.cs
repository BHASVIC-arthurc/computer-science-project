using System;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private GameObject activeMenu;
    private bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                unpause();
            }
            else if (!isPaused)
            {
                pause();
            }
        }
    }

    public void pause()
    {
        //pauses game
        isPaused=true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        activeMenu = pauseMenu;
    }
    
    public void unpause()
    {
        //unpauses game
        isPaused=false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        activeMenu = null;
    }

    public void SaveAndQuit()
    {
        print("saving");
        Application.Quit();
    }

    public void switchToGUI(GameObject targetGUI)
    {
        activeMenu.SetActive(false);
        targetGUI.SetActive(true);
        activeMenu = targetGUI;
    }
}
