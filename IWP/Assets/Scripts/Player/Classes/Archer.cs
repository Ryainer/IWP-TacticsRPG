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
    public Text dmgTxt;
    int jump;
    int def;
   public int MP;

    public TurnsManager turnsystem;

    public ParticleSystem particles;
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
        dmgTxt = GameObject.Find("PlayerDmg").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
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
                    
                    float range = Random.Range(0, 100);

                    if (range < chance)
                    {
                        //FindObjectOfType<AudioManager>().Player("miss");
                        dmgTxt.text = "Archer Missed";
                        turnsystem.setTurn(false);
                        turnsystem.swapControls();
                        transform.GetChild(6).gameObject.SetActive(false);
                        transform.GetChild(5).gameObject.SetActive(false);
                        particles = enemyToHit.transform.GetChild(7).GetComponent<ParticleSystem>();
                        particles.Play();
                    }
                    else if (range > chance)
                    {
                        //FindObjectOfType<AudioManager>().Player("thwack");
                        enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= atkstat;
                        dmgTxt.text = "Archer dealt " + atkstat + " to " + enemyToHit.name;
                        turnsystem.setTurn(false);
                        turnsystem.swapControls();
                        transform.GetChild(5).gameObject.SetActive(false);
                        transform.GetChild(6).gameObject.SetActive(false);
                        particles = enemyToHit.transform.GetChild(6).GetComponent<ParticleSystem>();
                        particles.Play();
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


                    float range = Random.Range(0, 100);

                    if (range < chance)
                    {
                        FindObjectOfType<AudioManager>().Player("miss");
                        MP -= 5;
                        dmgTxt.text = "Archer Missed";
                        turnsystem.setTurn(false);
                        turnsystem.swapControls();
                        transform.GetChild(6).gameObject.SetActive(false);
                        transform.GetChild(5).gameObject.SetActive(false);
                        particles = enemyToHit.transform.GetChild(7).GetComponent<ParticleSystem>();
                        particles.Play();
                    }
                    else if (range > chance)
                    {
                        FindObjectOfType<AudioManager>().Player("thwack");
                        MP -= 5;
                        enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= atkstat * 2;
                        dmgTxt.text = "Archer dealt " + atkstat + " to " + enemyToHit.name;
                        turnsystem.setTurn(false);
                        turnsystem.swapControls();
                        transform.GetChild(6).gameObject.SetActive(false);
                        transform.GetChild(5).gameObject.SetActive(false);
                        particles = enemyToHit.transform.GetChild(6).GetComponent<ParticleSystem>();
                        particles.Play();
                    }
                }
                else if(MP <= 0)
                {
                    dmgTxt.text = "not enough MP!";
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
                        float range = Random.Range(0, 100);

                        if (range < chance)
                        {
                            dmgTxt.text = "Archer Missed";
                            particles = enemyToHit.transform.GetChild(7).GetComponent<ParticleSystem>();
                            particles.Play();
                            shots++;
                        }
                        else if (range > chance)
                        {
                            //FindObjectOfType<AudioManager>().Player("thwack");
                            enemyToHit.GetComponent<EnemyBehaviour>().eneHealth -= atkstat;
                            dmgTxt.text = "Archer dealt " + atkstat + " to " + enemyToHit.name;
                            particles = enemyToHit.transform.GetChild(6).GetComponent<ParticleSystem>();
                            particles.Play();
                            shots++;
                        }
                    }
                    MP -= 6;
                    turnsystem.setTurn(false);
                    turnsystem.swapControls();
                    transform.GetChild(5).gameObject.SetActive(false);
                    transform.GetChild(6).gameObject.SetActive(false);
                }
                else if (MP <= 0)
                {
                    dmgTxt.text = "not enough MP!";
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
}
