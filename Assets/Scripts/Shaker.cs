using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public float shakeAmount = 10f;

    private float shakeTime = 0.0f;
    private Vector3 initialPosition;
    private bool isShaking = false;

    public Timer timer;

    private void Start()
    {
        timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        initialPosition = this.transform.position;
    }

    private void Update()
    {
        if (Mathf.Ceil(timer.currentTime) <= 3 && !isShaking)
        {
            ShakeForTime(3f);
        }

        if (shakeTime > 0)
        {
            this.transform.position = Random.insideUnitSphere * shakeAmount + initialPosition;
            shakeTime -= Time.deltaTime;
        }
        else if (isShaking)
        {
            isShaking = false;
            shakeTime = 0.0f;
            this.transform.position = initialPosition;
        }
    }

    public void ShakeForTime(float time)
    {
        shakeTime = time;
        isShaking = true;
    }
}
