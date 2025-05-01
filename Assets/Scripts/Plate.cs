using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    public TMP_Text ro1;
    public Image img;
    public Renderer rend;
    public Toggle Checkbottom;
    public Toggle Checkbottom1;
    public Toggle Checkbottom2;
    public Toggle[] Alltoggles;
    public AudioClip DingSound;
    public AudioClip Endtest;
    public GameObject WrongSe1;
    public Button CorrectAnswerButton;
    public Button WrongAnswerButton;
    public Button CorrectAnswerButton6;
    public Button WrongAnswerButton6;
    public test toggleChecker;  // Reference to the `test` script
    public RawImage RawImageToShow1;
    public RawImage RawImageToShow2;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ro1.text = "0.00g"; 
        RawImageToShow1.enabled = false;
        CorrectAnswerButton.onClick.AddListener(HandleCorrectAnswer);
        WrongAnswerButton.onClick.AddListener(HandleWrongAnswer);
        CorrectAnswerButton6.onClick.AddListener(HandleCorrectAnswer6);
        WrongAnswerButton6.onClick.AddListener(HandleWrongAnswer6);
    }

    private void HandleCorrectAnswer()
    {
        Alltoggles[2].GetComponent<Toggle>().isOn = true;  // Assuming the correct toggle is at index 2
        audioSource.PlayOneShot(DingSound);  // Optional: play a sound for correct answer
        toggleChecker.Cheak();  // Call the Cheak method to check if all toggles are on
    }
    private void HandleCorrectAnswer6()
    {
        Alltoggles[5].GetComponent<Toggle>().isOn = true;  // Assuming the correct toggle for question 6 is at index 5
        audioSource.PlayOneShot(DingSound);  // Optional: play a sound for correct answer
        toggleChecker.Cheak();  // Call the Cheak method to check if all toggles are on
    }

    private void HandleWrongAnswer6()
    {
        StartCoroutine(ro5sec());
    }
    private void HandleWrongAnswer()
    {
        StartCoroutine(ro5sec());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Beaker")
        {
            if (Alltoggles[3].GetComponent<Toggle>().isOn == true)
            {
                Debug.Log("IN");
                float currentFill = rend.material.GetFloat("_Fill");
                currentFill = currentFill * 733f;
                ro1.text = currentFill.ToString("F0") + ("g");
                img.color = Color.black;
                Alltoggles[4].GetComponent<Toggle>().isOn = true;
                RawImageToShow2.enabled = true;
                audioSource.PlayOneShot(DingSound);
                toggleChecker.Cheak();  // Call the Cheak method to check if all toggles are on
            }
            else
            {
                StartCoroutine(ro5sec());
            }
        }

        if (collision.gameObject.tag == "Beaker1")
        {
            if (collision.gameObject.tag == "Beaker1")
            {

                {
                    Debug.Log("IN");
                    float currentFill = rend.material.GetFloat("_Fill");
                    currentFill = currentFill * -100f;
                    ro1.text = currentFill.ToString("F0") + ("cm3");
                    img.color = Color.black;
                }

            }
        }

        if (collision.gameObject.tag == "Metal")
        {
            Debug.Log("IN");
            ro1.text = "78g";
            img.color = Color.black;
            RawImageToShow1.enabled = true;
            Alltoggles[0].GetComponent<Toggle>().isOn = true;
            audioSource.PlayOneShot(DingSound);
            toggleChecker.Cheak();  // Call the Cheak method to check if all toggles are on
        }
    }

    IEnumerator ro5sec()
    {
        WrongSe1.SetActive(true);
        yield return new WaitForSeconds(6f);
        WrongSe1.SetActive(false);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Beaker")
        {
            Debug.Log("OUT");
            ro1.text = "0.0g";
            img.color = Color.black;
        }

        if (collision.gameObject.tag == "Beaker1")
        {
            Debug.Log("OUT");
            ro1.text = "0.0g";
            img.color = Color.black;
        }

        if (collision.gameObject.tag == "Metal")
        {
            Debug.Log("OUT");
            ro1.text = "0.0g";
            img.color = Color.black;
        }
    }
}