using UnityEngine;

public class GlassCollision : MonoBehaviour
{
    public AudioClip GlassDropSound; // Assign the glass drop sound in the Unity Editor

    private void OnCollisionEnter(Collision collision)
    {
        AudioSource.PlayClipAtPoint(GlassDropSound, transform.position);
    }
}