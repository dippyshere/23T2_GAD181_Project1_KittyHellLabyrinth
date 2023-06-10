using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float extraTime = 3f; // Amount of time to add when picked up

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Timer timer = other.GetComponent<Timer>(); // Assuming you have a Timer script attached to the player

            if (timer != null)
            {
                timer.AddTime(extraTime); // Call a method in your Timer script to add the extra time
            }

            Destroy(gameObject); // Destroy the pickup object after it's been collected
        }
    }
}