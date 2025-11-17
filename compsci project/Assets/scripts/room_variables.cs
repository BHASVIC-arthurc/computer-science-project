using UnityEngine;

public class RoomVariables : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private GameObject[] doorReference;

    public int GetWidth() { return width; }
    public int GetHeight() { return height; }
    public GameObject GetDoorReference(int index)
    {
        return doorReference[index];
    }
    
}
