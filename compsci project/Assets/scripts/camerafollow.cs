using UnityEngine;

public class camerafollow : MonoBehaviour
{
   [SerializeField]
   private Transform target;
   private Vector3 wantedPosition;
   [SerializeField]
   private int maxy, miny;
   
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        wantedPosition = target.localPosition;
        if(wantedPosition.y<miny)
            wantedPosition.y = miny;
        if(wantedPosition.y>maxy)
            wantedPosition.y = maxy;
        transform.localPosition = new Vector3(wantedPosition.x, wantedPosition.y,-4);
    }
}
