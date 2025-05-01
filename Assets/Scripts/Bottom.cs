using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Particle")) // Check if the colliding object is a particle
        {
            Destroy(other.gameObject); // Destroy the particle
        }
    }
}