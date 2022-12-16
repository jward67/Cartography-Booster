using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip success, crash;

    [SerializeField] ParticleSystem successParticles, crashParticles;

    Movement movement;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start() 
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable; // toggle collision
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisable) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                //Debug.Log("Touching Start Pad");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
/*            case "Fuel":
                Debug.Log("Refueling Rocket");
                break; */
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        StopRocketBoosters();
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        movement.enabled = false;

        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        StopRocketBoosters();
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        movement.enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StopRocketBoosters()
    {
        movement.MainEngineParticles().Stop();
        movement.LeftEngineParticles().Stop();
        movement.RightEngineParticles().Stop();
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
