using UnityEngine;

public class WaterScript : MonoBehaviour {
    public Rigidbody2D rb;
    float speed;
    float force;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        rb.linearVelocity = new Vector2(6f,0f); // initialize velocity of particle
    }

    void Update() {
    }

    void onCollisionEnter2D(Collision2D collision) {
        Debug.Log("Water Collision!");
        float deltaTime = 0.017f; // should be framerate
        speed = rb.linearVelocity.magnitude;
        Debug.Log(speed);
        force = 2*(rb.mass * speed)/(deltaTime);
    }

    public float getForce() {
        return force;
    }


}
