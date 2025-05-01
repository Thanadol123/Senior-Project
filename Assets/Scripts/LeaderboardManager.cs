using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;
    public static int player;
    public static string[] SavePlayer;
    public static string[] SaveScorPlayer;
    public static string[] SaveTimePlayer;
    public static string[] SaveTimeDisplay;
    // คลาส Player สำหรับเก็บข้อมูลผู้เล่น
    public class Player
    {
        public string Name { get; set; }
        public string Score { get; set; }
        public string Time { get; set; } // เก็บเวลาในการทำคะแนน

        public Player(string name, string score, string time)
        {
            Name = name;
            Score = score;
            Time = time;
        }
    }

    // รายการผู้เล่นทั้งหมด
    private List<Player> players = new List<Player>();

    // ฟังก์ชันสำหรับเพิ่มผู้เล่นพร้อมคะแนนและเวลา
    public void AddPlayer(string name, string score, string time)
    {
        players.Add(new Player(name, score, time));
    }

    // ฟังก์ชันสำหรับเรียงลำดับผู้เล่นจากคะแนนสูงสุดเวลาน้อย ไปยังคะแนนต่ำสุดเวลามาก
    public List<Player> GetSortedLeaderboard()
    {
        return players
            .OrderByDescending(p => p.Score) // เรียงคะแนนจากมากไปน้อย
            .ThenBy(p => p.Time)             // หากคะแนนเท่ากัน ให้เรียงเวลาจากน้อยไปมาก
            .ToList();
    }
    // ฟังก์ชันแสดงผลอันดับผู้เล่นทั้งหมด
    public void DisplayLeaderboard()
    {
        Debug.Log("----- Leaderboard -----");

        var sortedPlayers = GetSortedLeaderboard();
        // Debug.Log("sortedPlayers.Count" + sortedPlayers.Count);
        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            var player = sortedPlayers[i];

            // Debug.Log($"{i + 1}. {player.Name} - Score: {player.Score}, Time: {player.Time}");

            string lbdtData = "No." + i + 1 + " Name:" + player.Name.ToString() + " Score:" + player.Score.ToString() + " Time:" + PlayerPrefs.GetString("SaveTimeDisplay[" + (i + 1) + "]").ToString();
            // PlayerPrefs.GetString("lData", lbdtData);
            if (player.Name != "")
                DisplaLeaderBoard.Instance.UpdatPlayer(lbdtData);

        }
    }
    void Start()
    {
        LeaderboardManager leaderboardAdd = new LeaderboardManager();
        Instance = this;
        player = PlayerPrefs.GetInt("player");

        Debug.Log("player ==" + player);
        for (int i = 0; i < player; i++)
        {

            Debug.Log("player 2 ==" + PlayerPrefs.GetString("SavePlayer[" + (i + 1) + "]"));
            if (PlayerPrefs.GetString("SavePlayer[" + (i + 1) + "]") != "")
            {
                Debug.Log("player 3 ==" + PlayerPrefs.GetString("SaveScorPlayer[" + (i + 1) + "]"));
                Debug.Log("player 4 ==" + PlayerPrefs.GetString("SaveTimeDisplay[" + (i + 1) + "]"));
                // leaderboardAdd.AddPlayer(PlayerPrefs.GetString("SavePlayer[" + (i + 1) + "]"), int.Parse(PlayerPrefs.GetString("SaveScorPlayer[" + (i + 1) + "]")), float.Parse(PlayerPrefs.GetString("SaveTimePlayer[" + (i + 1) + "]")));

            }
        }
       // leaderboardAdd.AddPlayer("Bob", "1200", "50.2f");
        //leaderboardAdd.AddPlayer("Charlie", "1800", "40.0f");
       // leaderboardAdd.AddPlayer("Dave", "1800", "35.7f"); // คะแนนสูงสุดแต่เวลาน้อยสุด
       // leaderboardAdd.AddPlayer("Eve", "800", "55.3f");   // คะแนนต่ำสุดและเวลามากสุด

        leaderboardAdd.DisplayLeaderboard();
    }
    public void Startleaderboard()
    {
        player = PlayerPrefs.GetInt("player");

        LeaderboardManager leaderboardAdd = new LeaderboardManager();
        Debug.Log("player ==" + player);
        for (int i = 0; i < player; i++)
        {

            Debug.Log("player 2 ==" + PlayerPrefs.GetString("SavePlayer[" + (i + 1) + "]"));
            if (PlayerPrefs.GetString("SavePlayer[" + (i + 1) + "]") != "")
            {
                Debug.Log("player 3 ==" + PlayerPrefs.GetString("SaveScorPlayer[" + (i + 1) + "]"));
                Debug.Log("player 4 ==" + PlayerPrefs.GetString("SaveTimePlayer[" + (i + 1) + "]"));
                leaderboardAdd.AddPlayer(PlayerPrefs.GetString("SavePlayer[" + (i + 1) + "]"), PlayerPrefs.GetString("SaveScorPlayer[" + (i + 1) + "]"), PlayerPrefs.GetString("SaveTimePlayer[" + (i + 1) + "]"));

            }
        }

        leaderboardAdd.DisplayLeaderboard();


    }
}