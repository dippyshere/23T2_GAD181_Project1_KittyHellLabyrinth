using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float totalTime = 10f; // Total time in seconds

    private float currentTime;
    private bool isTimerRunning;

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