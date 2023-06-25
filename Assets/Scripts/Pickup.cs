using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Timer timer;

    private bool isActive = true;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickup;

    private void Start()
    {
        timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        audioSource = GetComponent<AudioSource>();
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
                audioSource.PlayOneShot(pickup);
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