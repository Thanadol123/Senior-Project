using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int totalscore;
    public GameObject panel1, panel2, panel3, panel4, panel5;
    public GameObject q1, q2, q3, q4, q5;
    public TMP_Text sumscore;
    public bool done;

    private HashSet<GameObject> scoredPanels = new HashSet<GameObject>();
    private HashSet<GameObject> answeredQuestions = new HashSet<GameObject>();

    void Start()
    {
        done = false;
        totalscore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPanelActivation(panel1);
        CheckPanelActivation(panel2);
        CheckPanelActivation(panel3);
        CheckPanelActivation(panel4);
        CheckPanelActivation(panel5);
        CheckQuestionAnswer(q1);
        CheckQuestionAnswer(q2);
        CheckQuestionAnswer(q3);
        CheckQuestionAnswer(q4);
        CheckQuestionAnswer(q5);

        sumscore.text = totalscore.ToString() + " / 5";
    }

    void CheckPanelActivation(GameObject panel)
    {
        if (panel.activeSelf && !scoredPanels.Contains(panel))
        {
            // Check if the panel should be considered for scoring
            if (AllPanelsAddressed())
            {
                IncreaseScore();
                scoredPanels.Add(panel);
            }
        }
    }

    void CheckQuestionAnswer(GameObject question)
    {
        if (question.activeSelf)
        {
            answeredQuestions.Add(question);
            if (done)
            {
                done = false;
            }
        }
    }

    void IncreaseScore()
    {
        totalscore = Mathf.Min(totalscore + 1, 5); // Ensure the score doesn't exceed the number of questions
        done = true;
    }

    bool AllPanelsAddressed()
    {
        // Ensure all questions are answered
        return answeredQuestions.Count == 5;
    }
}