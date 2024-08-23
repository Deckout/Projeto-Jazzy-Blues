using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    void Update()
    {
        if(SpawnNPC.npcOrdem == 15){
            StartCoroutine(LoadLevel("Resultados"));
    }
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
