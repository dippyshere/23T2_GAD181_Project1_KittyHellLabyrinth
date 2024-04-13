using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMusicOnStart : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip inGameMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // Subscribe to the scene change event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the scene change event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (audioSource != null && audioSource.clip != null)
        {
            // Check if the new scene is the gameplay scene
            if (scene.name == "Main Game")
            {
                // Set the in-game music clip
                audioSource.clip = inGameMusic;

                // Play the in-game music
                audioSource.Play();
            }
            else
            {
                // Set the menu music clip
                audioSource.clip = menuMusic;

                // Play the menu music for any other scene
                audioSource.Play();
            }
        }
    }
}