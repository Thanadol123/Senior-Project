using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Beaker : MonoBehaviour
{
    public TMP_Text ro1;
    public Image img;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Beaker")
        {
            ro1.text = "100g";
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Beaker")
        {
            ro1.text = "0.0g";
            StartCoroutine(wait());
        }
    }

    public IEnumerator wait()
    {
        yield return new WaitForSeconds(4f);
        ro1.text = "0.0g";

    }
}