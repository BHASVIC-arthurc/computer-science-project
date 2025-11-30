using UnityEngine;

public class RoomVariables : MonoBehaviour
{
    //stores and has getters for the rooms width and height for room controller
    [SerializeField] private int width, height;
    [SerializeField] private GameObject[] doorReference;

    public int GetWidth() { return width; }
    public int GetHeight() { return height; }
    public GameObject GetDoorReference(int index)
    {
        return doorReference[index];
    }
    
}
