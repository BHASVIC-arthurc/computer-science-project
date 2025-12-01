using UnityEngine;

public class HUDcontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] health;
    private GameObject player;
    private PlayerMovement playerScript;

    void Start()
    {
         player = GameObject.FindWithTag("Player");
         playerScript = player.GetComponent<PlayerMovement>();
    }
    void Update()
    {
        for (int i = 0; i < health.Length; i++)
        {
            print(i+1 + " " + playerScript.getHealth() / 10);
            if (playerScript.getHealth() / 10 < i+1)
            {
                health[i].SetActive(false);
            }
        }
    }
}
