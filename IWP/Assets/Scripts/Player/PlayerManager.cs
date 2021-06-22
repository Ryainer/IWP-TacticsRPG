using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<Player> playercrew = new List<Player>();

    public static PlayerManager Instance{ get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(playercrew.Count != 1)
        {
            CreateInitialCrew();
        }
    }

    public void CreateInitialCrew()
    {
        Debug.Log("Creatingcrew");
        GameObject Crew;
        Crew = GameObject.Instantiate(LoadPrefabFromFile("Player"));
        Crew.GetComponent<Player>().createPlayers();
        playercrew.Add(Crew.GetComponent<Player>());
       
    }

    private GameObject LoadPrefabFromFile(string filename)
    {
        var loading = Resources.Load(filename) as GameObject;
        if(loading == null)
        {
            Debug.Log("null loaded");
            return null;
        }
        return loading;
    }
}
