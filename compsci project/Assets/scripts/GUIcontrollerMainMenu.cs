using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIcontrollerMainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    GameObject activeMenu;

    private void Awake()
    {
        activeMenu = mainMenu;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)&& activeMenu == mainMenu)
        {
            Quit();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void switchToGUI(GameObject targetGUI)
    {
        activeMenu.SetActive(false);
        targetGUI.SetActive(true);
        activeMenu = targetGUI;
    }

    public void startGame()
    {
        SceneManager.LoadScene("CombatWorld");
    }

}
