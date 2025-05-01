using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float Countdown;
    void Start()
    {
        audioSource.GetComponent<AudioSource>();
        StartCoroutine(PlaySound());
    }

    // Update is called once per frame
    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(Countdown);
        audioSource.PlayOneShot(clip,3.0f);
        yield return new WaitForSeconds(2.85f);
        audioSource.Stop();
    }
}
