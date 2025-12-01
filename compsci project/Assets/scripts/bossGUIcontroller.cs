using System.Collections;
using UnityEngine;

public class bossGUIcontroller : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(bossBattle());
    }

    private IEnumerator bossBattle()
    {
        print("starting soon");
        yield return new WaitForSeconds(3);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
