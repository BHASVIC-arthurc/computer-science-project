using UnityEngine;

public class room_variables : MonoBehaviour
{
    [SerializeField] private int left, right, top, bottom;

    public int GetLeft()
    {
        return left;
    }
    public int GetRight()
    {
        return right;
    }
    public int GetTop()
    {
        return top;
    }
    public int GetBottom()
    {
        return bottom;
    }
}
