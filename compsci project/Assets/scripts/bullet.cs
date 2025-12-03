using System;
using UnityEngine;

public class bullet : MonoBehaviour
{ 
    private GameObject player;
    private Vector3 target;
    [SerializeField] private String[] tags;
    [SerializeField] private Rigidbody2D rb; 
    private float speed=7f;
    private Transform parent;

    void Start()
    {
        parent = transform.parent;
        player = GameObject.FindWithTag("Player");
        target = player.transform.position;
        transform.LookAt(target);
        rb.linearVelocity = transform.forward * speed;
        transform.localRotation = new Quaternion(0,0,0,0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (String tag in tags)
        {
            if (collision.gameObject.CompareTag(tag)&&collision.gameObject!=parent.gameObject)
            {
                Destroy(gameObject);
            }
        }
    }
}
