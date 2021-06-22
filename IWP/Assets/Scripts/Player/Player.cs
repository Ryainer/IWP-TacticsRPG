using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public int health = 100;
    [HideInInspector]
    public int MP = 15;
    [HideInInspector]
    public int timer = 0;

    public List<GameObject> crewmembers = new List<GameObject>();

    public GameObject warrior;
    public GameObject archer;
    public GameObject movements;
    public GameObject joystick;
    public GameObject menu;
    public LayerMask ignore;
    private GameObject clone;
    GameObject[] Tiles;
    void Start()
    {
        Tiles = GameObject.FindGameObjectsWithTag("Tiles");
        createPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createPlayers()
    {
       
        Debug.Log("creating players");
       if(crewmembers.Count <= 0)
       {
            for(int i = 0; i < 3; ++i)
            {
                int range = Random.Range(0, Tiles.Length);

                Vector3 spawnPos = new Vector3(Tiles[range].transform.position.x,
                    Tiles[range].transform.position.y, Tiles[range].transform.position.z);

                spawnPos.y += 100f;

                Ray ray = new Ray(spawnPos, Vector3.down);

                RaycastHit hitinfo;

                if(Physics.Raycast(ray, out hitinfo, 100f, ~ignore))
                {
                    spawnPos = hitinfo.point;
                }

                //spawnPos.y += 1.0f;
                int chosenUnit = Random.Range(0, 2);
                if(chosenUnit == 0)
                {
                    clone = Instantiate(warrior, spawnPos, Quaternion.identity);
                    clone.name = warrior.name;
                }
                else if(chosenUnit == 1)
                {
                    clone = Instantiate(archer, spawnPos, Quaternion.identity);
                    clone.name = archer.name;
                }

                crewmembers.Add(clone);
                Debug.Log("created a player");
            }
        
       }
    }

    public void choosePlayer()
    {
        Debug.Log("Selecting player");
        int range = Random.Range(0, crewmembers.Count);
        movements.GetComponent<PlayerMovement>().Player = crewmembers[range];
        joystick.GetComponent<LockPlayerPos>().Player = crewmembers[range];
        if (crewmembers[range].name == "archerBlue")
        {
            menu.GetComponent<Menu>().archer = crewmembers[range];
        }
        else if(crewmembers[range].name == "knightBlue")
        {
            menu.GetComponent<Menu>().warrior = crewmembers[range];
        }

    }

    public void RemoveGO(GameObject GO2Remove)
    {
        crewmembers.Remove(GO2Remove);
        Destroy(GO2Remove);
    }
}
