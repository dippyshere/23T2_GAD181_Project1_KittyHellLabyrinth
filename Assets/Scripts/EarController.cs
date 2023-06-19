using UnityEngine;

public class EarController : MonoBehaviour
{
    public float upwardForce = 5f;
    public float forwardForce = -1f;
    public float torqueForce = 2f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Apply an initial upward force with a slight forward component
        Vector2 initialForce = new Vector2(forwardForce, upwardForce);
        rb.AddForce(initialForce, ForceMode2D.Impulse);

        // Apply torque force to make the sprite rotate
        rb.AddTorque(torqueForce, ForceMode2D.Impulse);

        // Start the coroutine to stop the sprite after a delay
        StartCoroutine(StopSpriteAfterDelay());
    }

    private System.Collections.IEnumerator StopSpriteAfterDelay()
    {
        yield return new WaitForSeconds(Random.Range(0.9f, 1.5f));
        rb.simulated = false;
    }
}
