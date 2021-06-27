using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMenu : MonoBehaviour
{
    public GameObject warrior;
    public GameObject atkConfirmation;

    // Start is called before the first frame update
    void Start()
    {
        //warrior = GameObject.Find("WarriorSprite");
    }

    public void AtkButtonPress()
    {
        //warrior.GetComponent<Warrior>().DoubleSwing();

        atkConfirmation.SetActive(true);
        atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("DoubleSwing", 75);
    }

    public void DblSwing()
    {
        //warrior.GetComponent<Warrior>().ChargeSmash();

        atkConfirmation.SetActive(true);
        atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("ChargeSmash", 65);
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
