using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChooseTarget : MonoBehaviour
{
    //[HideInInspector]
    public GameObject user;

    public GameObject camerarig;
    public Button cancel;
    private Vector3 OGCameraPOS;
    public GameObject combatControls;
    public GameObject joycon;
    private string skill;
    private float hitchance;
    private List<GameObject> choices = new List<GameObject>();
    public Text enemy;
    public Text playerMP;
    public GameObject dmgPanel;
    int num = 0;

    // Start is called before the first frame update
    void Start()
    {
        OGCameraPOS = camerarig.transform.position;
        cancel.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.2f;
    }

    public void nxtTarget()
    {
        if (num < choices.Count -1)
        {
            num++;
            targetsSelect(num);
        }
    }

    public void prevTarget()
    {
        if (num != 0)
        {
            --num;
            targetsSelect(num);
        }
    }

    public void confirmButton()
    {
        changeTiles();

        executeCommand(choices[num], skill);
    }

    public void cancelAttackButton()
    {
        gameObject.SetActive(false);
        combatControls.SetActive(false);
        
        string[] tokens = user.name.Split(' ');

        if (tokens[0] == "AllyarcherBlue")
        {
            user.GetComponent<Archer>().state = "";
        }
        else if (tokens[0] == "AllyknightBlue")
        {
            user.GetComponent<Warrior>().state = "";
        }
        camerarig.transform.LookAt(user.transform.position);
    }

    public void targetsSelect(string move)
    {
        OGCameraPOS = camerarig.transform.position;
        if (user != null )
        {
           
            choices = user.GetComponentInChildren<PlayerRanges>().enemies;
            Vector3 position = choices[0].transform.position;

            camerarig.transform.LookAt(position);

            skill = move;
            hitchance = hitrate();
            enemy.text = "Name: " + choices[0].name + "\n" + "Health: " + choices[0].GetComponent<EnemyBehaviour>().eneHealth
                + "\n" + "Hitchance" + hitrate() + "%";

            if (move != "attack")
            {
                string[] tokens = user.name.Split(' ');

                if (tokens[0] == "AllyarcherBlue")
                {
                    playerMP.text = "Player MP: " + user.GetComponent<Archer>().MP;
                }
                else if (tokens[0] == "AllyknightBlue")
                {
                    playerMP.text = "Player MP: " + user.GetComponent<Warrior>().MP;
                }
            }
        }
        else
        {
            camerarig.transform.LookAt(user.transform.position);
            gameObject.SetActive(false);
            combatControls.SetActive(false);
            joycon.SetActive(true);

            string[] tokens = user.name.Split(' ');

            if (tokens[0] == "AllyarcherBlue")
            {
                user.GetComponent<Archer>().state = "";
            }
            else if (tokens[0] == "AllyknightBlue")
            {
                user.GetComponent<Warrior>().state = "";
            }
            
        }
        

    }

    public void targetsSelect(int choice)
    {
       
        Vector3 position = choices[choice].transform.position;

        camerarig.transform.LookAt(position);

        hitchance = hitrate();
        enemy.text = "Name: " + choices[choice].name + "\n" + "Health: " + choices[0].GetComponent<EnemyBehaviour>().eneHealth
            + "\n" + "Hitchance" + hitrate() + "%";

        if (skill != "attack")
        {
            string[] tokens = user.name.Split(' ');

            if (tokens[0] == "AllyarcherBlue")
            {
                playerMP.text = "Player MP: " + user.GetComponent<Archer>().MP;
            }
            else if (tokens[0] == "AllyknightBlue")
            {
                playerMP.text = "Player MP: " + user.GetComponent<Warrior>().MP;
            }
        }

    }

    public void executeCommand(GameObject enemy, string actionused)
    {

        switch (actionused)
        {
            case "attack":
                {
                    string[] tokens = user.name.Split(' ');

                    if (tokens[0] == "AllyarcherBlue")
                    {
                        user.GetComponent<Archer>().Attack(enemy, hitchance);
                        if (gameObject.activeInHierarchy)
                        {
                            gameObject.SetActive(false);
                            
                            //joycon.SetActive(true);
                            user.GetComponent<Archer>().state = "";
                        }
                    }
                    else if (tokens[0] == "AllyknightBlue")
                    {
                        user.GetComponent<Warrior>().Attack(enemy, hitchance);
                        if (gameObject.activeInHierarchy)
                        {
                            gameObject.SetActive(false);
                            
                            //joycon.SetActive(true);
                            user.GetComponent<Warrior>().state = "";
                        }
                    }
                }
                break;
            case "DoubleSwing":
                {
                    user.GetComponent<Warrior>().DoubleSwing(enemy, hitchance);
                    if (gameObject.activeInHierarchy)
                    {
                        gameObject.SetActive(false);
                        
                        //joycon.SetActive(true);
                        user.GetComponent<Warrior>().state = "";
                    }
                }
                break;
            case "ChargeSmash":
                {
                    user.GetComponent<Warrior>().ChargeSmash(enemy, hitchance);
                    if (gameObject.activeInHierarchy)
                    {
                        gameObject.SetActive(false);
                        
                        //joycon.SetActive(true);
                        user.GetComponent<Warrior>().state = "";
                    }
                }
                break;
            case "chargedshot":
                {
                    user.GetComponent<Archer>().chargedShotAtk(enemy, hitchance);
                    if (gameObject.activeInHierarchy)
                    {
                        gameObject.SetActive(false);
                        
                        //joycon.SetActive(true);
                        user.GetComponent<Archer>().state = "";
                    }
                }
                break;
            case "repeatedshot":
                {
                    user.GetComponent<Archer>().repeatedShotAtk(enemy, hitchance);
                    if (gameObject.activeInHierarchy)
                    {
                        gameObject.SetActive(false);
                      
                        //joycon.SetActive(true);
                        user.GetComponent<Archer>().state = "";
                    }
                }
                break;
        }
        
    }

    float hitrate()
    {
        float rate = 50;

        string[] tokens = user.name.Split(' ');

        if (tokens[0] == "AllyarcherBlue")
        {
            rate = (user.GetComponent<Archer>().atkstat + user.GetComponent<Archer>().skillstat)
                - choices[num].GetComponent<EnemyBehaviour>().eneskill;
        }
        else if (tokens[0] == "AllyknightBlue")
        {
            rate = (user.GetComponent<Warrior>().atkstat + user.GetComponent<Warrior>().skillstat)
                - choices[num].GetComponent<EnemyBehaviour>().eneskill;
        }

        return rate;
    }

    private void changeTiles()
    {
        GameObject[] Tiles;

        Tiles = GameObject.FindGameObjectsWithTag("SelectedTiles");

        foreach (GameObject tile in Tiles)
        {
            Material OGMaterial = Resources.Load<Material>("Materials/grass");

            tile.GetComponent<MeshRenderer>().material = OGMaterial;
            tile.tag = "Tiles";
        }
    }
}
