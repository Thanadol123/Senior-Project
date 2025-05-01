using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Video; // Include Video namespace

public class TutorialController : MonoBehaviour
{
    public Toggle Checkbottom;
    public Toggle Checkbottom2;
    public Toggle Checkbottom3;
    public AudioClip taskCompletedSound; // Reference to the sound clip
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component

    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Make sure the video is not playing initially
        videoPlayer.Stop();

        // Subscribe to the onValueChanged events of both toggles
        Checkbottom.onValueChanged.AddListener(OnToggleChanged);
        Checkbottom2.onValueChanged.AddListener(OnToggleChanged);
    }

    // This method will be called every time one of the toggles' values changes
    private void OnToggleChanged(bool isOn)
    {
        // Check if both toggles are enabled (isOn == true)
        if (Checkbottom.isOn && Checkbottom2.isOn)
        {
            PlayVideo(); // Play the video when both toggles are on
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check for collision with a wall
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Task 1 completed: Collided with a wall!");
            Checkbottom.isOn = true; // Enable the first toggle
        }
        // Check for the clipboard entering the tray
        else if (other.gameObject.CompareTag("Clipboard"))
        {
            Debug.Log("Task 2 completed: Pick up the clipboard");
            Checkbottom2.isOn = true; // Enable the second toggle
        }
    }

    // Method to play the video
    void PlayVideo()
    {
        if (videoPlayer != null)
        {
            Debug.Log("Both toggles are enabled. Playing video.");
            videoPlayer.Play(); // Play the video
            StartCoroutine(WaitAndEnableToggle(72f));
        }
        
    }
    IEnumerator WaitAndEnableToggle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // Wait for the specified duration
        Checkbottom3.isOn = true; // Enable Checkbottom3 after the wait time
        Debug.Log("72 seconds have passed. Enabling Checkbottom3.");
    }
}