using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Player;
    
    [Header("Movement")]
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float jumpForce = 11.1f;
    [SerializeField] private Rigidbody2D rb;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer,enemyLayer,portalLayer;

    [Header("Attacks")]
    [SerializeField] private GameObject[] attacks;
    [SerializeField] private float[] attackTimes;
    [SerializeField] private float[] attackCooldowns;
    [SerializeField] private GameObject bolt;
    private int[] attackDamage = { 2, 6, 4, 9, 3, 6 };

    [Header("Player Stats")]
    [SerializeField] private int health = 100;

    [Header("Upgrades")]
    [SerializeField] private List<upgrades> upgrades = new List<upgrades>();
    
    [Header("scenes")]
    [SerializeField]
    Scene combatScene,mainMenuScene,nonCombatScene;
    

    private bool canLightAttack = true;
    private bool canHeavyAttack = true;
    private bool grounded = true;
    private bool isHit;
    private bool canDash = true;
    private bool dashing;
    private bool doubleDamage;
    private bool canCrit;
    private bool canStun;
    private bool canShoot;
    private bool sword1;
    private bool sword2;
    
    private float attackTimer;
    private float xSpeed;

    private int equippedWeapon = 1; // 1=sword,2=axe,3=spear

    private GameObject roomController;
    private RoomController roomControllerScript;

    private String currentAttack;
    
    
    
    
    private void Awake()
    {
        if (Player == null)
        {
            Player = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        roomController = GameObject.FindWithTag("room_controller");
        roomControllerScript = roomController.GetComponent<RoomController>();

        upgrades.Add(new upgrades("big baller", false, 1, 0.2f));
        upgrades.Add(new upgrades("small baller", false, -1, -0.1f));
        upgrades.Add(new upgrades("berserker", false, 2, 2));
        upgrades.Add(new upgrades("earthbreaker axe", false, 0, 0));
        upgrades.Add(new upgrades("thunder spear", false, 0, 0));
        upgrades.Add(new upgrades("reinforced spear", false, 0, 0));
        
    }

    void Update()
    {

        if (health <= 0)
        {
            SceneManager.LoadScene("Scenes/nonCombat");
            transform.position = new Vector3(0, 0, 0);
            sr.enabled = true;
            health = 100;
        }
        upgradeHandler();
        //the first thing the code check is if the player is on the ground
        IsGrounded();
        //this stops the player from changing anything when the player is dashing
        if (!dashing)
        {
            //runs both move scripts
            Move();
            Jump();
            //checks for if you are able to dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }
        }
        //starts or continous attack timer
        if (Input.GetMouseButton(0))
        {
            attackTimer += 1 * Time.deltaTime;
        }

        //when released do a heavy or light attack
        if (!Input.GetMouseButton(0))
        {
            if (attackTimer > 0.3f)
            {
                attackTimer = 0;
                if(canHeavyAttack) StartCoroutine(heavyAttack());
            }
            else if (attackTimer > 0)
            {
                attackTimer = 0;
                if(canLightAttack) StartCoroutine(lightAttack());
            }
        }

        if (xSpeed > 0)
        {
            for (int i=0;i<attacks.Length;i++)
            {
                Transform attackLocation = attacks[i].transform;
                if(attackLocation.localScale.x<0) attackLocation.localScale = new Vector3(attackLocation.localScale.x*-1,attackLocation.localScale.y,attackLocation.localScale.z);
            }
        }

        else if (xSpeed < 0)
        {
            for (int i=0;i<attacks.Length;i++)
            {
                Transform attackLocation = attacks[i].transform;
                if(attackLocation.localScale.x>0) attackLocation.localScale = new Vector3(attackLocation.localScale.x*-1,attackLocation.localScale.y,attackLocation.localScale.z);
            }
        }

        if (Physics2D.OverlapCircle(transform.position, 0.6100233f/2, enemyLayer)&&!isHit)
        {
            StartCoroutine(GetHit());
        }

        if (Physics2D.OverlapCircle(transform.position, 0.6100233f / 2, portalLayer)&&Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Scenes/CombatWorld");
        }
}
    private void Move()
    {
        //sets x_speed to acceleration timsed by the direction you're facing
        xSpeed = Input.GetAxis("Horizontal") * acceleration;
        //sets the speed of the player to the new x_speed
        rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);
        
        //checks if the player is moving and starts the moving animation
        if (xSpeed != 0) animator.SetBool("running",true);
        else animator.SetBool("running", false);
        
        //sets the player to face the way they are moving
        //right
        if (xSpeed > 0) sr.flipX = false;
        //left
        else if (xSpeed < 0) sr.flipX = true;
        //stays facing the same way with no input

    }

    private void Jump()
    {
        //checks for button down and player is on the ground
        if (Input.GetButtonDown("Jump") && grounded)
        {
            //sets the players vertical velocity to jumpForce
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            //says they aren't on ground
            grounded = false;
        }
    }

    private void IsGrounded()
    {
        //returns true of the sphere made at the bottom of the character with a radius of the characters feet overlaps with an object on the selected layer
            grounded = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y-1.15f/2),0.6100233f/2,groundLayer);
    }

    private IEnumerator Dash()
    {
        //stops player from double dashing
        canDash = false;
        //says they are dashing
        dashing = true;
        //stops player from falling
        rb.gravityScale = 0f;
        //doubles there horizontal speed
        rb.linearVelocity = new Vector2(xSpeed*2, 0);
        //waits for dash to be over
        yield return new WaitForSeconds(0.2f);
        //stops dashing
        dashing = false;
        //sets gravity back to normal
        rb.gravityScale =2f;
        //waits for dash cooldown
        yield return new WaitForSeconds(1);
        //allows player to dash again
        canDash = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //when you touch a jump boost object double jump force
        if (collision.gameObject.CompareTag("jump_boost"))
        {
            jumpForce = 16f;
        }

        //when you touch a door object gets the target door and teleports the player to it
        if (collision.gameObject.CompareTag("door"))
        {
            DoorTransport exitScript = collision.gameObject.GetComponent<DoorTransport>();
            if(exitScript.GetTargetDoor() == null)
            {roomControllerScript.exit_room(collision.gameObject);}
            else 
                MoveTo(exitScript.GetTargetDoor().transform.position);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //when you leave the jump boost object sets jump power back to normal
        if (collision.gameObject.CompareTag("jump_boost"))
        {
            jumpForce = 11.1f;
        }
    }

    public void MoveTo(Vector3 target)
    {
        target=new Vector3(target.x,target.y,-1);
        transform.position = target;

    }

    private IEnumerator lightAttack()
    {
        canLightAttack = false;
        currentAttack="light";
        attacks[equippedWeapon*2-2].SetActive(true);
        yield return new WaitForSeconds(attackTimes[equippedWeapon*2-2]);
        attacks[equippedWeapon*2-2].SetActive(false);
        yield return new WaitForSeconds(attackCooldowns[equippedWeapon*2-2]);
        if (equippedWeapon == 3&&canShoot)
        {
            Instantiate(bolt, transform.position, transform.localRotation);
        }
        canLightAttack = true;
    }
    
    private IEnumerator heavyAttack()
    {
        canHeavyAttack=false; 
        currentAttack="heavy";
        attacks[equippedWeapon*2-1].SetActive(true);
        yield return new WaitForSeconds(attackTimes[equippedWeapon*2-1]);
        attacks[equippedWeapon*2-1].SetActive(false);
        yield return new WaitForSeconds(attackCooldowns[equippedWeapon*2-1]);
        canHeavyAttack = true;
    }

    private IEnumerator GetHit()
    {

            isHit = true;
            if(doubleDamage) health-=20;
            else health-=10;
            for (int i = 0; i <= 5; i++)
            {
                sr.enabled = false;
                yield return new WaitForSeconds(0.1f);
                sr.enabled = true;
                yield return new WaitForSeconds(0.1f);
            }
            isHit=false;
    }

    public upgrades getUpgrades(int index)
    {
        return upgrades[index];
    }


    private void upgradeHandler()
    {
        if (upgrades[0].getEqipped())
        {
            if(!sword1)
            {
                sword1 = true;
                for (int i = 0; i < 6; i++)
                {
                    attackDamage[i] += 1;
                    attackCooldowns[i] += 0.2f;
                }
            }
        }
        
        if (upgrades[1].getEqipped())
        {
            if (!sword2)
            {
                sword2 = true;
                for (int i = 0; i < 6; i++)
                {
                    attackDamage[i] -= 1;
                    attackCooldowns[i] -= 0.1f;
                }
            }
        }
        
        if (upgrades[2].getEqipped())
        {
            doubleDamage = true;
        }
        
        if (upgrades[3].getEqipped())
        {
            canStun = true;
        }
        
        if (upgrades[4].getEqipped())
        {
            canShoot =  true;
        }
        
        if (upgrades[5].getEqipped())
        {
            canCrit = true;
        }
        
        if (!upgrades[0].getEqipped())
        {
            if(sword1)
            {
                sword1 = false;
                for (int i = 0; i < 6; i++)
                {
                    attackDamage[i] -= 1;
                    attackCooldowns[i] -= 0.2f;
                }
            }
        }
        
        if (!upgrades[1].getEqipped())
        {
            if (sword2)
            {
                sword2 = false;
                for (int i = 0; i < 6; i++)
                {
                    attackDamage[i] += 1;
                    attackCooldowns[i] += 0.1f;
                }
            }
        }
        
        if (!upgrades[2].getEqipped())
        {
            doubleDamage = false;
        }
        
        if (!upgrades[3].getEqipped())
        {
            canStun = false;
        }
        
        if (!upgrades[4].getEqipped())
        {
            canShoot =  false;
        }
        
        if (!upgrades[5].getEqipped())
        {
            canCrit = false;
        }
        
        
    }
    public bool getCanCrit()
    {
        return canCrit;
    }
    public bool getDoubleDamage()
    {
        return doubleDamage;
    }
    public bool getCanStun()
    {
        return canStun;
    }

    public int getAttackDamage(int index)
    {
        return attackDamage[index];
    }
    
    public int getEqipedWeapon()
    {
        return equippedWeapon;
    }

    public String getCurrentAttack()
    {
        return currentAttack;
    }
}
