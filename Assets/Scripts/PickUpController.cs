using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpController : MonoBehaviour
{
    [SerializeField] InputAction grabber;
    [SerializeField] float pickUpStrength = 100f;
    [SerializeField] Rigidbody targetRocket;

    Rigidbody rb;

    private void OnEnable()
    {
        grabber.Enable();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PickSubject")
        {
            rb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = (targetRocket.position - rb.position).normalized;

            if (grabber.IsPressed())
            {
                Debug.Log("I am here");
                rb.AddForce(direction * pickUpStrength * Time.fixedDeltaTime); //* Time.fixedDeltaTime
            }
        }
    }
}
