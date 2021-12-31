using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static StroopItem;

public class StroopTest : MonoBehaviour
{
    public int num = 10;
    public List<StroopItem> testElements;
    private GameManager gm;

    public bool timing = false;

    public float wordTimer = 0;
    private TextMeshProUGUI tmpTimer;

    public struct FinalScore
    {
        public bool calculated;
        public float scorePercent;
        public float numCorrect;
        public float totalTime;
        public float bestTime;
        public float worstTime;
    }

    public FinalScore finalScore;

    private void Awake()
    {
        finalScore.calculated = false;
        //Create finite test items.
        for (int i = 0; i < num; i++)
        {
            StroopItem si = new StroopItem();
            si.GenerateRandom();
            testElements.Add(si);
        }

        //Get reference to timer diplay.
        tmpTimer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TextMeshProUGUI>();
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(!gm.endGame)
        {
            if (timing)
            {
                wordTimer += Time.deltaTime;
                tmpTimer.text = wordTimer.ToString("0.00");
            }
        } else if(!finalScore.calculated)
        {
            //calculate scores.
            int correct = 0;
            float totalTime = 0;
            float bestTime = 300; //if the player exceeds 300 seconds (5 minutes) it will be capped at 300.
            float worstTime = 0;

            foreach(StroopItem si in testElements)
            {
                if (si.correct)
                {
                    correct += 1;

                    if (si.viewTime < bestTime)
                        bestTime = si.viewTime; //only includes times from correct answers. 
                }

                if (si.viewTime > worstTime)
                    worstTime = si.viewTime;

                totalTime += si.viewTime;
            }

            Debug.Log("Correct Answers: " + correct);

            finalScore.numCorrect = correct;
            finalScore.totalTime = totalTime;
            finalScore.bestTime = bestTime;
            finalScore.worstTime = worstTime;
            finalScore.scorePercent = (100f * ((float)correct / (float)num)) - totalTime;
            finalScore.calculated = true;
        }
    }
}
