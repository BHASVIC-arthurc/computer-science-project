using UnityEngine;

public class room_variables : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private GameObject[] doorReference;

    public int getWidth() { return width; }
    public int getHeight() { return height; }
    public GameObject GetDoorReference(int index)
    {
        return doorReference[index];
    }
    
}
