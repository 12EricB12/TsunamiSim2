using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using vector2 = UnityEngine.Vector2;

public class Shower : MonoBehaviour
{
    // Get the Simulation object
    public GameObject Simulation;
    // Get the Base_Particle object from Scene
    public GameObject Base_Particle;
    public GameObject TsunamiWall;
    public Vector2 init_speed = new Vector2(1.0f, 0.0f);
    public float spawn_rate = 0.001f;
    public float depth = 10;
    public int magnitude = 20;

    private float accel = 5f;
    private float speed;
    private float g = 9.81f;
    private int n;
    private bool runScript = true;
    private bool instantized = false;
    private bool stopped = false;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        n = magnitude * 5;
        TsunamiWall = GameObject.Find("TsunamiWall");
        Simulation = GameObject.Find("Simulation");
        Base_Particle = GameObject.Find("Base_Particle");
    }

    // Update is called once per frame
    void Update()
    {
        if (!runScript) {
            return;
        }

        if (!instantized && runScript)
        {
            for (int i = 0; i < n; i++)
            {
                // Spawn particles at a constant rate
                time += Time.deltaTime;

                // Create a new particle at the current position of the object
                GameObject new_particle = Instantiate(Base_Particle, new Vector2(-15 + (float)(0.1) * i, 0), Quaternion.identity);

                // update the particle's position
                new_particle.GetComponent<Particle>().pos = new_particle.transform.position;
                new_particle.GetComponent<Particle>().vel = init_speed;

                new_particle.AddComponent<Rigidbody2D>();

                // Set as child of the Simulation object
                new_particle.transform.parent = Simulation.transform;

                // Reset time
                time = 0.0f;
            }

            instantized = true;
        }

        StartBlock();

        // Limit the number of particles
        foreach (Transform child in Simulation.GetComponentsInChildren<Transform>())
        {
            Debug.Log("B");

            if (child.gameObject == Simulation || child.gameObject == Base_Particle) {
                continue;
            }

            // update the particle's position
            //child.GetComponent<Particle>().pos = transform.position;
            //child.GetComponent<Particle>().vel = init_speed;

            Debug.Log("Pos: " + child.transform.position);
            Debug.Log("Particle: " + child.GetComponent<Particle>().pos);

            if (stopped == true) {
                child.GetComponent<Particle>().vel = new Vector2(child.GetComponent<Particle>().vel.x + speed * Time.deltaTime, child.GetComponent<Particle>().vel.y + speed * Time.deltaTime);
                stopped = false;
            }

            child.transform.position = child.GetComponent<Particle>().pos;
            child.transform.parent = Simulation.transform;

            // Reset time
            time = 0.0f;
        }
    }

    public void Execute() {
        runScript = true;
    }

    // Begin "wave"
    // Mock tsunami movement due to the simplified model, uses a collider to give the initial push to the water molecules
    // Until they make landfall
    void StartBlock()
    {
        speed = (float)Math.Sqrt(g * depth * 0.01); // Equation for the speed of a tsunami
        BoxCollider2D collider = TsunamiWall.GetComponent<BoxCollider2D>();
        if (collider.transform.position.x >= 0) {
            stopped = true;
            return;
        }

        speed += accel * Time.deltaTime;
        collider.transform.position = new Vector2(collider.transform.position.x + speed * Time.deltaTime, collider.transform.position.y);
    }
}