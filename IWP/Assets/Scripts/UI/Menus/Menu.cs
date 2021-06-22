using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject warriorMenu;
    public GameObject healerMenu;
    public GameObject archerMenu;
    public GameObject warrior;
    public GameObject archer;
    public GameObject turns;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onAtkBtnPress()
    {
        if(warrior != null)
        {
            warrior.GetComponent<Warrior>().Attack();
        }
        else if(archer != null)
        {
            archer.GetComponent<Archer>().Attack();
        }
    }

    public void onSkillBtnPress()
    {
        if(!warriorMenu.activeInHierarchy && warrior != null)
        {
            warriorMenu.SetActive(true);
            warriorMenu.GetComponent<WarriorMenu>().warrior = warrior;
        }
        else if(!archerMenu.activeInHierarchy && archer != null)
        {
            archerMenu.SetActive(true);
            archerMenu.GetComponent<ArcherMenu>().Archer = archer;
        }
    }

    public void onEndTurnBtnPress()
    {
        if(turns.GetComponent<TurnsManager>().getTurn())
        {
            turns.GetComponent<TurnsManager>().setTurn(false);
            turns.GetComponent<TurnsManager>().swapControls();
        }
    }

}
