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
    public GameObject dmgPanel;
    public void chargedShotBtnPress()
    {
        if(Archer.GetComponentInChildren<PlayerRanges>().enemies.Count > 0)
        {
            actionButtons.SetActive(false);
            joycon.SetActive(false);
            pausebutton.SetActive(false);

            combatControlButtons.SetActive(true);
            atkConfirmation.SetActive(true);
            atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("chargedshot");
            Archer.GetComponent<Archer>().state = "attack";
            dmgPanel.SetActive(false);
            gameObject.SetActive(false);
        }
        //Archer.GetComponent<Archer>().chargedShotAtk();
       
    }

    public void repeatedShot()
    {
        if (Archer.GetComponentInChildren<PlayerRanges>().enemies.Count > 0)
        {
            actionButtons.SetActive(false);
            joycon.SetActive(false);
            pausebutton.SetActive(false);

            combatControlButtons.SetActive(true);
            atkConfirmation.SetActive(true);
            atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("repeatedshot");
            Archer.GetComponent<Archer>().state = "attack";
            dmgPanel.SetActive(false);
            gameObject.SetActive(false);
        }

            //Archer.GetComponent<Archer>().repeatedShotAtk();
        
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}
