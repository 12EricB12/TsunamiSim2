using UnityEngine;

public class ParticleColor : MonoBehaviour
{
    private Rigidbody2D rb;
    SpriteRenderer sr;
    public int scaleFactor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocityX = 10;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = rb.linearVelocity;
        print(vel.magnitude);
        float scaledVel = vel.magnitude * scaleFactor / 256;
        sr.color = new Color(scaledVel, 0f, 1 - scaledVel, 1);
    }

}
