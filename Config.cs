using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    // Simulation parameters
    public static int N = 100; // Number of particles
    public static float SIM_W = 11f;  // Simulation space width
    public static float BOTTOM = -5f;  // Simulation space ground
    public static float DAM = -0.3f;  // Position of the dam, simulation space is between -0.5 and 0.5
    public static int DAM_BREAK = 200; // Number of frames before the dam breaks
    public static float DT = 10f;  // Time step
    public static float WALL_POS = 0.15f; // Position adjustment for particles that are too close to the walls

    // Physics parameters
    public static float G = 0.02f * 0.5f;  // Acceleration of gravity
    public static float SPACING = 1.0f;  // Spacing between particles, used to calculate pressure
    public static float K = SPACING / 1000.0f;  // Pressure factor
    public static float K_NEAR = K * 0.4f;  // Near pressure factor, pressure when particles are close to each other
    // Default density, will be compared to local density to calculate pressure
    public static float REST_DENSITY = 0.4f;
    // Neighbour radius, if the distance between two particles is less than R, they are neighbours
    public static float R = SPACING * 0.125f;
    public static float SIGMA = 0.4f;  // Viscosity factor
    public static float MAX_VEL = 50f;  // Maximum velocity of particles, used to avoid instability
    // Wall constraints factor, how much the particle is pushed away from the simulation walls
    public static float WALL_DAMP = 0.0f;
    public static float VEL_DAMP = 0.1f;  // Velocity reduction factor when particles are going above MAX_VEL
}