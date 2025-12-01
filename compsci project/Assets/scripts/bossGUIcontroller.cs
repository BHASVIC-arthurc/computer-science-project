using System.Collections;
using UnityEngine;

public class bossGUIcontroller : MonoBehaviour
{
    private float time;
    void Start()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        bossBattle();
    }

    private void bossBattle()
    {
        print("starting soon");
        while(time<3)
        {
            time += Time.unscaledDeltaTime;
        }
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
