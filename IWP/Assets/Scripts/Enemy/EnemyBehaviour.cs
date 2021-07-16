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
    private GameObject turns;
    private GameObject dmgindicator;
    private GameObject healthBar;
    public SameAbilityRange range;
    GameObject board;
    UnitTilePos pos;
    private bool switchOn;
    private List<GameObject> playersinRange = new List<GameObject>();
    private List<GameObject> tilesinRange = new List<GameObject>();
    private Rigidbody rigidbody;
    private NavMeshAgent agent;

    public bool moving = false;
   
    public bool attacking = false;

    public LayerMask ignoreself;

    private enemyRange ene_range;

    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.Find("Board");
        rigidbody = gameObject.GetComponent<Rigidbody>();
        eneHealth = 50;
        eneMP = 10;
        eneAtk = 5;
        eneskill = Random.Range(10, 20);
        switchOn = false;
        gameObject.GetComponentInChildren<healthbar>().setMaxHealth(eneHealth);
        ene_range = FindObjectOfType<enemyRange>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(attacking + "Sweda " + moving);
       if(attacking == true)
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

       if(eneHealth > 0)
       {
            gameObject.GetComponentInChildren<healthbar>().setHealth(eneHealth);
       }
       
    }

    public void chooseAction()
    {
        int range = Random.Range(0, 10);
        turns = GameObject.Find("TurnManager");
        //players = GameObject.FindGameObjectsWithTag("Player");
        dmgindicator = GameObject.Find("EnemyDmg");
        ene_range.GetPlayersInCollider();
        ene_range.GetTilesInCollider();
        playersinRange = ene_range.playersInRange;
        tilesinRange = ene_range.TilesInrange;
       
        if (switchOn && !turns.GetComponent<TurnsManager>().getTurn())
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
        if(!turns.GetComponent<TurnsManager>().getTurn())
        {
            //players = turns.GetComponent<TurnsManager>().existingPlayers();
            GameObject playerToHit = searchNearestplayerinRange();

            float hitchance = 100 / heightCheck(transform.position.y, playerToHit.transform.position.y);

            float range = Random.Range(0, 100);

            if(range < hitchance)
            {
                FindObjectOfType<AudioManager>().Player("miss");
                dmgindicator.GetComponent<Text>().text = "Enemy warrior Missed";
            }
            else if(range > hitchance)
            {
                FindObjectOfType<AudioManager>().Player("slash");
                if (playerToHit.gameObject.name == "archerBlue")
                {
                    playerToHit.GetComponent<Archer>().health -= eneAtk;
                }
                else if(playerToHit.gameObject.name == "knightBlue")
                {
                    playerToHit.GetComponent<Warrior>().health -= eneAtk;
                }

                dmgindicator.GetComponent<Text>().text = "Enemy warrior dealt " + eneAtk + " to " + playerToHit.name;
            }
            turns.GetComponent<TurnsManager>().setTurn(true);
            turns.GetComponent<TurnsManager>().swapControls();
            Debug.Log("EnemySAttacked");
        }
        attacking = false;
       
    }

    void enemyShoot()
    {
        if (!turns.GetComponent<TurnsManager>().getTurn())
        {
            //players = turns.GetComponent<TurnsManager>().existingPlayers();
            GameObject playerToHit = searchNearestplayerinRange();

            float hitchance = 75 / heightCheck(transform.position.y, playerToHit.transform.position.y);

            float range = Random.Range(0, 100);

            if (range < hitchance)
            {
                FindObjectOfType<AudioManager>().Player("miss");
                dmgindicator.GetComponent<Text>().text = "Enemy archer Missed";
            }
            else if (range > hitchance)
            {
                FindObjectOfType<AudioManager>().Player("thwack");
                if (playerToHit.gameObject.name == "archerBlue")
                {
                    playerToHit.GetComponent<Archer>().health -= eneAtk;
                }
                else if (playerToHit.gameObject.name == "knightBlue")
                {
                    playerToHit.GetComponent<Warrior>().health -= eneAtk;
                }
                dmgindicator.GetComponent<Text>().text = "Enemy archer dealt " + eneAtk + " to " + playerToHit.name;
            }
            turns.GetComponent<TurnsManager>().setTurn(true);
            turns.GetComponent<TurnsManager>().swapControls();
            Debug.Log("EnemyAAttacked");
        }
        attacking = false;

    }

    private GameObject searchNearestplayerinRange()
    {
        //List<GameObject> playersinRange = new List<GameObject>();
        GameObject closestPlayer;
        closestPlayer = null;

        float distance = Mathf.Infinity;
        Vector3 EnemyPos = transform.position;

        foreach (GameObject player in playersinRange)
        {
            Vector3 diff = player.transform.position - EnemyPos;
            float currentDist = diff.sqrMagnitude;

            if (currentDist < distance)
            {
                closestPlayer = player;
                distance = currentDist;
            }
        }

        if(closestPlayer == null)
        {
            Debug.Log("player is not detected");
        }

        return closestPlayer;
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

    public void moveAround()
    {
        
        int randomLocation = Random.Range(0, tilesinRange.Count );
        Debug.Log("chosen number location" + randomLocation);
        //Debug.Log("tilesinrange count: " + TilesinRange.Count);
        //Debug.Log(randomLocation);

        Vector3 newPosition = tilesinRange[randomLocation].GetComponent<Tiles>().center;

        newPosition.y += 10f;

        Ray ray = new Ray(newPosition, Vector3.down);

        RaycastHit hitinfo;

        if (Physics.Raycast(ray, out hitinfo, 150f, ~ignoreself))
        {
            newPosition = hitinfo.point;
        }
        Debug.Log("target: " + newPosition);
        //var hitColliders = Physics.OverlapSphere(newPosition, 2);

        //if (hitColliders.Length > 0.1)
        //{
        //    newPosition += Vector3.forward;
        //}

        //newPosition.y += 0.5f;

        //transform.position = newPosition;
        //Debug.Log(controller.enabled);

        //Vector3 travellingpos = transform.position;
        //travellingpos.y += 10;
        //transform.position = travellingpos;

        // rigidbody.MovePosition(newPosition * Time.deltaTime * 5f);
        //transform.Translate(newPosition);
        Debug.Log(gameObject.name + " Old Position: " + gameObject.transform.position);
        // StartCoroutine("movingtoNewLocation", newPosition);
        agent.SetDestination(newPosition);
        //transform.position = Vector3.Lerp(transform.position, newPosition, 2f * Time.deltaTime);
        //rigidbody.MovePosition(newPosition);

        Debug.Log(gameObject.name + " New Position: " + gameObject.transform.position);
        turns.GetComponent<TurnsManager>().setTurn(true);
        turns.GetComponent<TurnsManager>().swapControls();
        moving = false;
    }

    public void setSwitch(bool enableSwitch )
    {
        this.switchOn = enableSwitch;
    }

    IEnumerator movingtoNewLocation(Vector3 Target)
    {
        while(Vector3.Distance(transform.position, Target) > 0.05f)
        {
            //transform.position = Vector3.MoveTowards(transform.position, Target, 1f * Time.deltaTime);
            Vector3 tempGO = gameObject.transform.position;
            tempGO+= Vector3.forward;

            Ray ray = new Ray(tempGO, Vector3.down);

            RaycastHit hitinfo;

            if(Physics.Raycast(ray, out hitinfo, 20f))
            {
                transform.position = tempGO;
            }
            else
            {
                Debug.Log("Nothing");
            }

            
            yield return new WaitForSeconds(2);
        }

        yield return new WaitForSeconds(3); 
    }
}
