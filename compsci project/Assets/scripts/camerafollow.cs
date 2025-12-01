
using UnityEngine;
public class Camerafollow : MonoBehaviour
{
   [SerializeField]
   //this is the object the camera will follow
   private Vector3 wantedPosition;
   [SerializeField]
   //this is the furthest it will go in all directions
   private int maxx,minx,maxy, miny;
   private Transform target;
   
    void Update()
    {
        if (target == null)
        {
            target=GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
        wantedPosition = target.localPosition;
        //sets the location back to the max or min if they go over
        if(wantedPosition.x<minx) wantedPosition.x = minx;
        if(wantedPosition.x>maxx) wantedPosition.x = maxx;
        if(wantedPosition.y<miny) wantedPosition.y = miny;
        if(wantedPosition.y>maxy) wantedPosition.y = maxy;
        transform.localPosition = new Vector3(wantedPosition.x, wantedPosition.y,-4);
    }
}
