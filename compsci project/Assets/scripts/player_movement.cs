using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]  private float acceleration;
    [SerializeField]  private float jumpForce;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private LayerMask groundLayer;
    private float xSpeed;
    private float ySpeed;
    private bool grounded = true;
    private bool canDash=true;
    private bool dashing;
    
    private GameObject roomController;
    private RoomController roomControllerScript;

    void Start()
    {
        roomController = GameObject.FindWithTag("room_controller");
        roomControllerScript = roomController.GetComponent<RoomController>();
    }
    void Update()
    {
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
}
