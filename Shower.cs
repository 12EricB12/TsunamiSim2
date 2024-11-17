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
    public Vector2 init_speed = new Vector2(1.0f, 0.0f);
    public float spawn_rate = 0.001f;
    private bool runScript = false;
    private bool instantized = false;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
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
            for (int i = 0; i < 102; i++)
            {
                // Spawn particles at a constant rate
                time += Time.deltaTime;

                // Create a new particle at the current position of the object
                GameObject new_particle = Instantiate(Base_Particle, Vector2.zero, Quaternion.identity);

                // update the particle's position
                new_particle.GetComponent<Particle>().pos = Vector2.zero;
                new_particle.GetComponent<Particle>().previous_pos = transform.position;
                new_particle.GetComponent<Particle>().visual_pos = transform.position;
                new_particle.GetComponent<Particle>().vel = new Vector2(UnityEngine.Random.Range(-1, 1), 0);

                new_particle.AddComponent<Rigidbody2D>();

                // Set as child of the Simulation object
                new_particle.transform.parent = Simulation.transform;

                // Reset time
                time = 0.0f;
            }

            instantized = true;
        }

        // Limit the number of particles
        foreach (Transform child in Simulation.GetComponentsInChildren<Transform>())
        {
            // Spawn particles at a constant rate
            time += Time.deltaTime;

            // Create a new particle at the current position of the object
            GameObject curr_particle = child.gameObject;

            if (curr_particle.GetComponent<Particle>() == null) continue;

            // update the particle's position
            curr_particle.GetComponent<Particle>().pos = curr_particle.transform.position;
            curr_particle.GetComponent<Particle>().previous_pos = curr_particle.transform.position;
            curr_particle.GetComponent<Particle>().visual_pos = curr_particle.transform.position;
            curr_particle.GetComponent<Particle>().vel = init_speed;

            // Set as child of the Simulation object
            curr_particle.transform.parent = Simulation.transform;

            // Reset time
            time = 0.0f;
        }
    }

    public void Execute() {
        runScript = true;
    }
}