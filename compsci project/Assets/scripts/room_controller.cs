using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RoomController : MonoBehaviour
{
    [SerializeField] private GameObject[] roomReference;

    private GameObject player;
    private PlayerMovement playerScript;
    private RoomVariables roomScript;
    private DoorTransport doorScript;
    private GameObject currentRoom;
    private GameObject roomPrefab;
    private GameObject targetDoor;
    private GameObject entranceDoor;
    private bool canPlace;
    private int randomRoom;
    private Vector3 roomEntrancePos;
    private int count;
    private int roomCount=0;
    [SerializeField] private LayerMask roomLayer;

private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement>();
        new_room(null);
    }

    private void Update()
    {
        if (roomCount >= 15)
        {
            print("boss fight time");
            SceneManager.LoadScene("Scenes/boss Fight");
            playerScript.MoveTo(new Vector3(0, 0, 0));
        }
    }
    private GameObject new_room(GameObject exitDoor)
    {
        Vector3 spawnLocation;
        count = 0;
        do
        {
            spawnLocation = new Vector3(0, 0, 0);
            randomRoom = Random.Range(0, 3);
            roomPrefab = roomReference[randomRoom];
            roomScript = roomPrefab.GetComponent<RoomVariables>();
            if (exitDoor != null)
            {
                float roomWidth = roomScript.GetWidth();
                float roomHeight = roomScript.GetHeight();
                DoorTransport exitDoorScript = exitDoor.GetComponent<DoorTransport>();
                Vector3 exitPos = exitDoor.transform.position;
                spawnLocation = exitPos;
                switch (exitDoorScript.GetDoorType())
                {
                    case "Top Left":
                        spawnLocation = new Vector3(
                            spawnLocation.x - (roomWidth + 1) / 2,
                            spawnLocation.y + (roomHeight-4) / 2,
                            spawnLocation.z);

                        break;
                    case "Top Right":
                        spawnLocation = new Vector3(
                            spawnLocation.x + (roomWidth + 1) / 2,
                            spawnLocation.y + (roomHeight - 4) / 2,
                            spawnLocation.z);

                        break;
                    case "Bottom Left":
                        spawnLocation = new Vector3(
                            spawnLocation.x - (roomWidth + 1) / 2,
                            spawnLocation.y - (roomHeight - 4) / 2,
                            spawnLocation.z);

                        break;
                    case "Bottom Right":
                        spawnLocation = new Vector3(
                            spawnLocation.x + (roomWidth + 1) / 2,
                            spawnLocation.y - (roomHeight - 4) / 2,
                            spawnLocation.z);

                        break;

                }
                canPlace = !Physics2D.OverlapArea(
                    new Vector2(spawnLocation.x + roomWidth / 2-3, spawnLocation.y + roomHeight / 2-3),
                    new Vector2(spawnLocation.x - roomWidth / 2+3, spawnLocation.y - roomHeight / 2+3),
                    roomLayer);
            }
            count++;
        } while (!canPlace && count!=20 && exitDoor!=null);

        if (count!=20){
            currentRoom = Instantiate(roomPrefab, spawnLocation, roomPrefab.transform.rotation);
            roomCount++;
            roomScript = currentRoom.GetComponent<RoomVariables>();
            if (exitDoor != null)
            {
                entranceDoor = roomScript.GetDoorReference(GetOppositeDoor(exitDoor));
            }
            else return null;

            playerScript.MoveTo(entranceDoor.transform.position);
            return entranceDoor;
        }
        return null;
    }

    public void exit_room(GameObject exitDoor)
    {
        link_doors(exitDoor,new_room(exitDoor));
        
    }

    public void link_doors(GameObject exit, GameObject entrance)
    {
        DoorTransport exitScript = exit.GetComponent<DoorTransport>();
        DoorTransport entranceScript = entrance.GetComponent<DoorTransport>();
        exitScript.SetTargetDoor(entrance);
        entranceScript.SetTargetDoor(exit);
    }

    private int GetOppositeDoor(GameObject door)
    {
        switch (door.GetComponent<DoorTransport>().GetDoorType())
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
