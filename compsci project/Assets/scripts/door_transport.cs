using System;
using UnityEngine;

public class door_transport : MonoBehaviour
{
    [SerializeField] private GameObject target_door;
    [SerializeField] private String doorType;
    [SerializeField] LayerMask door_layer;


    void Start()
    {
        
        GameObject room_controller = GameObject.FindWithTag("room_controller");
        room_controller room_controller_script = room_controller.GetComponent<room_controller>();
        
        if (Physics2D.OverlapCircle(new Vector2(transform.position.x+1.5f,transform.position.y),0.25f, door_layer))
        {
            
            room_controller_script.link_doors(gameObject,Physics2D.OverlapCircle(new Vector2(transform.position.x+1.5f,transform.position.y),0.25f, door_layer).gameObject);
        }
        else if (Physics2D.OverlapCircle(new Vector2(transform.position.x - 1.5f, transform.position.y), 0.25f, door_layer))
        {
            room_controller_script.link_doors(gameObject,Physics2D.OverlapCircle(new Vector2(transform.position.x - 1.5f, transform.position.y), 0.25f, door_layer).gameObject);
        }
    }
    public String getDoorType()
    {
        return doorType;
    }

    public GameObject getTargetDoor()
    {
        return target_door;
    }

    public void setTargetDoor(GameObject door)
    {
        target_door = door;
    }
    

}