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

    public List<GameObject> currentPlayers = new List<GameObject>();
    public List<GameObject> currentEnemy = new List<GameObject>();
  
    private void Awake()
    {
        turnCounter = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        whostarts = true;
        currentPlayers = Plamanage.GetComponent<Player>().crewmembers;
        currentEnemy = Enemanage.GetComponent<EnemyManager>().enemies;
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
                whostarts =false;
            }
            else if(range > 5)
            {
                turnCounter = false;
                swapControls();
                whostarts = false;
            }
        }

        if (turnCounter)
        {
            turns.text = "Turn: Player";
        }
        else if(!turnCounter)
        {
            turns.text = "Turn: Enemy";
            
        }

        if(players.Length <= 0)
        {
            turns.text = "enemy wins";
        }
        else if(enemy.Length <= 0)
        {
            turns.text = "player wins";
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
        if(turnCounter)
        {
            Plamanage.GetComponent<Player>().choosePlayer();
        }
        else
        {
            Enemanage.GetComponent<EnemyManager>().chooseAnEnemy();
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

   
}
