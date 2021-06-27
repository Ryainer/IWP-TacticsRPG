using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChooseTarget : MonoBehaviour
{
    //[HideInInspector]
    public GameObject user;
    private string skill;
    private int hitchance;
    private List<GameObject> choices = new List<GameObject>();
    public Text enemy;
    int num = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void nxtTarget()
    {
        if(num < choices.Count)
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

    public void cancelButton()
    {
        if(gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
    public void targetsSelect(string move, int chance)
    {
        if (user != null)
        {
            choices = user.GetComponentInChildren<PlayerRanges>().enemies;
        }
        skill = move;
        hitchance = chance;
        enemy.text = "Name: " + choices[0].name + "\n" + "Health: " + choices[0].GetComponent<EnemyBehaviour>().eneHealth
            + "\n" + "Hitchance" + (chance - heightCheck(user.transform.position.y, choices[0].transform.position.y))  + "%";

    }

    public void targetsSelect(int choice)
    {
        enemy.text = "Name: " + choices[choice].name + "\n" + "Health: " + choices[0].GetComponent<EnemyBehaviour>().eneHealth
            + "\n" + "Hitchance" + (hitchance - heightCheck(user.transform.position.y, choices[choice].transform.position.y))  + "%";
    }

    public void executeCommand()
    {

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
