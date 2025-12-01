using System.Collections;
using UnityEngine;

public class boss : MonoBehaviour
{
    private float xDistance;
    [SerializeField] private GameObject eye1, eye2;
    [SerializeField] private GameObject winScreen;

    private void Start()
    {
        StartCoroutine(move());
    }

    private void Update()
    {
        if (eye1 == null && eye2 == null)
        {
            Destroy(gameObject);
            winScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private IEnumerator move()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            xDistance += Random.Range(-10f, 10f);
            while (xDistance <= -20 || xDistance >= 20)
            {
                xDistance += Random.Range(-10f, 10f);
                if (xDistance <= -30 || xDistance >= 30) xDistance = 0;
            }
            transform.position = new Vector3(xDistance, 12, 0);
        }
    }
}