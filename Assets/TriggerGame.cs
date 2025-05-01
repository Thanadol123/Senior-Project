using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGame : MonoBehaviour
{
    public GameObject TargetLine;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RingSpark")
        {
            TargetLine.SetActive(true);
        }

    }
 
}
