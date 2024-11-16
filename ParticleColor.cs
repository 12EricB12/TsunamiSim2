using UnityEngine;

public class ParticleColor : MonoBehaviour
{
    private Rigidbody2D rb;
    SpriteRenderer sr;

    // choose higher scale factor for lower velocities so full gradient is shown
    public int scaleFactor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // colour of particle becomes more red at a higher velocity
        Vector2 vel = rb.linearVelocity;
        float scaledVel = vel.magnitude * scaleFactor / 256;
        sr.color = new Color(scaledVel, 0f, 1 - scaledVel, 1);
    }

}
