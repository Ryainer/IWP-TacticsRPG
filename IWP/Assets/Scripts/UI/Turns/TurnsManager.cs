using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnsManager : MonoBehaviour
{
    public Text turns;
    public bool turnCounter/* { get; set; }*/ ;
    public GameObject[] players;
    public GameObject[] enemy;
    public GameObject Plamanage;
    public GameObject Enemanage;
    public bool whostarts;
    public Board boardLoad;

    public List<GameObject> currentPlayers = new List<GameObject>();
    public List<GameObject> currentEnemy = new List<GameObject>();
  
    private void Awake()
    {
        turnCounter = true;
        boardLoad.Load(FindObjectOfType<LevelList>().levels[0]);
    }

    // Start is called before the first frame update
    void Start()
    {
        whostarts = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(turnCounter);
        if (whostarts)
        {
            int range = Random.Range(0, 10);
            enemy = GameObject.FindGameObjectsWithTag("Enemy");
            players = GameObject.FindGameObjectsWithTag("Player");
            if (range < 5)
            {
                turnCounter = true;
                swapControls();
                whostarts = false;
            }
            else if (range > 5)
            {
                turnCounter = false;
                swapControls();
                whostarts = false;
            }
        }

        if (turnCounter && Plamanage.GetComponent<Player>().crewmembers.Count > 0)
        {
            turns.text = "Turn: Player";
        }
        else if (!turnCounter && Enemanage.GetComponent<EnemyManager>().enemies.Count > 0)
        {
            turns.text = "Turn: Enemy";
        }
    }

    public bool getTurn()
    {
        return turnCounter;
    }

    public void setTurn(bool turns)
    {
        Debug.Log("yes");
        turnCounter = turns;
    }

    public void swapControls()
    {
        if(turnCounter && Plamanage.GetComponent<Player>().crewmembers.Count > 0)
        {
            Plamanage.GetComponent<Player>().choosePlayer();
            Debug.Log("yeyasy player");
        }
        else if(!turnCounter && Enemanage.GetComponent<EnemyManager>().enemies.Count > 0)
        {
            
            Enemanage.GetComponent<EnemyManager>().chooseAnEnemy();
            Debug.Log("yesyenemy");
        }

        if(Plamanage.GetComponent<Player>().crewmembers.Count <= 0)
        {
            turns.text = "enemy wins";
        }
        else if(Enemanage.GetComponent<EnemyManager>().enemies.Count <= 0)
        {
            turns.text = "player wins";
        }
    }

    public GameObject[] existingPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        return players;
    }

    public GameObject[] existingEnemy()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        return enemy;
    }

    public void endgame(string lossmsg)
    {
        //currentPlayers = Plamanage.GetComponent<Player>().crewmembers;
        //currentEnemy = Enemanage.GetComponent<EnemyManager>().enemies;
        if (lossmsg == "player loss")
        {
            turns.text = "enemy wins";
        }
        else if (lossmsg == "enemy loss")
        {
            turns.text = "player wins";
        }
    }
   
}
