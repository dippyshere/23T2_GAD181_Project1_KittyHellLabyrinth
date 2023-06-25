using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float totalTime = 10f; // Total time in seconds

    [HideInInspector] public float currentTime;
    private bool isTimerRunning;

    public Image timerImage;
    public List<Sprite> timerSprites;

    public PlayerController playerController;

    private AudioSource audioSource;
    [SerializeField] AudioClip tickSound;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        // Start the timer
        StartTimer();
    }

    private void Update()
    {
        // Update the timer only if it's running
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;

            // Check if the timer has reached zero
            if (currentTime <= 0f)
            {
                // Stop the timer
                StopTimer();

                // Trigger game over or any other desired actions
                GameOver();
            }
        }

        switch (Mathf.Ceil(currentTime))
        {
            case 0:
                timerImage.sprite = timerSprites[0];
                audioSource.volume = 1f;
                break;
            case 1:
                timerImage.sprite = timerSprites[1];
                audioSource.volume = 1f;
                break;
            case 2:
                timerImage.sprite = timerSprites[2];
                audioSource.volume = 0.9f;
                break;
            case 3:
                timerImage.sprite = timerSprites[3];
                audioSource.volume = 0.8f;
                break;
            case 4:
                timerImage.sprite = timerSprites[4];
                audioSource.volume = 0.7f;
                break;
            case 5:
                timerImage.sprite = timerSprites[5];
                audioSource.volume = 0.6f;
                break;
            case 6:
                timerImage.sprite = timerSprites[6];
                audioSource.volume = 0.5f;
                break;
            case 7:
                timerImage.sprite = timerSprites[7];
                audioSource.volume = 0.4f;
                break;
            case 8:
                timerImage.sprite = timerSprites[8];
                audioSource.volume = 0.3f;
                break;
            case 9:
                timerImage.sprite = timerSprites[9];
                audioSource.volume = 0.2f;
                break;
            case 10:
                timerImage.sprite = timerSprites[10];
                audioSource.volume = 0.1f;
                break;
        }
    }

    private void StartTimer()
    {
        currentTime = totalTime;
        isTimerRunning = true;
        StartCoroutine(TimerAudio());
    }

    private void StopTimer()
    {
        isTimerRunning = false;
    }

    private void GameOver()
    {
        //Debug.Log("Game Over");

        playerController.isGameOver = true;
        Time.timeScale = 0f;
    }

    public IEnumerator TimerAudio()
    {
        while (!playerController.isGameOver)
        {
            yield return new WaitForSeconds(1f);
            audioSource.PlayOneShot(tickSound);
        }
    }
}