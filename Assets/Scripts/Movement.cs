using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem rightBooster;
    [SerializeField] ParticleSystem leftBooster;

    Rigidbody rb;
    AudioSource audio_s;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio_s = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable(); //когда вызывается колбек OnEnable, то мы как бы говорим включи и наш инпут экшн
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime); //Vector3.up тоже самое если б аргументом был (0, 1, 0) //Relative, а не обычный форс потомучто сила добавляется исходя из локальных осей что нам и нужно при поворотах и т.п.
        if (!audio_s.isPlaying)
        {
            audio_s.PlayOneShot(mainEngine);
        }

        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    private void StopThrusting()
    {
        audio_s.Stop();
        mainBooster.Stop();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            RotateRight();
        }
        else if (rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(rotationStrength);
        if (!rightBooster.isPlaying)
        {
            leftBooster.Stop();
            rightBooster.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationStrength);
        if (!leftBooster.isPlaying)
        {
            rightBooster.Stop();
            leftBooster.Play();
        }
    }

    private void StopRotating()
    {
        leftBooster.Stop();
        rightBooster.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}