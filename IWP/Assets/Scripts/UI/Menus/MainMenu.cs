using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenu;

    public void onStartGamePressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
