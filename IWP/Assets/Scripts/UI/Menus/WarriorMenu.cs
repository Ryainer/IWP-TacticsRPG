using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMenu : MonoBehaviour
{
    public GameObject warrior;
  

    // Start is called before the first frame update
    void Start()
    {
        //warrior = GameObject.Find("WarriorSprite");
    }

    public void AtkButtonPress()
    {
        warrior.GetComponent<Warrior>().DoubleSwing();
    }

    public void DblSwing()
    {
        warrior.GetComponent<Warrior>().ChargeSmash();
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
