using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public Animator sceneTransition;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int lvlIndex)
    {
        sceneTransition.SetTrigger("Transition");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(lvlIndex);
    }
}
