using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private int direction=1;
    void Update()
    {
        rb.linearVelocity = new Vector2(3*direction,0);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("door"))
        {
           direction *= -1;
        }
    }
    
}
