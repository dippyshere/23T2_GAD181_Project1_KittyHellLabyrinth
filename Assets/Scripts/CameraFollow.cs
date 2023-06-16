using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    public bool followYAxis = true;
    public Vector3 offset = new(4f, 4.5f, -50f);
    public Vector3 minTarget = new(3.5f, 2f, -50f);
    public Vector3 maxTarget = new(190f, 2f, -50f);
    
    private float flipX = 1f;
    //private bool canMove = true;
    public Vector3 targetPosition = new Vector3(3.5f, 2f, -50f);

    private void Update()
    {
        // this took like 6 hours D:
        if (target.position.x + offset.x <= minTarget.x || target.position.x + (offset.x * flipX) <= minTarget.x)
        {
            //canMove = false;
            targetPosition.x = minTarget.x;
        }
        else if (target.position.x + offset.x >= maxTarget.x || target.position.x + (offset.x * flipX) >= maxTarget.x)
        {
            //canMove = false;
            targetPosition.x = maxTarget.x;
        }
        else
        {
            //canMove = true;
            targetPosition.x = target.position.x + offset.x;
        }
        if (followYAxis)
        {
            if (target.position.y + offset.y <= minTarget.y)
            {
                targetPosition.y = minTarget.y;
            }
            else if (target.position.y + offset.y >= maxTarget.y)
            {
                targetPosition.y = maxTarget.y;
            }
            else
            {
                targetPosition.y = target.position.y + offset.y;
            }
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
    public IEnumerator ShakeCamera(float intensity, float duration)
    {
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float x = originalPosition.x + Random.Range(-intensity, intensity);
            float y = originalPosition.y + Random.Range(-intensity, intensity);
            transform.position = new Vector3(x, y, originalPosition.z);
            // Gradually decrease the intensity of the shake over time
            float t = Mathf.Clamp01(elapsedTime / duration);
            intensity = Mathf.Lerp(intensity, 0f, t/4);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }

}
