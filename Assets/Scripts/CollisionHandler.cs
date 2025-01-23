using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("One Message");
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartFinishSequence()
    {
        //todo add sfx and particles
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 2f);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }
}
