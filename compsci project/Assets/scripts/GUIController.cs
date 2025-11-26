using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu,theDarkness;
    private Image lightLevel;
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
        activeMenu.SetActive(false);
        activeMenu = null;
    }

    public void SaveAndQuit()
    {
        print("saving");
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/MainMenu");
        
    }

    public void switchToGUI(GameObject targetGUI)
    {
        activeMenu.SetActive(false);
        targetGUI.SetActive(true);
        activeMenu = targetGUI;
    }

    public void changeBrightness(Slider brightnessSlider)
    {
        lightLevel = theDarkness.GetComponent<Image>();
        float brightness = brightnessSlider.value;
        if (brightness > 49)
        {
            lightLevel.color = new Color(1,1,1,(brightness-50)/50);
        }
        else
        {
            lightLevel.color = new Color(0,0,0,1-(brightness)/50);
        }
    }

    public void changeVolume(Slider volumeSlider)
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

}
