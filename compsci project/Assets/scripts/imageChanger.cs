using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class imageChanger : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerScript;
    [SerializeField] private Image image;
    [SerializeField] private Sprite axe,sword,spear;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript.getEqipedWeapon()==1) image.sprite = sword;
        else if(playerScript.getEqipedWeapon()==2)  image.sprite = axe;
        else image.sprite = spear;
    }
}
