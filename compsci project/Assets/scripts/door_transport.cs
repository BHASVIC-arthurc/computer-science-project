using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DoorTransport : MonoBehaviour
{
    [SerializeField] private GameObject targetDoor;
    [SerializeField] private String doorType;
    [SerializeField] LayerMask doorLayer;
    [SerializeField] LayerMask wallLayer;
    BoxCollider2D box;
    SpriteRenderer sr;
    private bool canSpawn1;
    private bool canSpawn2;
    private bool canSpawn3;


    void Start()
    {   
        box=GetComponent<BoxCollider2D>();
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
        canSpawn1 = true;
        canSpawn2 = true;
        canSpawn3 = true;
        //for the square room
        switch (doorType)
        {
            case "Top Left":
                if (Physics2D.OverlapBox(new Vector2(transform.position.x - 18, transform.position.y + 18), new Vector2(transform.position.x - 1, transform.position.y + 1), wallLayer)) canSpawn1 = false;
                break;
            case "Top Right":
                if (Physics2D.OverlapBox(new Vector2(transform.position.x + 18, transform.position.y + 18), new Vector2(transform.position.x + 1, transform.position.y + 1), wallLayer)) canSpawn1 = false;
                break;
            case "Bottom Left":
                if (Physics2D.OverlapBox(new Vector2(transform.position.x - 18, transform.position.y - 18), new Vector2(transform.position.x - 1, transform.position.y - 1), wallLayer)) canSpawn1 = false;
                break;
            case "Bottom Right":
                if (Physics2D.OverlapBox(new Vector2(transform.position.x + 18, transform.position.y - 18), new Vector2(transform.position.x + 1, transform.position.y - 1), wallLayer)) canSpawn1 = false;
                break;
         
        }
        
        //for the tall room
        switch (doorType)
        {
            case "Top Left":
                if (Physics2D.OverlapBox(new Vector2(transform.position.x - 18, transform.position.y + 38), new Vector2(transform.position.x - 1, transform.position.y + 1), wallLayer)) canSpawn2 = false;
                break;
            case "Top Right":
                if (Physics2D.OverlapBox(new Vector2(transform.position.x + 18, transform.position.y + 38), new Vector2(transform.position.x + 1, transform.position.y + 1), wallLayer)) canSpawn2 = false;
                break;
            case "Bottom Left":
                if (Physics2D.OverlapBox(new Vector2(transform.position.x - 18, transform.position.y - 38), new Vector2(transform.position.x - 1, transform.position.y - 1), wallLayer)) canSpawn2 = false;
                break;
            case "Bottom Right":
                if (Physics2D.OverlapBox(new Vector2(transform.position.x + 18, transform.position.y - 38), new Vector2(transform.position.x + 1, transform.position.y - 1), wallLayer)) canSpawn2 = false;
                break;
         
        }
        
        //for the long room
        switch (doorType)
        {
            case "Top Left":
                if (Physics2D.OverlapBox(new Vector2(transform.position.x - 38, transform.position.y + 18), new Vector2(transform.position.x - 1, transform.position.y + 1), wallLayer)) canSpawn3 = false;
                break;
            case "Top Right":
                if (Physics2D.OverlapBox(new Vector2(transform.position.x + 38, transform.position.y + 18), new Vector2(transform.position.x + 1, transform.position.y + 1), wallLayer)) canSpawn3 = false;
                break;
            case "Bottom Left":
                if (Physics2D.OverlapBox(new Vector2(transform.position.x - 38, transform.position.y - 18), new Vector2(transform.position.x - 1, transform.position.y - 1), wallLayer)) canSpawn3 = false;
                break;
            case "Bottom Right":
                if (Physics2D.OverlapBox(new Vector2(transform.position.x + 38, transform.position.y - 18), new Vector2(transform.position.x + 1, transform.position.y - 1), wallLayer)) canSpawn3 = false;
                break;
         
        }

        if (!canSpawn1 && !canSpawn2 && !canSpawn3 && targetDoor == null)
        {
            sr.color = Color.white;
            box.isTrigger = false;
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