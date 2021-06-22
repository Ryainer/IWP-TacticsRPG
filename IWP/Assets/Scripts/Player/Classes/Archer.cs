using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archer : MonoBehaviour
{
    public GameObject turns;
    public GameObject dmgindicator;

    public Player playerStats;
    public int health;
    int attack;
    int jump;
    int def;
    int MP;

    private void Awake()
    {
        health = 35;
        attack = 3;
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
    }

    public void Attack()
    {
        turns = GameObject.Find("TurnManager");
        dmgindicator = GameObject.Find("PlayerDmg");
        if (turns != null)
        {
            if (turns.GetComponent<TurnsManager>().getTurn())
            {

                GameObject enemyToHit = searchNearestEnemyinRange();
                if (enemyToHit != null)
                {
                    Debug.Log("CLOSEST ENEMY" + enemyToHit.name);
                    float hitchance = 75 / heightCheck(transform.position.y, enemyToHit.transform.position.y);

                    float range = Random.Range(0, 100);

                    if (range < hitchance)
                    {
                        dmgindicator.GetComponent<Text>().text = "Archer Missed";
                        turns.GetComponent<TurnsManager>().setTurn(false);
                        turns.GetComponent<TurnsManager>().swapControls();
                    }
                    else if (range > hitchance)
                    {
                        Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                        enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= attack;
                        dmgindicator.GetComponent<Text>().text = "Archer dealt " + attack + " to " + enemyToHit.name;
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

    public void chargedShotAtk()
    {
        turns = GameObject.Find("TurnManager");
        dmgindicator = GameObject.Find("PlayerDmg");
        if (turns != null)
        {
            if (turns.GetComponent<TurnsManager>().getTurn())
            {

                GameObject enemyToHit = searchNearestEnemyinRange();
                if (enemyToHit != null)
                {
                    Debug.Log("CLOSEST ENEMY" + enemyToHit.name);
                    float hitchance = 55 / heightCheck(transform.position.y, enemyToHit.transform.position.y);

                    float range = Random.Range(0, 100);

                    if (range < hitchance)
                    {
                        dmgindicator.GetComponent<Text>().text = "Archer Missed";
                        turns.GetComponent<TurnsManager>().setTurn(false);
                        turns.GetComponent<TurnsManager>().swapControls();
                    }
                    else if (range > hitchance)
                    {
                        Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                        enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= attack * 2;
                        dmgindicator.GetComponent<Text>().text = "Archer dealt " + attack + " to " + enemyToHit.name;
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

    public void repeatedShotAtk()
    {
        turns = GameObject.Find("TurnManager");
        dmgindicator = GameObject.Find("PlayerDmg");
        if (turns != null)
        {
            if (turns.GetComponent<TurnsManager>().getTurn())
            {

                GameObject enemyToHit = searchNearestEnemyinRange();
                if (enemyToHit != null)
                {
                    Debug.Log("CLOSEST ENEMY" + enemyToHit.name);
                    float hitchance = 55 / heightCheck(transform.position.y, enemyToHit.transform.position.y);
                    int shots = 0;
                    while(shots < 5)
                    {
                        float range = Random.Range(0, 100);

                        if (range < hitchance)
                        {
                            dmgindicator.GetComponent<Text>().text = "Archer Missed";
                            
                        }
                        else if (range > hitchance)
                        {
                            Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                            enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= attack;
                            dmgindicator.GetComponent<Text>().text = "Archer dealt " + attack + " to " + enemyToHit.name;
                            
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

    private GameObject searchNearestEnemyinRange()
    {
        List<GameObject> enemiesinrange = new List<GameObject>();
        enemiesinrange = GameObject.Find("collider_archerB").GetComponent<PlayerRanges>().enemies;
        if (enemiesinrange.Count > 0)
        {
            Debug.Log("there is an enemy");
        }
        // enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy;
        closestEnemy = null;

        float distance = float.MaxValue;
        Vector3 PlayerPos = transform.position;


        foreach (GameObject enemy in enemiesinrange)
        {
            if (enemy == null)
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

        if (closestEnemy == null)
        {
            Debug.Log("closest enemy error");
        }

        return closestEnemy;
    }

    float heightCheck(float a, float b)
    {
        float heightFound = 0;

        if (a > b)
        {
            heightFound = a - b;
        }
        else if (a < b)
        {
            heightFound = b - a;
        }


        return heightFound;
    }
}
