using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    GameObject tiles;

   

    public Dictionary<Point, Tiles> tileSet = new Dictionary<Point, Tiles>();

    Point[] dir;

    // Start is called before the first frame update
    void Start()
    {
        dir = new Point[4];
        //when we start up we set up an array of directions based on NSEW
        dir[0] = new Point(0, 1);
        dir[1] = new Point(0, -1);
        dir[2] = new Point(1, 0);
        dir[3] = new Point(-1, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //starts from a specfic tile, then goes through a delegate to see if we can move 
    public List<Tiles> Search(Tiles start, Func<Tiles, Tiles, bool>addTile)
    {
        List<Tiles> retValues = new List<Tiles>();
        retValues.Add(start);

        clearSearch();
        //two queues one for the next tile and the curr tile
        Queue<Tiles> nxtTile = new Queue<Tiles>();
        Queue<Tiles> currTile = new Queue<Tiles>();

        start.distance = 0;
        currTile.Enqueue(start);

        while(currTile.Count > 0)
        {
            Tiles toChck = currTile.Dequeue();

            for(int i = 0; i < dir.Length; ++i)
            {
                //gets next tile in each direction of curr tile
                Tiles next = getTile(toChck.pos + dir[i]);
                //check if next exists
                if (next == null || next.distance <= toChck.distance + 1)
                    continue;

                //chck if the tile can be passed through
                if(addTile(toChck,next))
                {
                    next.distance = toChck.distance + 1;
                    next.previous = toChck;
                    nxtTile.Enqueue(next);
                    retValues.Add(next);
                }

                if(currTile.Count <= 0)
                {
                    swapRef(ref currTile, ref nxtTile);
                }
            }
        }

        return retValues;
    }

    public void Load(LevelData data)
    {
        for(int i = 0; i < data.tiles.Count; ++i)
        {
            GameObject instance = Instantiate(tiles);
            Tiles t = instance.GetComponent<Tiles>();
            t.Load(data.tiles[i]);
            tileSet.Add(t.pos, t);
            Debug.Log("tile pos: " + t.pos + "tiles: " + t);
        }
    }

    void clearSearch() //clear off any of the old tile searches
    {
        foreach(Tiles t in tileSet.Values)
        {
            t.previous = null;
            t.distance = int.MaxValue;
        }
    }

    public Tiles getTile(Point p)
    {
        if(tileSet.ContainsKey(p))
        {
            return tileSet[p];
        }
        else
        {
            return null;
        }
    }

    void swapRef(ref Queue<Tiles>x, ref Queue<Tiles>y)
    {
        Queue<Tiles> temp = x;
        x = y;
        y = temp;
    }
}
