using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archer : MonoBehaviour
{
    
    public GameObject dmgindicator;
    public string state;
    public Player playerStats;
    public int health;
    public int atkstat;
    public int skillstat;
    int jump;
    int def;
   public int MP;

    public TurnsManager turnsystem;

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
        turnsystem = GameObject.Find("TurnManager").GetComponent<TurnsManager>();
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
        
        dmgindicator = GameObject.Find("PlayerDmg");
        if (turnsystem != null)
        {
            if (turnsystem.getTurn())
            {

                GameObject enemyToHit = target;
                if (enemyToHit != null)
                {
                    Debug.Log("CLOSEST ENEMY" + enemyToHit.name);


                    float range = 100;//Random.Range(0, 100);

                    if (range < chance)
                    {
                        FindObjectOfType<AudioManager>().Player("miss");
                        dmgindicator.GetComponent<Text>().text = "Archer Missed";
                        turnsystem.setTurn(false);
                        turnsystem.swapControls();
                        transform.GetChild(5).gameObject.SetActive(false);
                    }
                    else if (range > chance)
                    {
                        FindObjectOfType<AudioManager>().Player("thwack");
                        Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                        enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= atkstat;
                        dmgindicator.GetComponent<Text>().text = "Archer dealt " + atkstat + " to " + enemyToHit.name;
                        turnsystem.setTurn(false);
                        turnsystem.swapControls();
                        transform.GetChild(5).gameObject.SetActive(false);
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
       
        dmgindicator = GameObject.Find("PlayerDmg");
        if (turnsystem != null)
        {
            if (turnsystem.getTurn() && MP > 0)
            {

                GameObject enemyToHit = target;
                if (enemyToHit != null)
                {
                    Debug.Log("CLOSEST ENEMY" + enemyToHit.name);


                    float range = 100;//Random.Range(0, 100);

                    if (range < chance)
                    {
                        MP -= 5;
                        dmgindicator.GetComponent<Text>().text = "Archer Missed";
                        turnsystem.setTurn(false);
                        turnsystem.swapControls();
                        transform.GetChild(5).gameObject.SetActive(false);
                    }
                    else if (range > chance)
                    {
                        MP -= 5;
                        Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                        enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= atkstat * 2;
                        dmgindicator.GetComponent<Text>().text = "Archer dealt " + atkstat + " to " + enemyToHit.name;
                        turnsystem.setTurn(false);
                        turnsystem.swapControls();
                        transform.GetChild(5).gameObject.SetActive(false);
                        Debug.Log("after attack: " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                    }
                }
                else if(MP <= 0)
                {
                    dmgindicator.GetComponent<Text>().text = "not enough MP!";
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
        dmgindicator = GameObject.Find("PlayerDmg");
        if (turnsystem != null)
        {
            if (turnsystem.getTurn() && MP > 0)
            {

                GameObject enemyToHit = target;
                if (enemyToHit != null)
                {
                    Debug.Log("CLOSEST ENEMY" + enemyToHit.name);
                   
                    int shots = 0;
                    while(shots < 5)
                    {
                        float range = 100;//Random.Range(0, 100);

                        if (range < chance)
                        {
                            dmgindicator.GetComponent<Text>().text = "Archer Missed";
                            shots++;
                        }
                        else if (range > chance)
                        {
                            Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                            enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= atkstat;
                            dmgindicator.GetComponent<Text>().text = "Archer dealt " + atkstat + " to " + enemyToHit.name;
                            shots++;
                            Debug.Log("after attack: " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                        }
                    }
                    MP -= 6;
                    turnsystem.setTurn(false);
                    turnsystem.swapControls();
                    transform.GetChild(5).gameObject.SetActive(false);
                }
                else if (MP <= 0)
                {
                    dmgindicator.GetComponent<Text>().text = "not enough MP!";
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
