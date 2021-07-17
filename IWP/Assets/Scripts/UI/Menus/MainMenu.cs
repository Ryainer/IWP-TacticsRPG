using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenu;
    public Text helptxt;
    
    public void onStartGamePressed()
    {
       
    }

    public void onOptionsButtonPressed()
    {
        if(!OptionsMenu.activeInHierarchy)
        {
            OptionsMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void ControlButtonPress()
    {

    }


    public void onQuitButton()
    {
        Application.Quit();
    }
}
