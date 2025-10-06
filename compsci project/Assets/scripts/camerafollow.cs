using UnityEngine;

public class camerafollow : MonoBehaviour
{
   [SerializeField]
   private Transform target;
   private Vector3 wantedPosition;
   
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        wantedPosition = target.localPosition;
        wantedPosition.y = transform.position.y;
        transform.localPosition = new Vector3(wantedPosition.x, wantedPosition.y,-4);
    }
}
