using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resultados : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    public Animator transition;
    public float transitionTime = 1f;
    public void PlayGame()

    {
        StartCoroutine(LoadLevel("MainMenu"));
    }

    IEnumerator LoadLevel(string levelName)
    {
        //play animation
        transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(transitionTime);

        //load scene
        SceneManager.LoadScene(levelName);
    }
}
