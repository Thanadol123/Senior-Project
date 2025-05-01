using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiquidInteraction : MonoBehaviour
{
    public GameObject WaterleakParticlePrefab;
    public Transform Droppoint;
    public Renderer rend;
    public float _Fill;
    public AudioClip waterPourSound; // Assign your water pour sound in the Unity Editor
    public Toggle Checkbottom;
    public AudioClip DingSound;

    private List<GameObject> instantiatedParticles = new List<GameObject>();
    private AudioSource audioSource;
    private bool soundPlayed = false;
    private bool checkedCheckbottom = false; // Flag to track if Checkbottom has been checked

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckRotationAngles();
    }

    private void CheckRotationAngles()
    {
        float angle = transform.rotation.eulerAngles.z;

        if ((angle >= 100f && angle <= 270f) || (angle <= -100f && angle >= -270f))
        {
            // If sound not played yet, play the water pour sound
            if (!soundPlayed)
            {
                PlayWaterPourSound();
                soundPlayed = true;
            }

            // Instantiate the particle effect and store the reference
            GameObject particle = Instantiate(WaterleakParticlePrefab, Droppoint.position, Droppoint.rotation);
            instantiatedParticles.Add(particle);

            // Start a coroutine to wait and then destroy the particle
            StartCoroutine(WaitAndDestroyParticle(particle));
        }
        else
        {
            // Reset the flag when pouring stops
            if (soundPlayed)
            {
                StopWaterPourSound();
                soundPlayed = false;
            }
        }
    }

    private IEnumerator WaitAndDestroyParticle(GameObject particle)
    {
        yield return new WaitForSeconds(0.1f); // Adjust the waiting time as needed
        Destroy(particle);
        instantiatedParticles.Remove(particle);
    }

    private void PlayWaterPourSound()
    {
        if (waterPourSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(waterPourSound);
        }
    }

    private void StopWaterPourSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Particle")
        {
            float currentFill = rend.material.GetFloat("_Fill");
            if (currentFill < 0.04f)
            {
                currentFill += 0.001f;
                rend.material.SetFloat("_Fill", currentFill);
                Destroy(other.gameObject);
            }

            else if (!checkedCheckbottom) // Check if Checkbottom has not been checked yet
            {
                Checkbottom.GetComponent<Toggle>().isOn = true;
                checkedCheckbottom = true; // Set the flag to true to indicate that Checkbottom has been checked
                audioSource.PlayOneShot(DingSound); // Play the sound after setting Checkbottom
            }
        }
    }
}