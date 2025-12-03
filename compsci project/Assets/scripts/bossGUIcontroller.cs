using System.Collections;
using UnityEngine;

public class bossGUIcontroller : MonoBehaviour
{
    private float time;

    private void Start()
    {
        StartCoroutine(ShowCountdown());
    }

    private IEnumerator ShowCountdown()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("starting soon");
        while (time < 3)
        {
            Debug.Log(time);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
