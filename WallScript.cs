using UnityEngine;

public class WallScript : MonoBehaviour
{
    SpriteRenderer sr;
    Color forceColor;
    float red = 0;
    float force = 0; //initializes force to zero
    float speed;
    //int fTicks = 0; // allow force to be sustained for 1 tick before resetting
    const float MAXFORCE = 10000; // max force we will scale to when doing colors

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (force != 0f && fTicks <= 100) {
            fTicks++;
        } else if (fTicks >= 100) {
            fTicks = 0;
            force = 0f;
            sr.color = Color.white;
        } force over certain ticks*/
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision!");
     //   Debug.Log(collision.gameObject.GetComponent<WaterScript>().getForce());
        float deltaTime = 0.017f; // should be framerate
        speed = collision.gameObject.GetComponent<WaterScript>().rb.linearVelocity.magnitude;
        //Debug.Log(speed);
        force += 2*(collision.gameObject.GetComponent<WaterScript>().rb.mass * speed)/(deltaTime);

        // if max force > 2000, we will map as if F = 2000
        if (force >= MAXFORCE) {
            force = MAXFORCE;
        }
        
        red = map(force, 0, MAXFORCE, 0, 255); // maps the amount of force to the red function

        sr.color = new Color(1,1-red/255,1-red/255); // color will deviate from white becoming increasingly red as red (variable) gets larger
        Debug.Log(red);
    }
    
    // map function from Arduino.cc
    float map(float input, float iMin, float iMax, float oMin, float oMax) {
        return (input - iMin)*(oMax-oMin) / ((iMax - iMin) + oMin);
    }
}
