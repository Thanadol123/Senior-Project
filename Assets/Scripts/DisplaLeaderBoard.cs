using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplaLeaderBoard : MonoBehaviour
{
    public static DisplaLeaderBoard Instance;
    [SerializeField]
    public GameObject GenLeaderBoard;
    [SerializeField]
    public Transform ContantLeaderBoard;

   // public TextMeshProUGUI textData;
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    public void UpdatPlayer(string gData)
    {
        if (gData != "")
        {
                Debug.Log("Get Player -----" + gData);
                // textData.text = getDt.ToString();
                 GameObject varEntry = (GameObject)Instantiate(GenLeaderBoard, ContantLeaderBoard);
                 varEntry.GetComponent<LBdetail>().LeaderBoardDetail(gData);
                // StartCoroutine(SendData());
            
        }
    }
}
