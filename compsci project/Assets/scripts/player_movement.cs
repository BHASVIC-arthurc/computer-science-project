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
    
    void Start()
    {
         
    }
    void Update()
    {
        isGrounded();
        if (!Dashing)
        {
            Move();
            Jump();
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(dash());
            }
        }

    }
    private void Move()
    {
        x_speed = Input.GetAxis("Horizontal") * acceleration;
        rb.linearVelocity = new Vector2(x_speed, rb.linearVelocity.y);
        
        if (x_speed != 0) animator.SetBool("running",true);
        else animator.SetBool("running", false);
        
        if (x_speed > 0) sr.flipX = false;
        else  sr.flipX = true;

    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && Grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            Grounded = false;
        }
    }

    private void isGrounded()
    {
            Grounded = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y-1.15f/2),0.6100233f/2,groundLayer);
    }

    private IEnumerator dash()
    {
        canDash = false;
        Dashing = true;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(x_speed*2, 0);
        yield return new WaitForSeconds(0.2f);
        Dashing = false;
        rb.gravityScale =2f;
        yield return new WaitForSeconds(1);
        canDash = true;
    }
}
