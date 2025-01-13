using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;

    Rigidbody rb;
    AudioSource audio_s;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio_s = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable(); //����� ���������� ������ OnEnable, �� �� ��� �� ������� ������ � ��� ����� ����
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime); //Vector3.up ���� ����� ���� � ���������� ��� (0, 1, 0) //Relative, � �� ������� ���� ��������� ���� ����������� ������ �� ��������� ���� ��� ��� � ����� ��� ��������� � �.�.
            if (!audio_s.isPlaying)
            {    
                audio_s.Play();
            }
        }
        else
        {
            audio_s.Stop();
        }
    }
}
