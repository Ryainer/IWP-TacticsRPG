using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public const float stepHeight = 0.25f;
    public Point pos;
    public int height;
    public GameObject unit;

    [HideInInspector]
    public Tiles previous;
    [HideInInspector]
    public int distance;

    public Vector3 center { get { return new Vector3(pos.x, height * stepHeight, pos.y); } }
    

    //ensures that whenever there are changes to the position of the tile within the code 
    //it is reflected visually as well
    void Match()
    {
        transform.localPosition = new Vector3(pos.x, height * stepHeight /2f, pos.y);
        transform.localScale = new Vector3(1, height * stepHeight, 1);
    }

    //these two will adjust size of tiles
    public void increase()
    {
        height++;
        Match();
    }

    public void decrease()
    {
        height--;
        Match();
    }
    //to ensure the data remains after the process ends to be used as a vector3
    public void Load(Point p, int h)
    {
        pos = p;
        height = h;
        Match();
    }

    public void Load(Vector3 v)
    {
        Load(new Point((int)v.x, (int)v.z), (int)v.y);
    }
}
