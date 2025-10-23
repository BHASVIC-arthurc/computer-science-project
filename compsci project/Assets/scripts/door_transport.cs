using System;
using UnityEngine;

public class door_transport : MonoBehaviour
{
    private GameObject target_door;
    [SerializeField] private String doorType;

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