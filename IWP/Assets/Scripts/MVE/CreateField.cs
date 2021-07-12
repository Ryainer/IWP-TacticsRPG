using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class CreateField : MonoBehaviour
{
    [SerializeField] GameObject tileViewPrefab;
    [SerializeField] GameObject tileSelectionIndicatorPrefab;
    [SerializeField] int width = 10;
    [SerializeField] int depth = 10;
    [SerializeField] int height = 8;

    [SerializeField] Point pos;

    [SerializeField] LevelData levelData;

    //to contain the tiles made to see if the tile exists at a certain coord
    Dictionary<Point, Tiles> tiles = new Dictionary<Point, Tiles>();

    public int number = 1;

    //lazy loading, checks if object exists. If it doesnt then create it
    Transform marker
    {
        get
        {
            if(_marker == null)
            {
                GameObject instance = Instantiate(tileSelectionIndicatorPrefab) as GameObject;
                _marker = instance.transform;
            }
            return _marker;
        }
    }
    Transform _marker;

    //methods to help that would be triggered to help wth the field creation
    public void IncreaseField()
    {
        Rect r = RandomRect();
        IncreaseRect(r);
    }

    public void DecreaseField()
    {
        Rect r = RandomRect();
        DecreaseRect(r);
    }

    //generates the random rect in the region 
    Rect RandomRect()
    {
        int x = Random.Range(0, width);
        int y = Random.Range(0, height);
        int w = Random.Range(1, width - x + 1);
        int h = Random.Range(1, depth - y + 1);
        return new Rect(x, y, h, w);
    }

    //these two loop through the various tile positions, 
    //increasing or decreasing the size of the tile at a time
    void IncreaseRect(Rect rect)
    {
        for(int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        {
            for(int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
            {
                Point p = new Point(x, y);
                increaseSingleTile(p);
            }
        }
    }

    void DecreaseRect(Rect rect)
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        {
            for (int x = (int)rect.xMin; x < rect.xMax; ++x)
            {
                Point p = new Point(x, y);
                decreaseSingleTile(p);
            }
        }
    }

    Tiles Create()//called if the tile does not exist
    {
        GameObject instance = Instantiate(tileViewPrefab) as GameObject;
        instance.transform.parent = transform;
        return instance.GetComponent<Tiles>();
    }

    Tiles GetTiles(Point p)
    {
        if(tiles.ContainsKey(p)) //if tile exists
        {
            return tiles[p];
        }
        else //if tile doesnt exists
        {
            Tiles t = Create();
            t.Load(p, 0);
            tiles.Add(p, t);
            return t;
        }
    }

    void increaseSingleTile(Point p)
    {
        Tiles t = GetTiles(p);
        if (t.height < height)
            t.increase();
    }   
    //Decreasing size of tile. doesnt need to create
    void decreaseSingleTile(Point p)
    {
        if (!tiles.ContainsKey(p))
            return;

        Tiles t = tiles[p];
        t.decrease();

        if(t.height <= 0)
        {
            tiles.Remove(p);
            DestroyImmediate(t.gameObject);
        }
    }

    //these two methods for manual size changes
    public void increaseTilesize()
    {
        increaseSingleTile(pos);
    }

    public void decreaseTilesize()
    {
        decreaseSingleTile(pos);
    }

    public void GenerateBoard()
    {
        clear();
        for(int x =0; x < width; ++x)
        {
            for(int y = 0; y < depth; ++y)
            {
                pos = new Point(x, y);
                increaseSingleTile(pos);
            }
        }
    }

    public void updateIndicator()
    {
        Tiles t;

        if(tiles.ContainsKey(pos))
        {
            t = tiles[pos];
        }
        else
        {
            t = null;
        }

        if(t != null)
        {
            marker.localPosition = t.center;
        }
        else
        {
            marker.localPosition = new Vector3(pos.x, 0, pos.y);
        }
    }
    //the reset
    public void clear()
    {
        for(int i = transform.childCount - 1; i >= 0; --i)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        tiles.Clear();
    }
#if UNITY_EDITOR
    //to save the level data
    public void save()
    {
        string filePath = Application.dataPath + "/Resources/Levels";
        if(!Directory.Exists(filePath))
        {
            CreateSaveDirectory();
        }

        LevelData board = ScriptableObject.CreateInstance<LevelData>();
        board.tiles = new List<Vector3>(tiles.Count);
        foreach(Tiles t in tiles.Values)
        {
            board.tiles.Add(new Vector3(t.pos.x, t.height, t.pos.y));
        }
        
        string currentfilename = levelData.ToString();
        string nameOfFile = "Level " + number;
        Debug.Log(levelData.ToString());
        if(levelData.ToString() == nameOfFile + " (LevelData)")
        {
            int newnum = ++number;
            nameOfFile = "Level " + newnum;
            number = newnum;
            Debug.Log(nameOfFile);
        }

        string fileName = string.Format("Assets/Resources/Levels/{1}.asset", filePath, nameOfFile);
        AssetDatabase.CreateAsset(board, fileName);
        

    }

    void CreateSaveDirectory()
    {
        string filePath = Application.dataPath + "/Resources";

        if(!Directory.Exists(filePath))
        {
            AssetDatabase.CreateFolder("Assets", "Resources");
        }
        filePath += "/Levels";
        if(!Directory.Exists(filePath))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        }

        AssetDatabase.Refresh();
    }
#endif
    //loads an existing level
    public void Load()
    {
        clear();
        if (levelData == null) //if the level doesnt exist
        {
            Debug.Log("Level doesnt exist!");
            return;
        }

        foreach (Vector3 v in levelData.tiles)//otherwise cycle through the list to get tiles out
        {
            Tiles t = Create();
            t.Load(v);
            tiles.Add(t.pos, t);
        }
    }

    
}
