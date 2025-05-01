using System.Collections;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
	public static PlayerStats instance;

	public static int Score;
	public int startScore = 0;

	public static int tiemPlay;
	public int startTiemPlay = 0;

	public static int quize;

	public static int player;
	public static string[] SavePlayer;
	public static string[] SaveScorPlayer;
	public static string[] SaveTimePlayer;
	public static string[] SaveTimeDisplay;

	public static int timeFloat;
	public static string timeDis;
	void Start()
	{
		 //PlayerPrefs.DeleteAll();
		instance = this;
		Score = startScore;
		tiemPlay = startTiemPlay;

	}
    public void GetPlayer()
    {
        player = PlayerPrefs.GetInt("player");
        PlayerPrefs.SetInt("player", player + 1);
        player = PlayerPrefs.GetInt("player");
        PlayerPrefs.SetString("SavePlayer[" + player + "]", "Player_" + player);
        return;

    }
    public void GetScorePlayer()
	{
		player = PlayerPrefs.GetInt("player",1);
		string newScore = PlayerPrefs.GetString("SaveScorPlayer[" + player + "]");
        if (newScore == "")
        {
			PlayerPrefs.SetString("SaveScorPlayer[" + player + "]", "1".ToString());

		}
		else
        {
			int UdateSore = int.Parse(newScore) + 1;
			PlayerPrefs.SetString("SaveScorPlayer[" + player + "]", UdateSore.ToString());
		}

	}
	public void GetTimePlayer()
	{
		timeFloat = PlayerPrefs.GetInt("timeFloat");
		timeDis = PlayerPrefs.GetString("timeDis");
		player = PlayerPrefs.GetInt("player");
		PlayerPrefs.SetString("SaveTimePlayer[" + player + "]", timeFloat.ToString());
		PlayerPrefs.SetString("SaveTimeDisplay[" + player + "]", timeDis.ToString());
		StartCoroutine(WaitForData());
	}
	
	
	IEnumerator WaitForData()
	{
		yield return new WaitForSeconds(1);
		PlayerPrefs.SetInt("timeFloat", 0);
		PlayerPrefs.SetString("timeDis", "");
	}
	

}