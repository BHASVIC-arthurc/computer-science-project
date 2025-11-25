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
        
        for (int i = 0; i < doorReference.Length; i++)
        {
            doorCode=doorReference[i].GetComponent<DoorTransport>();
            Targets[i]=doorCode.GetTargetDoor();
        }

        var multiples = checkForMultiples();
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
        for (int i = 0; i < doorReference.Length; i++)
        {
            for (int j = i+1; j < doorReference.Length; j++)
            {
                if (Targets[j] == doorReference[i])
                {
                    return (doorReference[i], doorReference[j]);
                }
            }
        }
        return (null, null);
    }
}
