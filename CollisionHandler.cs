using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float delayLoad = 2f;
    [SerializeField] private AudioClip crashAudio;
    [SerializeField] private AudioClip successAudio;

    private AudioSource audioSource;

    private bool isControllable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable) return;
        switch (other.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "Finish":
                StartNextSceneSequence();
                break;
            case "Fuel":
                print("Fuel");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        // todo add sfx and particles
        audioSource.PlayOneShot(crashAudio);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadScene), 2f);
    }

    private void StartNextSceneSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextScene), delayLoad);
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}