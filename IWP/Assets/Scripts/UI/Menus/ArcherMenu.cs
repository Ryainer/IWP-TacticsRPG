using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMenu : MonoBehaviour
{
    public GameObject Archer;

    public void chargedShotBtnPress()
    {
        Archer.GetComponent<Archer>().chargedShotAtk();
    }

    public void repeatedShot()
    {
        Archer.GetComponent<Archer>().repeatedShotAtk();
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}
