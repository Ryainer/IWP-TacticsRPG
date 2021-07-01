using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warrior : MonoBehaviour
{
    public Tiles tile { get; protected set; }

    public GameObject dmgindicator;
    public string state;
    public Player playerStats;
    public int health ;

    private GameObject healthbar;

    public TurnsManager turnsystem;
    public Text dmgTxt;

    public int atkstat;
    int jump;
    int def;
    int MP;
    int timer;
    public float skillstat;

    private void Awake()
    {
       // turns = new TurnsManager();
        health = 50;
        atkstat = 40;
        MP = 15;
        skillstat = 25;
    }

    private void Start()
    {
        health = 50;
        //atkstat = 5;
        MP = 15;
        gameObject.GetComponentInChildren<healthbar>().setMaxHealth(health);
        turnsystem = GameObject.Find("TurnManager").GetComponent<TurnsManager>();
        dmgTxt = GameObject.Find("PlayerDmg").GetComponent<Text>();
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

    public void Attack(GameObject target, float chance)
    {

        if (turnsystem != null)
        {
            if (turnsystem.getTurn())
            {
                GameObject enemyToHit = target;
                if (enemyToHit != null)
                {
                    //Debug.Log("CLOSEST ENEMY" + enemyToHit.name);
                    
                    float range = Random.Range(0, 100);

                    if (range < chance)
                    {
                        FindObjectOfType<AudioManager>().Player("miss");
                        dmgTxt.text = "Warrior Missed";
                        turnsystem.setTurn(false);
                        turnsystem.swapControls();
                    }
                    else if (range > chance)
                    {
                        FindObjectOfType<AudioManager>().Player("slash");
                        //Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                        enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= atkstat;
                        dmgTxt.text = "Warrior dealt " + atkstat + " to " + enemyToHit.name;
                        turnsystem.setTurn(false);
                        turnsystem.swapControls();
                       // Debug.Log("after attack: " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                    }
                }
                else
                {
                    dmgTxt.text = "No enemy in range";
                }

            }
        }
        else
        {
            Debug.Log("NULL");
        }

    }

    public void DoubleSwing(GameObject target, float chance)
    {
        if (turnsystem.getTurn() && MP > 0)
        {

            GameObject enemyToHit = target;

            float range = Random.Range(0, 100);

            if (range < chance)
            {
                dmgTxt.text = "Warrior missed double swing";
                MP -= 4;
                turnsystem.setTurn(false);
                turnsystem.swapControls();
            }
            else if (range > chance)
            {
                MP -= 4;
                //Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= atkstat * 2;
                dmgTxt.text = "Warrior dealt " + (atkstat * 2) + " to " + enemyToHit.name;
                turnsystem.setTurn(false);
                turnsystem.swapControls();
               // Debug.Log("after attack: " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
            }



           // Debug.Log("DoubleSwing");
        }
        else if (MP <= 0)
        {
            dmgTxt.text = "Not Enough MP!!";
        }
    }

    public void ChargeSmash(GameObject target, float chance)
    {
   
        if (turnsystem.getTurn() && MP > 0)
        {

            GameObject enemyToHit = target;

            float range = Random.Range(0, 100);

            if (range < chance)
            {
                MP -= 5;
                dmgTxt.text = "Warrior missed charge swing";
                turnsystem.setTurn(false);
                turnsystem.swapControls();
            }
            else if (range > chance)
            {
                MP -= 5;
                //Debug.Log("Initial health of " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
                enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= atkstat * 4;
                dmgTxt.text = "Warrior dealt " + (atkstat * 4) + " to " + enemyToHit.name;
                turnsystem.setTurn(false);
                turnsystem.swapControls();
                //Debug.Log("after attack: " + enemyToHit.name + " " + enemyToHit.GetComponent<EnemyBehaviour>().eneHealth);
            }

        }
        else if (MP <= 0)
        {
            dmgTxt.text = "Not Enough MP!!";
        }
        Debug.Log("ChargeSmash");
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
