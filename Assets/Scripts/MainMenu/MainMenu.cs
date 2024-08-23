using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public void PlayGame()
    {
        StartCoroutine(LoadLevel("Jogo"));
    }

    public void QuitGame()
    {
        Application.Quit();
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
