using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu,theDarkness,upgradeMenu;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPaused)
            {
                unpause();
            }
            else
            {
                openUpgrades();
            }
        }
    }

    public void pause()
    {
        //pauses game and opens menu
        isPaused=true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        activeMenu = pauseMenu;
    }
    
    public void unpause()
    {
        //unpauses game and closes menu
        isPaused=false;
        Time.timeScale = 1;
        activeMenu.SetActive(false);
        activeMenu = null;
    }

    public void openUpgrades()
    {
        isPaused=true;
        Time.timeScale = 0;
        upgradeMenu.SetActive(true);
        activeMenu = upgradeMenu;
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

    public void equip(int index)
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().getUpgrades(index).setEqiped(true);
    }
    public void equip2(GameObject button)
    {
        Image image = button.GetComponent<Image>();
        image.color=Color.yellowNice;
    }

    public void uneqip(int index)
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().getUpgrades(index).setEqiped(false);
    }
    public void uneqip2(GameObject button)
    {
        Image image = button.GetComponent<Image>();
        image.color=Color.red;
    }

}
