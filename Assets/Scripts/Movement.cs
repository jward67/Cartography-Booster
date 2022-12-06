using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles; 
    [SerializeField] ParticleSystem rightEngineParticles;

    Rigidbody rb;
    AudioSource audioSource; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    public ParticleSystem MainEngineParticles() { return mainEngineParticles; }

    public ParticleSystem LeftEngineParticles() { return leftEngineParticles; }

    public ParticleSystem RightEngineParticles() { return rightEngineParticles; }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }


    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }
    
    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void RotateLeft()
    {
        if (!leftEngineParticles.isPlaying)
        {
            leftEngineParticles.Play();
        }
        ApplyRotation(rotationThrust);
    }

    void RotateRight()
    {
        if (!rightEngineParticles.isPlaying)
        {
            rightEngineParticles.Play();
        }
        ApplyRotation(-rotationThrust);
    }

    void StopRotating()
    {
        leftEngineParticles.Stop();
        rightEngineParticles.Stop();
    }


    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   // Freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  // Unfreezing rotation so physics system can take over.
    }
}
