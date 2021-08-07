using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class LockPlayerPos : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public GameObject Player;


    //OnPointerDown is also required to receive OnPointerUp callbacks
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down bad");
        
    }

    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {

        GameObject snap = closestTile();
        Vector3 changes = Player.transform.position;

        changes.x = snap.transform.position.x;
        changes.z = snap.transform.position.z;

        Player.transform.position = changes;
        Debug.Log("up bad");
    }
    GameObject closestTile()
    {
        GameObject[] closestTiles;

        closestTiles = GameObject.FindGameObjectsWithTag("SelectedTiles");
        GameObject theClosest = null;
        float distance = Mathf.Infinity;
        Vector3 position = Player.transform.position;

        foreach (GameObject indiviTile in closestTiles)
        {
            Vector3 diff = indiviTile.transform.position - position;
            float currentDist = diff.sqrMagnitude;

            if (currentDist < distance)
            {
                theClosest = indiviTile;
                distance = currentDist;
            }
        }

        return theClosest;
    }
}
