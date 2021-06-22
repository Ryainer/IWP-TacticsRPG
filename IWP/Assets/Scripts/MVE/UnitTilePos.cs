using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTilePos : MonoBehaviour
{
    public Tiles tile { get; protected set; }

    private void Update()
    {
       // Debug.Log(tile.center);
    }

    //this is meant to be a generic function to get where a unit's tile is
    public void tilePlacement(Tiles target)
    {
        if(tile != null && tile.unit == gameObject)
        {
            tile.unit = null;
        }

        tile = target;

        if(target != null)
        {
            target.unit = gameObject;
        }
    }
}
