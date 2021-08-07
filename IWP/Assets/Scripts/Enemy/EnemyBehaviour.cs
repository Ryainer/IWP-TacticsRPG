using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class EnemyBehaviour : MonoBehaviour
{
    public int eneHealth;
    public int eneMP;
    public int eneAtk;
    public int eneskill;
    public string namechck;
    private TurnsManager turns;
    private GameObject dmgindicator;
    private GameObject healthBar;
    public SameAbilityRange range;
    public Text enemydmgtxt;
    
    private bool switchOn;
    public List<GameObject> playersinRange = new List<GameObject>();
    private List<GameObject> tilesinRange = new List<GameObject>();

    public Warrior playerWarrior;
    public Archer playerArcher;
    
    private NavMeshAgent agent;

    public bool moving = false;
   
    public bool attacking = false;

    public LayerMask ignoreself;

    public ParticleSystem particles;

    private enemyRange ene_range;

    public Camera camera;

    private int countdown;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        eneHealth = 50;
        eneMP = 10;
        eneAtk = 5;
        eneskill = Random.Range(10, 20);
        switchOn = false;
        gameObject.GetComponentInChildren<healthbar>().setMaxHealth(eneHealth);
        //ene_range = FindObjectOfType<enemyRange>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        enemydmgtxt = GameObject.Find("EnemyDmg").GetComponent<Text>();
    }

    

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(attacking + "Sweda " + moving);
       if(attacking == true && countdown <= 0)
       {
            Debug.Log("Entering Attack");
            if(namechck == "Soldier")
            {
                enemyAttack();
            }
            else if(namechck == "Archer")
            {
                enemyShoot();
            }
            
       }
       else if(moving == true)
       {
            Debug.Log("Entering Moving");
            moveAround();
            
       }
       else if(countdown > 0)
        {
            countdown--;
        }

       if(eneHealth > 0)
       {
            gameObject.GetComponentInChildren<healthbar>().setHealth(eneHealth);
       }
       
    }

    private void LateUpdate()
    {
        if(turns != null && !turns.getTurn())
        {
            camera.transform.LookAt(transform.position);
        }
    }

    public void chooseAction()
    {
        int range = Random.Range(0, 10);
        turns = GameObject.Find("TurnManager").GetComponent<TurnsManager>();
        dmgindicator = GameObject.Find("EnemyDmg");
        ene_range = GetComponentInChildren<enemyRange>();
       
        playersinRange = ene_range.GetPlayersInCollider(/*transform.position*/);
        tilesinRange = ene_range.GetTilesInCollider();
       
        if (switchOn && !turns.getTurn())
        {
            Debug.Log(switchOn);
            if(playersinRange.Count > 0)
            {
                if (range <= 4)
                {
                    moving = true;
                    switchOn = false;
                    Debug.Log("Moving");
                    Debug.Log("moving bool" + moving);
                }
                else if (range >= 5)
                {
                    attacking = true;
                    switchOn = false;
                    Debug.Log("Attacking");
                    Debug.Log("attacking bool" + attacking);
                }
            }
            else
            {
                moving = true;
                switchOn = false;
                Debug.Log("Moving due to no enemies");
                Debug.Log("moving bool" + moving);
            }
        }
        //Debug.Log("moving bool"+moving);
    }

    void enemyAttack()
    {
        //attacking = false;
        if(!turns.getTurn())
        {
            int rand = Random.Range(0, playersinRange.Count);

            //players = turns.GetComponent<TurnsManager>().existingPlayers();
            GameObject playerToHit = playersinRange[rand];
            string[] target = playerToHit.gameObject.name.Split(' ');
            float hitchance = (eneAtk + eneskill);
            if (target[0] == "archerBlue")
            {
                playerArcher = playerToHit.GetComponent<Archer>();
                hitchance -= playerArcher.skillstat;
            }
            else if (target[0] == "knightBlue")
            {
                playerWarrior = playerToHit.GetComponent<Warrior>();
                hitchance -= playerWarrior.skillstat;
            }


            float range = Random.Range(0, 100);

            if(range < hitchance)
            {
                //FindObjectOfType<AudioManager>().Player("miss");
                enemydmgtxt.text = "Enemy warrior Missed";
                particles = playerToHit.transform.GetChild(9).GetComponent<ParticleSystem>();
                particles.Play();
            }
            else if(range > hitchance)
            {
                //FindObjectOfType<AudioManager>().Player("slash");
                if (target[0] == "archerBlue")
                {
                    playerArcher.health -= eneAtk;
                }
                else if(target[0] == "knightBlue")
                {
                    playerWarrior.health -= eneAtk;
                }
                particles = playerToHit.transform.GetChild(8).GetComponent<ParticleSystem>();
                particles.Play();
                enemydmgtxt.text = "Enemy dealt " + eneAtk + " to " + playerToHit.name;
            }
            turns.setTurn(true);
            turns.swapControls();
            Debug.Log("EnemySAttacked");
        }
        attacking = false;
       
    }

    void enemyShoot()
    {
        if (!turns.getTurn())
        {
            int rand = Random.Range(0, playersinRange.Count);
            
            GameObject playerToHit = playersinRange[rand];

            string[] target = playerToHit.gameObject.name.Split(' ');
            float hitchance = (eneAtk + eneskill);
            if (target[0] == "archerBlue")
            {
                playerArcher = playerToHit.GetComponent<Archer>();
                hitchance -= playerArcher.skillstat;
            }
            else if (target[0] == "knightBlue")
            {
                playerWarrior = playerToHit.GetComponent<Warrior>();
                hitchance -= playerWarrior.skillstat;
            }


            float range = Random.Range(0, 100);

            if (range < hitchance)
            {
                particles = playerToHit.transform.GetChild(9).GetComponent<ParticleSystem>();
                particles.Play();
                //FindObjectOfType<AudioManager>().Player("miss");
                enemydmgtxt.text = "Enemy archer Missed";
            }
            else if (range > hitchance)
            {
                

                //FindObjectOfType<AudioManager>().Player("thwack");
                if (target[0] == "archerBlue")
                {
                    playerArcher.health -= eneAtk;
                }
                else if (target[0] == "knightBlue")
                {
                    playerWarrior.health -= eneAtk;
                }
                enemydmgtxt.text = "Enemy archer dealt " + eneAtk + " to " + playerToHit.name;
                particles = playerToHit.transform.GetChild(8).GetComponent<ParticleSystem>();
                particles.Play();
            }
            turns.setTurn(true);
            turns.swapControls();
            Debug.Log("EnemyAAttacked");
        }
        attacking = false;

    }

   
    public void moveAround()
    {
        
        int randomLocation = Random.Range(0, tilesinRange.Count );
        Debug.Log("chosen number location" + randomLocation);
      

        Vector3 newPosition = tilesinRange[randomLocation].GetComponent<Tiles>().center;

        newPosition.y += 10f;

        Ray ray = new Ray(newPosition, Vector3.down);

        RaycastHit hitinfo;

        if (Physics.Raycast(ray, out hitinfo, 150f, ~ignoreself))
        {
            newPosition = hitinfo.point;
        }
        Debug.Log("target: " + newPosition);
        Debug.Log(gameObject.name + " Old Position: " + gameObject.transform.position);
        // agent.SetDestination(newPosition);
        StartCoroutine(movingtoNewLocation(newPosition));

        Debug.Log(gameObject.name + " New Position: " + gameObject.transform.position);


        moving = false;
    }

    public void setSwitch(bool enableSwitch )
    {
        this.switchOn = enableSwitch;
    }

    IEnumerator movingtoNewLocation(Vector3 Target)
    {
        while(Vector3.Distance(transform.position, Target) > 0.5f)
        {
            agent.SetDestination(Target);
            Debug.Log("oh no: " + Vector3.Distance(transform.position, Target));
            yield return new WaitForSeconds(3);
        }
        // ene_range.GetPlayersInCollider();
        postActions();
        yield return /*null*/new WaitForSeconds(3); 
    }

    private void postActions()
    {
        playersinRange = ene_range.GetPlayersInCollider();
        if (playersinRange.Count > 0)
        {
            string[] chck = gameObject.name.Split(' ');

            if (chck[0] == "Soldier")
            {
                Debug.Log("target soldier");
                enemyAttack();
            }
            else if (chck[0] == "Archer")
            {
                Debug.Log("target archer");
                enemyShoot();
            }
            Debug.Log("Oh boi" + playersinRange.Count);
        }
        else
        {
            Debug.Log("no players to target");
            turns.GetComponent<TurnsManager>().setTurn(true);
            turns.GetComponent<TurnsManager>().swapControls();
        }
    }    
}
