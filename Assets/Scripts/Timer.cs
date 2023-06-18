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

    private void Start()
    {
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
                break;
            case 1:
                timerImage.sprite = timerSprites[1];
                break;
            case 2:
                timerImage.sprite = timerSprites[2];
                break;
            case 3:
                timerImage.sprite = timerSprites[3];
                break;
            case 4:
                timerImage.sprite = timerSprites[4];
                break;
            case 5:
                timerImage.sprite = timerSprites[5];
                break;
            case 6:
                timerImage.sprite = timerSprites[6];
                break;
            case 7:
                timerImage.sprite = timerSprites[7];
                break;
            case 8:
                timerImage.sprite = timerSprites[8];
                break;
            case 9:
                timerImage.sprite = timerSprites[9];
                break;
            case 10:
                timerImage.sprite = timerSprites[10];
                break;
        }
    }

    private void StartTimer()
    {
        currentTime = totalTime;
        isTimerRunning = true;
    }

    private void StopTimer()
    {
        isTimerRunning = false;
    }

    private void GameOver()
    {
        // Implement game over logic here
        Debug.Log("Game Over");
        // You can restart the game, display a game over screen, or perform any other desired actions
    }

    public void AddTime(float extraTime)
    {
        currentTime += extraTime;
    }
}