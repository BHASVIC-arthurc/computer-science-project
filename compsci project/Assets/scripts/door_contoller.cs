using System;
using UnityEngine;

public class door_contoller : MonoBehaviour
{
    [SerializeField] private GameObject[] doorReference;
    private DoorTransport doorCode;
    private GameObject[] Targets;

    private void Start()
    {
        Targets = new GameObject[doorReference.Length];
        //on start make a list of each doors target in a room
        for (int i = 0; i < doorReference.Length; i++)
        {
            doorCode=doorReference[i].GetComponent<DoorTransport>();
            Targets[i]=doorCode.GetTargetDoor();
        }
    }
    private void Update()
    {
        var multiples = checkForMultiples();
        //only runs if the two matching doors actually have a target
        if (multiples!=(null,null))
        {
            //wipe door 1
            doorCode=multiples.door1.GetComponent<DoorTransport>();
            doorCode.SetTargetDoor(null);
            doorCode.link_adjacent();
            //wipe door 2
            doorCode=multiples.door2.GetComponent<DoorTransport>();
            doorCode.SetTargetDoor(null);
            doorCode.link_adjacent();
            
            
            
        }
    }

    private (GameObject door1,GameObject door2) checkForMultiples()
    {
        // for each door in the list
        for (int i = 0; i < doorReference.Length; i++)
        {
            //check against each door left in the list
            for (int j = i+1; j < doorReference.Length; j++)
            {
                //if they are the same return the two doors
                if (Targets[j] == Targets[i] &&  Targets[i] != null)
                {
                    return (doorReference[i], doorReference[j]);
                }
            }
        }
        return (null, null);
    }
}
