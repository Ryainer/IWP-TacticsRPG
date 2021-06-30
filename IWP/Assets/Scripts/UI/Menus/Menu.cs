﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject warriorMenu;
    public GameObject healerMenu;
    public GameObject archerMenu;
    public GameObject atkConfirmation;
    public GameObject warrior;
    public GameObject archer;
    public GameObject turns;

    public GameObject combatControlButtons;
    public GameObject actionButtons;
    public GameObject joycon;

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
            warrior.GetComponent<Warrior>().state = "attack";

            actionButtons.SetActive(false);
            joycon.SetActive(false);

            combatControlButtons.SetActive(true);
            atkConfirmation.SetActive(true);
            atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("attack");
        }
        else if(archer != null)
        {
            archer.GetComponent<Archer>().state = "attack";

            actionButtons.SetActive(false);
            joycon.SetActive(false);

            combatControlButtons.SetActive(true);
            atkConfirmation.SetActive(true);
            atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("attack");
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
