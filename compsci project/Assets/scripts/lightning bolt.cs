using System;
using UnityEngine;

public class lightningbolt : MonoBehaviour
{
    [SerializeField] private String[] tags;
    [SerializeField] private Rigidbody2D rb;
    private float speed = 7f;

    void Update()
    {
        rb.linearVelocity = transform.right * speed;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (String tag in tags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                Destroy(gameObject);
            }
        }
    }
}
