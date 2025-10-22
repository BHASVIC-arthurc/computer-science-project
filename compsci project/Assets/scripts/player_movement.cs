using System.Collections;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField]  private float acceleration;
    [SerializeField]  private float jumpForce;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private LayerMask groundLayer;
    private float x_speed;
    private float y_speed;
    private bool Grounded = true;
    private bool canDash=true;
    private bool Dashing;
    
    void Update()
    {
        //first thing the code checks is if the player is on the ground
        isGrounded();
        //this stops the player from changing anything when the player is dashing
        if (!Dashing)
        {
            //runs both movescripts
            Move();
            Jump();
            //checks for if you are able to dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(dash());
            }
        }

    }
    private void Move()
    {
        //sets x_speed to acceleration timsed by the direction your facing
        x_speed = Input.GetAxis("Horizontal") * acceleration;
        //sets the speed of the player to the new x_speed
        rb.linearVelocity = new Vector2(x_speed, rb.linearVelocity.y);
        
        //checks if the player is moving and starts the moving animation
        if (x_speed != 0) animator.SetBool("running",true);
        else animator.SetBool("running", false);
        
        //sets the player to face the way they are moving
        //right
        if (x_speed > 0) sr.flipX = false;
        //left
        else if (x_speed < 0) sr.flipX = true;
        //stays facing the same way with no input

    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && Grounded)
        {
            //sets the players vertical velocity to jumpForce
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            //says they arent on ground
            Grounded = false;
        }
    }

    private void isGrounded()
    {
        //returns true of the sphere made at the bottom of the charecter with a radius of the charecters feet overlaps with an object on the selected layer
            Grounded = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y-1.15f/2),0.6100233f/2,groundLayer);
    }

    private IEnumerator dash()
    {
        //stops player from double dashing
        canDash = false;
        //says they are dashing
        Dashing = true;
        //stops player from falling
        rb.gravityScale = 0f;
        //doubles there horizontal speed
        rb.linearVelocity = new Vector2(x_speed*2, 0);
        //waits for dash to be over
        yield return new WaitForSeconds(0.2f);
        //stops dashing
        Dashing = false;
        //sets gravity back to normal
        rb.gravityScale =2f;
        //waits for dash cooldown
        yield return new WaitForSeconds(1);
        //allows player to dash again
        canDash = true;
    }
}
