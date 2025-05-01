using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountTime : MonoBehaviour
{
    public static CountTime Instance;
    public float timeRemaining = 0;
    public bool timeIsRunning = true;
    public TMP_Text timeText;
    public string lastTime;
    public static int timeFloat;
    public static string timeDis;
    //??????????
    void Start()
    {
        Instance = this;
        timeIsRunning = true;
        PlayerPrefs.SetFloat("timeFloat", 0f);
        PlayerPrefs.SetInt("timeDis", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining += Time.deltaTime;
                PlayerPrefs.SetInt("timeFloat", (int)timeRemaining);
                timeFloat = PlayerPrefs.GetInt("timeFloat");
                DisplayTime(timeRemaining);
            }
        }
    }

    //???????
    void DisplayTime (float timeToDisplay)
    {
        
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt (timeToDisplay / 60);
        float seconds = Mathf.FloorToInt (timeToDisplay % 60);
        timeText.text = string.Format ("{0:00} : {1:00}",minutes , seconds);
        //lastTime = timeText.text.ToString();
        PlayerPrefs.SetString("timeDis", timeText.text.ToString());
        timeDis = PlayerPrefs.GetString("timeDis");
    }
}
