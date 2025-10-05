using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField]  private float acceleration;
    [SerializeField]  private float jumpForce;
    
    void Start()
    {
        
    }
    void Update()
    {
        Move();
        Jump();
    }
    private void Move()
    {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * acceleration * Time.deltaTime, 0, 0);
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}
