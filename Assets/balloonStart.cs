using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonStart : MonoBehaviour
{
    public static balloonStart Instance;
    public Transform Spawns;
    public GameObject balloon;
    public bool StopBalloon;
    public float LoopTime;
    public float StartTime;
    // public AudioSource audioSource;
    // public AudioClip clip;
    public void Start()
    {
        Instance = this;
        StopBalloon = false;
       
    }
    public void ReMach()
    {
        StopBalloon = true;

        StartCoroutine(BalloonLoop());

        StartCoroutine(BalloonStart());

  
    }
    // Update is called once per frame
    public void StatusUpdate()
    {
     
        StopBalloon = false;

        Debug.Log("Woooow=" + StopBalloon);
    }
    IEnumerator BalloonStart()
    {
        if (StopBalloon == true)
        {
            yield return new WaitForSeconds(StartTime);
            Instantiate(balloon, Spawns.transform.position, Spawns.transform.rotation);
           
        }
        
    }
    IEnumerator BalloonLoop()
    {
        if (StopBalloon == true)
        {
            yield return new WaitForSeconds(LoopTime);
            Instantiate(balloon, Spawns.transform.position, Spawns.transform.rotation);
            StartCoroutine(BalloonLoop());
        }
       
    }
}
