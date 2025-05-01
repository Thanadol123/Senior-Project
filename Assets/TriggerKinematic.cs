using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKinematic : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(EnableRagdoll());
    }
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("other.name == " + other.name);
        if (other.name != "")
        {
            string[] g = other.name.Split('_');
            if (g.Length <= 0) return;
            //Debug.Log("EffectMesh == " + g[1]);
            //name = g[1];
            //if (g[1] == "EffectMesh")
            //{
            //   // other.tag = "Floor";
            //    //Debug.Log("Yes ");
            //    //if (other.tag == "Floor") { StartCoroutine(EnableRagdoll()); }
            //    StartCoroutine(EnableRagdoll());
            //}
        }

    }
    private void OnTriggerStay(Collider other)
    {
        // Debug.Log("other.name == " + other.name);
        if (other.name != "")
        {
            string[] g = other.name.Split('_');
            if (g.Length <= 0) return;

            //if (g[1] == "EffectMesh")
            //{
            //    other.isTrigger = true;
            //    StartCoroutine(EnableRagdoll());
            //}
        }

    }
    IEnumerator EnableRagdoll()
    {

        yield return new WaitForSeconds(2);
        rb.isKinematic = true;

    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("other.name == " + other.name);
        if (other.name != "")
        {
            string[] g = other.name.Split('_');
            //Debug.Log("EffectMesh == " + g[1]);
            //name = g[1];
            //    if (g[1] == "EffectMesh")
            //    {
            //        other.isTrigger = false;
            //        DisableRagdoll();
            //    }
            //}

        }

        void DisableRagdoll()
        {
            rb.isKinematic = false;

        }


    }
}
