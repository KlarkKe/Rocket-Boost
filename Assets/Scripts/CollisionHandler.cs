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
            case "Fuel":
                Debug.Log("I got a fuel");
                break;
            case "Finish":
                Debug.Log("I finished");
                SceneManager.LoadScene(0);
                break;
            default:
                break;
        }
    }
}
