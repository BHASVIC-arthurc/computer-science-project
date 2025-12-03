using System.Collections;
using UnityEngine;

public class shooting_enemy : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] private int health = 10;
    [SerializeField] private LayerMask weaponLayer;
    private bool isHit;
    private GameObject player;
    private PlayerMovement playerScript;
    private bool canShoot=true;
    
    
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement>();
        StartCoroutine(shoot());
        
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 1, weaponLayer)&&!isHit)
        {
            StartCoroutine(GetHit());
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator shoot()
    {
        while(true)
        { 
            if(canShoot) Instantiate(projectile, new Vector3(transform.position.x,transform.position.y,-1), transform.rotation,gameObject.transform);
        yield return new WaitForSeconds(Random.Range(1.5f,3f));
        }
    }
    
    private IEnumerator GetHit()
    {

        isHit = true;
        int startDamage=0;
        int weapon = playerScript.getEqipedWeapon();
        if (playerScript.getCurrentAttack() == "light")
        {
            startDamage = playerScript.getAttackDamage(weapon * 2 - 2);
        }
        else if (playerScript.getCurrentAttack() == "heavy")
        {
            startDamage = playerScript.getAttackDamage(weapon * 2 - 1);
        }
        int finalDamage = startDamage;

        bool doubleDamage = playerScript.getDoubleDamage() && weapon == 2;
        bool gotCrit = playerScript.getCanCrit() && weapon == 3 && Random.Range(0, 10) == 10;
        bool canStun = playerScript.getCanStun() && weapon == 2;

        if (doubleDamage || gotCrit)
        {
            finalDamage *= 2;
        }

        
        health -= finalDamage;
        if (canStun)
        {
            StartCoroutine(stun());
        }
        yield return new WaitForSeconds(0.2f);
        isHit = false;
    }

    private IEnumerator stun()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
    
}
