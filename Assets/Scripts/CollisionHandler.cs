using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("One Message");
                break;
            case "Finish":
                LoadNextLevel();
                break;
            default:
                ReloadLevel();
                break;
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
}
