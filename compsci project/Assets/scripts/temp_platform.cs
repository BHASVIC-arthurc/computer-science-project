using System.Collections;
using UnityEngine;

public class TempPlatform : MonoBehaviour
{
    //collider of the platform goes here
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private SpriteRenderer sr;
    private void OnCollisionEnter2D(Collision2D other)
    {
        //when touching another object if its a player run the subroutine
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(break_platform());
        }
    }
    
    private IEnumerator break_platform()
    {
        //waits a second
        yield return new WaitForSeconds(1f);
        //turns off the platform
        box.enabled = false;
        sr.enabled = false;
        //waits another second
        yield return new WaitForSeconds(1f);
        //turns on the platform
        box.enabled = true;
        sr.enabled = true;
    }
}
