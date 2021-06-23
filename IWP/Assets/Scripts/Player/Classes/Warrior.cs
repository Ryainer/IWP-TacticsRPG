using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warrior : MonoBehaviour
{
    public Tiles tile { get; protected set; }

    public GameObject turns;
    public GameObject dmgindicator;

    public Player playerStats;
    public int health ;

    private GameObject healthbar;

    int attack;
    int jump;
    int def;
    int MP;
    int timer;

    private void Awake()
    {
       // turns = new TurnsManager();
        health = 50;
        attack = 5;
        MP = 15;
    }

    private void Start()
    {
        health = 50;
        attack = 5;
        MP = 15;
        gameObject.GetComponentInChildren<healthbar>().setMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            GameObject playerManager = GameObject.Find("PlayerManager");
            playerManager.GetComponent<Player>().RemoveGO(gameObject);
        }
        else
        {
            gameObject.GetComponentInChildren<healthbar>().setHealth(health);
        }
    }

    public void Attack()
    {
        turns = GameObject.Find("TurnManager");
        dmgindicator = GameObject.Find("PlayerDmg");
        if (turns != null )
        {
            if (turns.GetComponent<TurnsManager>().getTurn())
            {
               
                GameObject enemyToHit = searchNearestEnemyinRange();
                if (enemyToHit != null)
                {
                    Debug.Log("CLOSEST ENEMY" + enemyToHit.name);
                    float hitchance = 100 / heightCheck(transform.position.y, enemyToHit.transform.position.y);

                    float range = Random.Range(0, 100);

                    if (range < hitchance)
                    {
                        dmgindicator.GetComponent<Text>().text = "Warrior Missed";
                        turns.GetComponent<TurnsManager>().setTurn(false);
                        turns.GetComponent<TurnsManager>().swapControls();
                    }
                    else if (range > hitchance)
                    {
                        Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                        enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= attack;
                        dmgindicator.GetComponent<Text>().text = "Warrior dealt " + attack + " to " + enemyToHit.name;
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

    public void DoubleSwing()
    {
        turns = GameObject.Find("TurnManager");
        dmgindicator = GameObject.Find("PlayerDmg");
        if (turns.GetComponent<TurnsManager>().getTurn() && MP > 0)
        {
           
            GameObject enemyToHit = searchNearestEnemyinRange();

            float hitchance = 55 / heightCheck(transform.position.y, enemyToHit.transform.position.y);

            float range = Random.Range(0, 100);

            if(range < hitchance)
            {
                dmgindicator.GetComponent<Text>().text = "Warrior missed double swing";
                MP -= 4;
                turns.GetComponent<TurnsManager>().setTurn(false);
                turns.GetComponent<TurnsManager>().swapControls();
            }
            else if(range > hitchance)
            {
                MP -= 4;
                Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= attack * 2;
                dmgindicator.GetComponent<Text>().text = "Warrior dealt " + (attack * 2) + " to " + enemyToHit.name;
                turns.GetComponent<TurnsManager>().setTurn(false);
                turns.GetComponent<TurnsManager>().swapControls();
                Debug.Log("after attack: " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
            }


            
            Debug.Log("DoubleSwing");
        }
        else if(MP <= 0)
        {
            dmgindicator.GetComponent<Text>().text = "Not Enough MP!!";
        }
    }

    public void ChargeSmash()
    {
        turns = GameObject.Find("TurnManager");
        dmgindicator = GameObject.Find("PlayerDmg");
        if(turns.GetComponent<TurnsManager>().getTurn() && MP > 0)
        {
           
            GameObject enemyToHit = searchNearestEnemyinRange();

            float hitchance = 65 / heightCheck(transform.position.y, enemyToHit.transform.position.y);
            float range = Random.Range(0, 100);

            if (range < hitchance)
            {
                MP -= 5;
                dmgindicator.GetComponent<Text>().text = "Warrior missed charge swing";
                turns.GetComponent<TurnsManager>().setTurn(false);
                turns.GetComponent<TurnsManager>().swapControls();
            }
            else if (range > hitchance)
            {
                MP -= 5;
                Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= attack * 4;
                dmgindicator.GetComponent<Text>().text = "Warrior dealt " + (attack * 4) + " to " + enemyToHit.name;
                turns.GetComponent<TurnsManager>().setTurn(false);
                turns.GetComponent<TurnsManager>().swapControls();
                Debug.Log("after attack: " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
            }

        }
        else if (MP <= 0)
        {
            dmgindicator.GetComponent<Text>().text = "Not Enough MP!!";
        }
        Debug.Log("ChargeSmash");
    }

    private GameObject searchNearestEnemyinRange()
    {
        List<GameObject> enemiesinrange = new List<GameObject>();
        enemiesinrange = GameObject.Find("collider_knightB").GetComponent<PlayerRanges>().enemies;
        if(enemiesinrange.Count <= 0)
        {
            Debug.Log("there is an enemy");
        }
       // enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy;
        closestEnemy = null;

        float distance = float.MaxValue;
        Vector3 PlayerPos = transform.position;
        

        foreach(GameObject enemy in enemiesinrange)
        {
            if(enemy == null)
            {
                Debug.Log("nulled");
            }
            else
            {
                Vector3 diff = enemy.transform.position - PlayerPos;
                float currentDist = diff.sqrMagnitude;

                if (currentDist < distance)
                {
                    closestEnemy = enemy;
                    distance = currentDist;
                    Debug.Log("checked");
                }
            }
            
        }

        if(closestEnemy == null)
        {
            Debug.Log("closest enemy error");
        }

        return closestEnemy;
    }

    float heightCheck(float a, float b)
    {
        float heightFound = 0;

        if(a > b)
        {
            heightFound = a - b;
        }
        else if(a < b)
        {
            heightFound = b - a;
        }


        return heightFound;
    }
}
