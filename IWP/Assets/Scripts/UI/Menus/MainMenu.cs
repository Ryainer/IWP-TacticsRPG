using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenu;
    
    public void onStartGamePressed()
    {
       
    }

    public void onOptionsButtonPressed()
    {
        if(!OptionsMenu.activeInHierarchy)
        {
            OptionsMenu.SetActive(true);
        }
    }

    public void onQuitButton()
    {
        Application.Quit();
    }
}
