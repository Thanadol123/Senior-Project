using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class ParticleTrigger : MonoBehaviour
{
    public GameObject WaterleakParticlePrefab;
    public Transform Droppoint2;
    public Renderer rend;
    public float _Fill;
    public TMP_Text ro1;
    public Image img;
    public Toggle Checkbottom;
    public Toggle Checkbottom1;
    public AudioClip DingSound;
    public GameObject WrongSe1;
    public RawImage RawImageToShow2;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private List<GameObject> instantiatedParticles = new List<GameObject>();

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision occurred with the bottom surface
        if (collision.gameObject.CompareTag("BottomSurface"))
        {
            GameObject particle = Instantiate(WaterleakParticlePrefab, Droppoint2.position, Droppoint2.rotation);
            instantiatedParticles.Add(particle);

            // Start a coroutine to wait and then destroy the particle
            StartCoroutine(WaitAndDestroyParticle(particle));
        }

    }

    private IEnumerator WaitAndDestroyParticle(GameObject particle)
    {
        yield return new WaitForSeconds(0.1f); // Adjust the waiting time as needed
        Destroy(particle);
        instantiatedParticles.Remove(particle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Particle")
        {
            if(Checkbottom1.GetComponent<Toggle>().isOn == true)
            {


                float currentFill = rend.material.GetFloat("_Fill");

                if (currentFill < 0.1f)
                {
                    currentFill += 0.03f;
                    rend.material.SetFloat("_Fill", currentFill);
                    Destroy(other.gameObject);
                }
                RawImageToShow2.enabled = true;
                Checkbottom.GetComponent<Toggle>().isOn = true;
                audioSource.PlayOneShot(DingSound);
            }
            else
            {
                StartCoroutine(ro5sec());

            }
        }

    }
    IEnumerator ro5sec()
    {
        WrongSe1.SetActive(true);
        yield return new WaitForSeconds(6f);
        WrongSe1.SetActive(false);
    }

}
