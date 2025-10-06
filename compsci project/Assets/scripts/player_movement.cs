using System.Collections;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField]  private float acceleration;
    [SerializeField]  private float jumpForce;
    private bool Grounded = true;
    
    void Start()
    {
         
    }
    void Update()
    {
        Move();
        Jump();
        StartCoroutine(dash());
       
    }
    private void Move()
    {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * acceleration * Time.deltaTime, 0, 0);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && Grounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Grounded = false;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            Grounded = true;
        }
    }

    private IEnumerator dash()
    {
        while(true){
            yield return new WaitForSeconds(1);
            if(Input.GetKeyDown(KeyCode.LeftShift))GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 0), ForceMode2D.Impulse);
        }
    }
}
