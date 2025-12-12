using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DoorTransport : MonoBehaviour
{
    [SerializeField] private GameObject targetDoor;
    [SerializeField] private String doorType;
    [SerializeField] private LayerMask doorLayer;
    [SerializeField] private LayerMask wallLayer;
    BoxCollider2D box;
    SpriteRenderer sr;
    private bool canSpawn1;
    private bool canSpawn2;
    private bool canSpawn3;


    void Start()
    {   
        box=GetComponent<BoxCollider2D>();
        sr=GetComponent<SpriteRenderer>();
        link_adjacent();
    }

    void FixedUpdate()
    {
        canSpawn();
        //every set amount of frames check if a room can be spawned from this door
        if (!canSpawn1 && !canSpawn2 && !canSpawn3 && targetDoor == null || Physics2D.OverlapCircle(transform.position, .5f, wallLayer))
        {
            //if not close door
            sr.color = Color.white;
            box.isTrigger = false;
        }
        else
        {
            //if can open door
            sr.color = Color.black;
            box.isTrigger = true;
        }
        
        
    }
    private void canSpawn()
    {
        //checks for each door for each room if there is a wall within the area the room would be spawned
        canSpawn1 = true;
        canSpawn2 = true;
        canSpawn3 = true;
        //for the square room
        switch (doorType)
        {
            case "Top Left":
                if (Physics2D.OverlapCircle(new Vector2(transform.position.x - 10, transform.position.y + 10), 5, wallLayer)) canSpawn1 = false;
                break;
            case "Top Right":
                if (Physics2D.OverlapCircle(new Vector2(transform.position.x + 10, transform.position.y + 10), 5, wallLayer)) canSpawn1 = false;
                break;
            case "Bottom Left":
                if (Physics2D.OverlapCircle(new Vector2(transform.position.x - 10, transform.position.y - 10), 5, wallLayer)) canSpawn1 = false;
                break;
            case "Bottom Right":
                if (Physics2D.OverlapCircle(new Vector2(transform.position.x + 10, transform.position.y - 10), 5, wallLayer)) canSpawn1 = false;
                break;
        }

        //for the tall room
        switch (doorType)
        {
            case "Top Left":
                if (Physics2D.OverlapCircle(new Vector2(transform.position.x - 10, transform.position.y + 20), 5, wallLayer)) canSpawn2 = false;
                break;
            case "Top Right":
                if (Physics2D.OverlapCircle(new Vector2(transform.position.x + 10, transform.position.y + 20), 5, wallLayer)) canSpawn2 = false;
                break;
            case "Bottom Left":
                if (Physics2D.OverlapCircle(new Vector2(transform.position.x - 10, transform.position.y - 20), 5, wallLayer)) canSpawn2 = false;
                break;
            case "Bottom Right":
                if (Physics2D.OverlapCircle(new Vector2(transform.position.x + 10, transform.position.y - 20), 5, wallLayer)) canSpawn2 = false;
                break;
        }

        //for the long room
        switch (doorType)
        {
            case "Top Left":
                if (Physics2D.OverlapCircle(new Vector2(transform.position.x - 20, transform.position.y + 10), 5, wallLayer)) canSpawn3 = false;
                break;
            case "Top Right":
                if (Physics2D.OverlapCircle(new Vector2(transform.position.x + 20, transform.position.y + 10), 5, wallLayer)) canSpawn3 = false;
                break;
            case "Bottom Left":
                if (Physics2D.OverlapCircle(new Vector2(transform.position.x - 20, transform.position.y - 10), 5, wallLayer)) canSpawn3 = false;
                break;
            case "Bottom Right":
                if (Physics2D.OverlapCircle(new Vector2(transform.position.x + 20, transform.position.y - 10), 5, wallLayer)) canSpawn3 = false;
                break;
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

    public void link_adjacent()
    {
        //gets the door contoller script
        GameObject roomController = GameObject.FindWithTag("room_controller");
        RoomController roomControllerScript = roomController.GetComponent<RoomController>();
        //links door if its on the right of this door
        if (Physics2D.OverlapCircle(new Vector2(transform.position.x+1.5f,transform.position.y),0.25f, doorLayer))
        {
            
            roomControllerScript.link_doors(gameObject,Physics2D.OverlapCircle(new Vector2(transform.position.x+1.5f,transform.position.y),0.25f, doorLayer).gameObject);
        }
        //links to door on the left of this door
        else if (Physics2D.OverlapCircle(new Vector2(transform.position.x - 1.5f, transform.position.y), 0.25f, doorLayer))
        {
            roomControllerScript.link_doors(gameObject,Physics2D.OverlapCircle(new Vector2(transform.position.x - 1.5f, transform.position.y), 0.25f, doorLayer).gameObject);
        }
    }
    

}