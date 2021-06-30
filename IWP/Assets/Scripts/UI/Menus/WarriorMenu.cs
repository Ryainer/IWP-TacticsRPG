﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMenu : MonoBehaviour
{
    public GameObject warrior;
    public GameObject atkConfirmation;
    public GameObject combatControlButtons;
    public GameObject actionButtons;
    public GameObject joycon;

    // Start is called before the first frame update
    void Start()
    {
        //warrior = GameObject.Find("WarriorSprite");
    }

    public void AtkButtonPress()
    {
        //warrior.GetComponent<Warrior>().DoubleSwing();

        actionButtons.SetActive(false);
        joycon.SetActive(false);

        combatControlButtons.SetActive(true);
        atkConfirmation.SetActive(true);
        atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("DoubleSwing");
    }

    public void DblSwing()
    {
        //warrior.GetComponent<Warrior>().ChargeSmash();
        actionButtons.SetActive(false);
        joycon.SetActive(false);

        combatControlButtons.SetActive(true);
        atkConfirmation.SetActive(true);
        atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("ChargeSmash");
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
