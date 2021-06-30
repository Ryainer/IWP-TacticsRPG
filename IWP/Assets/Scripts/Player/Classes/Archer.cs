using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archer : MonoBehaviour
{
    public GameObject turns;
    public GameObject dmgindicator;
    public string state;
    public Player playerStats;
    public int health;
    public int atkstat;
    public int skillstat;
    int jump;
    int def;
    int MP;

    private void Awake()
    {
        health = 35;
        atkstat = 13;
        skillstat = 50;
        MP = 15;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameObject playerManager = GameObject.Find("PlayerManager");
            playerManager.GetComponent<Player>().RemoveGO(gameObject);
        }
        else
        {
            gameObject.GetComponentInChildren<healthbar>().setHealth(health);
        }
    }

    public void Attack(GameObject target, float chance)
    {
        turns = GameObject.Find("TurnManager");
        dmgindicator = GameObject.Find("PlayerDmg");
        if (turns != null)
        {
            if (turns.GetComponent<TurnsManager>().getTurn())
            {

                GameObject enemyToHit = target;
                if (enemyToHit != null)
                {
                    Debug.Log("CLOSEST ENEMY" + enemyToHit.name);
                  

                    float range = Random.Range(0, 100);

                    if (range < chance)
                    {
                        FindObjectOfType<AudioManager>().Player("miss");
                        dmgindicator.GetComponent<Text>().text = "Archer Missed";
                        turns.GetComponent<TurnsManager>().setTurn(false);
                        turns.GetComponent<TurnsManager>().swapControls();
                    }
                    else if (range > chance)
                    {
                        FindObjectOfType<AudioManager>().Player("thwack");
                        Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                        enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= atkstat;
                        dmgindicator.GetComponent<Text>().text = "Archer dealt " + atkstat + " to " + enemyToHit.name;
                        turns.GetComponent<TurnsManager>().setTurn(false);
                        turns.GetComponent<TurnsManager>().swapControls();
                        Debug.Log("after attack: " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                    }
                }
                else
                {
                    dmgindicator.GetComponent<Text>().text = "No enemy in range";
                }

            }
        }
        else
        {
            Debug.Log("NULL");
        }

    }

    public void chargedShotAtk(GameObject target, float chance)
    {
        turns = GameObject.Find("TurnManager");
        dmgindicator = GameObject.Find("PlayerDmg");
        if (turns != null)
        {
            if (turns.GetComponent<TurnsManager>().getTurn())
            {

                GameObject enemyToHit = target;
                if (enemyToHit != null)
                {
                    Debug.Log("CLOSEST ENEMY" + enemyToHit.name);
                    

                    float range = Random.Range(0, 100);

                    if (range < chance)
                    {
                        dmgindicator.GetComponent<Text>().text = "Archer Missed";
                        turns.GetComponent<TurnsManager>().setTurn(false);
                        turns.GetComponent<TurnsManager>().swapControls();
                    }
                    else if (range > chance)
                    {
                        Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                        enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= atkstat * 2;
                        dmgindicator.GetComponent<Text>().text = "Archer dealt " + atkstat + " to " + enemyToHit.name;
                        turns.GetComponent<TurnsManager>().setTurn(false);
                        turns.GetComponent<TurnsManager>().swapControls();
                        Debug.Log("after attack: " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                    }
                }
                else
                {
                    dmgindicator.GetComponent<Text>().text = "No enemy in range";
                }

            }
        }
        else
        {
            Debug.Log("NULL");
        }

    }

    public void repeatedShotAtk(GameObject target, float chance)
    {
        turns = GameObject.Find("TurnManager");
        dmgindicator = GameObject.Find("PlayerDmg");
        if (turns != null)
        {
            if (turns.GetComponent<TurnsManager>().getTurn())
            {

                GameObject enemyToHit = target;
                if (enemyToHit != null)
                {
                    Debug.Log("CLOSEST ENEMY" + enemyToHit.name);
                   
                    int shots = 0;
                    while(shots < 5)
                    {
                        float range = Random.Range(0, 100);

                        if (range < chance)
                        {
                            dmgindicator.GetComponent<Text>().text = "Archer Missed";
                            
                        }
                        else if (range > chance)
                        {
                            Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                            enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= atkstat;
                            dmgindicator.GetComponent<Text>().text = "Archer dealt " + atkstat + " to " + enemyToHit.name;
                            
                            Debug.Log("after attack: " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                        }
                    }
                    turns.GetComponent<TurnsManager>().setTurn(false);
                    turns.GetComponent<TurnsManager>().swapControls();
                }
                else
                {
                    dmgindicator.GetComponent<Text>().text = "No enemy in range";
                }

            }
        }
        else
        {
            Debug.Log("NULL");
        }

    }
}
