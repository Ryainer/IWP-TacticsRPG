using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMenu : MonoBehaviour
{
    public GameObject Archer;
    public GameObject atkConfirmation;
    public void chargedShotBtnPress()
    {
        //Archer.GetComponent<Archer>().chargedShotAtk();

        atkConfirmation.SetActive(true);
        atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("chargedshot", 75);
    }

    public void repeatedShot()
    {
        //Archer.GetComponent<Archer>().repeatedShotAtk();

        atkConfirmation.SetActive(true);
        atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("repeatedshot", 75);
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}
