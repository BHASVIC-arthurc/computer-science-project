using UnityEngine;

public class temp_platform : MonoBehaviour
{
    [SerializeField] private LayerMask player;
    void Update()
    {
        print(Physics2D.OverlapCapsule(new Vector2(transform.position.x,transform.position.y+0.5f),new Vector2(1,0.5f),CapsuleDirection2D.Horizontal,player));
        if(Physics2D.OverlapCapsule(new Vector2(transform.position.x,transform.position.y+0.5f),new Vector2(1,0.5f),CapsuleDirection2D.Horizontal,player)!=null)
            Destroy(this.gameObject);
    }
}
