using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Timer timer;

    private bool isActive = true;

    private void Start()
    {
        timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActive)
        {
            if (timer != null)
            {
                timer.currentTime = timer.totalTime;

                timer.StopAllCoroutines();
                timer.StartCoroutine(timer.TimerAudio());
                StartCoroutine(Respawn());
            }
        }
    }

    private IEnumerator Respawn()
    {
        isActive = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(Random.Range(11, 20));

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        isActive = true;
    }
}