using System;
using UnityEditor.Build;
using UnityEngine;
using Random = UnityEngine.Random;

public class room_controller : MonoBehaviour
{
    [SerializeField] private GameObject[] room_reference;

    private GameObject player;
    private player_movement player_script;
    private room_variables room_script;
    private door_transport door_script;
    private GameObject current_room;
    private GameObject room_prefab;
    private GameObject target_door;
    private GameObject entrance_door;
    [SerializeField] private Vector3 room_entrance_pos;

private void Start()
    {
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<player_movement>();
        new_room(null);
    }
    
    private GameObject new_room(GameObject exit_door)
    {
        Vector3 spawn_location= new Vector3(0,0,0);
        room_prefab= room_reference[Random.Range(0,1)];
        room_script = room_prefab.GetComponent<room_variables>();
        
        if (exit_door != null)
        { 
            float room_width = room_script.getWidth();
            float room_height = room_script.getHeight();
            door_transport exit_door_script=exit_door.GetComponent<door_transport>();
            Vector3 exit_pos = exit_door.transform.position;
            spawn_location = exit_pos;
            switch (exit_door_script.getDoorType())
            {
                case "Top Left":
                    spawn_location = new Vector3(
                        spawn_location.x-room_width/2-0.5f,
                        spawn_location.y+room_height/2,
                        spawn_location.z);
                   
                    break;
                case "Top Right":
                    spawn_location = new Vector3(
                        spawn_location.x+room_width/2+0.5f,
                        spawn_location.y+room_height/2,
                        spawn_location.z);
                   
                    break;
                case "Bottom Left":
                    spawn_location = new Vector3(
                        spawn_location.x-room_width/2-0.5f,
                        spawn_location.y-room_height/2+5,
                        spawn_location.z);
                    
                    break;
                case "Bottom Right":
                    spawn_location = new Vector3(
                        spawn_location.x+room_width/2+0.5f,
                        spawn_location.y-room_height/2+5,
                        spawn_location.z);
                    
                    break;
                        
            }
        }
        current_room =  Instantiate(room_prefab,spawn_location,room_prefab.transform.rotation);
        room_script = current_room.GetComponent<room_variables>();
        if (exit_door != null)
        {
            entrance_door = room_script.GetDoorReference(getOppositeDoor(exit_door));
        }
        else return null;
        player_script.MoveTo(entrance_door.transform.position);
        return entrance_door;
    }
    
    public void exit_room(GameObject exit_door)
    {
        link_doors(exit_door,new_room(exit_door));
        
    }

    public void link_doors(GameObject exit, GameObject entrance)
    {
        door_transport exit_script = exit.GetComponent<door_transport>();
        door_transport entrance_script = entrance.GetComponent<door_transport>();
        exit_script.setTargetDoor(entrance);
        entrance_script.setTargetDoor(exit);
    }

    private int getOppositeDoor(GameObject door)
    {
        switch (door.GetComponent<door_transport>().getDoorType())
        {
            case "Top Left":
                return 3;
            case "Bottom Left":
                return 1;
            case "Top Right":
                return 2;
            case "Bottom Right":
                return 0;
            default:
                return -1;
        }
    }
}
