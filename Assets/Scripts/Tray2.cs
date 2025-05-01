using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tray2 : MonoBehaviour
{
    public GameObject WrongSe1;  // UI element to show for wrong tag
    public Toggle Toggle1;
    public Toggle[] Alltoggles;
    public AudioSource audioSource;  // Audio source component
    public AudioClip DingSound;  // Sound to play

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Mixtures"))
        {
            Alltoggles[0].isOn = true;
            audioSource.PlayOneShot(DingSound);  // Play sound
        }

        else if (collision.gameObject.CompareTag("Mixtures1"))
        {
            Debug.Log("IN");
            Alltoggles[1].isOn = true;  // Turn on the first toggle
            audioSource.PlayOneShot(DingSound);  // Play sound
        }
        // For any other tag (show wrong UI)
        else
        {
            StartCoroutine(ShowWrongCanvas());
        }
    }

    private IEnumerator ShowWrongCanvas()
    {
        WrongSe1.SetActive(true);  // Show wrong canvas
        yield return new WaitForSeconds(6);  // Wait for 6 seconds
        WrongSe1.SetActive(false);  // Hide wrong canvas
    }
}