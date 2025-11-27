using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]  private float acceleration;
    [SerializeField]  private float jumpForce;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject[] attacks;
    //sword=1 axe=2 spear=3
    private int equipedWeapon=1;
    private float xSpeed,ySpeed,attackTimer;
    private bool grounded = true,canDash=true,dashing;
    
    private GameObject roomController;
    private RoomController roomControllerScript;

    void Start()
    {
        roomController = GameObject.FindWithTag("room_controller");
        roomControllerScript = roomController.GetComponent<RoomController>();
    }
    void Update()
    {
        for (int i = 0; i < attacks.Length; i++)
        {
            //attacks[i].GetComponent<Transform>().position.x = attacks[i].GetComponent<Transform>().position.x * -1;
            //attacks[i].GetComponent<Transform>().rotation.z = attacks[i].GetComponent<Transform>().rotation.z * -1;
            //attacks[i].GetComponent<Transform>().localScale.x = attacks[i].GetComponent<Transform>().localScale.x * -1;
        }
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
            attackTimer+=1*Time.deltaTime;
        }

        //when released do a heavy or light attack
        if (!Input.GetMouseButton(0))
        {
            if (attackTimer > 1)
            {
                attackTimer = 0;
                StartCoroutine(heavyAttack());
            }
            else if (attackTimer > 0)
            {
                attackTimer = 0;
                StartCoroutine(lightAttack());
            }
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
        if (collision.gameObject.CompareTag("jump_boost"))
        {
            jumpForce = 16f;
        }

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
        print("light attack");
        attacks[equipedWeapon*2-2].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        attacks[equipedWeapon*2-2].SetActive(false);
    }
    
    private IEnumerator heavyAttack()
    {
        print("heavy attack");
        attacks[equipedWeapon*2-1].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        attacks[equipedWeapon*2-1].SetActive(false);
    }
}
