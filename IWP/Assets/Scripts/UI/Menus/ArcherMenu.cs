using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMenu : MonoBehaviour
{
    public GameObject Archer;
    public GameObject atkConfirmation;

    public GameObject nxtButtonConfirm;
    public GameObject prevButtonConfirm;
    public GameObject atkButton;
    public GameObject skillButton;
    public GameObject endTurnButton;
    public GameObject joycon;
    public void chargedShotBtnPress()
    {
        //Archer.GetComponent<Archer>().chargedShotAtk();
        atkButton.SetActive(false);
        skillButton.SetActive(false);
        endTurnButton.SetActive(false);
        joycon.SetActive(false);

        nxtButtonConfirm.SetActive(true);
        prevButtonConfirm.SetActive(true);

        atkConfirmation.SetActive(true);
        atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("chargedshot");
    }

    public void repeatedShot()
    {
        //Archer.GetComponent<Archer>().repeatedShotAtk();
        atkButton.SetActive(false);
        skillButton.SetActive(false);
        endTurnButton.SetActive(false);
        joycon.SetActive(false);

        nxtButtonConfirm.SetActive(true);
        prevButtonConfirm.SetActive(true);

        atkConfirmation.SetActive(true);
        atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("repeatedshot");
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}
