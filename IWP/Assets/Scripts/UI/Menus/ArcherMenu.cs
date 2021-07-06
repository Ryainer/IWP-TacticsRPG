using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMenu : MonoBehaviour
{
    public GameObject Archer;
    public GameObject atkConfirmation;

    public GameObject combatControlButtons;
    public GameObject actionButtons;
    public GameObject joycon;
    public GameObject pausebutton;
    public void chargedShotBtnPress()
    {
        //Archer.GetComponent<Archer>().chargedShotAtk();
        actionButtons.SetActive(false);
        joycon.SetActive(false);
        pausebutton.SetActive(false);

        combatControlButtons.SetActive(true);
        atkConfirmation.SetActive(true);
        atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("chargedshot");
        Archer.GetComponent<Archer>().state = "attack";
        gameObject.SetActive(false);
    }

    public void repeatedShot()
    {
        //Archer.GetComponent<Archer>().repeatedShotAtk();
        actionButtons.SetActive(false);
        joycon.SetActive(false);
        pausebutton.SetActive(false);

        combatControlButtons.SetActive(true);
        atkConfirmation.SetActive(true);
        atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("repeatedshot");
        Archer.GetComponent<Archer>().state = "attack";
        gameObject.SetActive(false);
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}
