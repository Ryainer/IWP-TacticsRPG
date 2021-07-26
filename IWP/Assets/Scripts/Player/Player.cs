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
    public GameObject combatScreen;
    public LayerMask ignore;
    public GameObject cameraRig;
    public Menu menuselection;
    private GameObject clone;
    GameObject[] Tiles;
    void Start()
    {
        Tiles = GameObject.FindGameObjectsWithTag("Tiles");
        menuselection = menu.GetComponent<Menu>();
        createPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if(crewmembers.Count > 0)
        {
            for(int i = 0; i < crewmembers.Count; ++i)
            {
                if(crewmembers[i].name == "knightBlue" && crewmembers[i].GetComponent<Warrior>().health <= 0)
                {
                    GameObject playertodestroy = crewmembers[i];
                    crewmembers.Remove(crewmembers[i]);
                    Destroy(playertodestroy);
                }
                else if(crewmembers[i].name == "archerBlue" && crewmembers[i].GetComponent<Archer>().health <= 0)
                {
                    GameObject playertodestroy = crewmembers[i];
                    crewmembers.Remove(crewmembers[i]);
                    Destroy(playertodestroy);
                }
            }
            //GameObject.Find("TurnManager").GetComponent<TurnsManager>().currentPlayers = crewmembers;
        }    
    }

    public void createPlayers()
    {
       
        Debug.Log("creating players");
       if(crewmembers.Count <= 0)
       {
            for(int i = 0; i < 3; ++i)
            {
                int num = i;
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
                    clone.name = warrior.name + " " +num;
                }
                else if(chosenUnit == 1)
                {
                    clone = Instantiate(archer, spawnPos, Quaternion.identity);
                    clone.name = archer.name + " " + num;
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
        movements.GetComponent<PlayerMovement>().origin = crewmembers[range].transform.position;
        joystick.GetComponent<LockPlayerPos>().Player = crewmembers[range];
        combatScreen.GetComponent<PlayerChooseTarget>().user = crewmembers[range];
        crewmembers[range].transform.GetChild(5).gameObject.SetActive(true);
        crewmembers[range].transform.GetChild(6).gameObject.SetActive(true);
        cameraRig.transform.LookAt(crewmembers[range].transform.position);
        menuselection.character = crewmembers[range];
    }

    public void RemoveGO(GameObject GO2Remove)
    {
        crewmembers.Remove(GO2Remove);
        Destroy(GO2Remove);
    }
}
