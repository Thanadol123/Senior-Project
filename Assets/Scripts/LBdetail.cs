using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LBdetail : MonoBehaviour
{
    public static LBdetail Instance;
    public TextMeshProUGUI lbdtText;
    void Start()
    {
        Instance = this;
        
    }

    public void LeaderBoardDetail(string lbdt)
    {
        lbdtText.text = lbdt.ToString();
    }
    
}
