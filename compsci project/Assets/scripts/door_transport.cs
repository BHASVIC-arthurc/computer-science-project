using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DoorTransport : MonoBehaviour
{
    [FormerlySerializedAs("target_door")] [SerializeField] private GameObject targetDoor;
    [SerializeField] private String doorType;
    [FormerlySerializedAs("door_layer")] [SerializeField] LayerMask doorLayer;
    [FormerlySerializedAs("wall_layer")] [SerializeField] LayerMask wallLayer;
    BoxCollider2D box;
    SpriteRenderer sr;


    void Start()
    {   
        box = GetComponent<BoxCollider2D>();
        sr=GetComponent<SpriteRenderer>();
        GameObject roomController = GameObject.FindWithTag("room_controller");
        RoomController roomControllerScript = roomController.GetComponent<RoomController>();
        
        if (Physics2D.OverlapCircle(new Vector2(transform.position.x+1.5f,transform.position.y),0.25f, doorLayer))
        {
            
            roomControllerScript.link_doors(gameObject,Physics2D.OverlapCircle(new Vector2(transform.position.x+1.5f,transform.position.y),0.25f, doorLayer).gameObject);
        }
        else if (Physics2D.OverlapCircle(new Vector2(transform.position.x - 1.5f, transform.position.y), 0.25f, doorLayer))
        {
            roomControllerScript.link_doors(gameObject,Physics2D.OverlapCircle(new Vector2(transform.position.x - 1.5f, transform.position.y), 0.25f, doorLayer).gameObject);
        }
    }

    void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.5f, wallLayer))
        {
            box.isTrigger = false;
            sr.color=Color.white;
        }
    }
    public String GetDoorType()
    {
        return doorType;
    }

    public GameObject GetTargetDoor()
    {
        return targetDoor;
    }

    public void SetTargetDoor(GameObject door)
    {
        targetDoor = door;
    }
    

}