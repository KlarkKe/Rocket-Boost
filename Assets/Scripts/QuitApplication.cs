using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    //[SerializeField] InputAction quit;

/*    private void OnEnable()
    {
        quit.Enable();
    }*/



    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("test test test");
            Application.Quit();
        }
    }
}
