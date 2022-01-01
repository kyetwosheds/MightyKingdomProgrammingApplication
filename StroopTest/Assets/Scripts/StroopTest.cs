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
        StroopItem prevStroopItem = new StroopItem();
        Debug.Log("Beep");
        prevStroopItem.GenerateRandom();
        Debug.Log("Boop");
        Debug.Log(prevStroopItem.text + " " + prevStroopItem.ColourText());
        //Create finite test items.
        for (int i = 0; i < num; i++)
        {
            StroopItem si = new StroopItem();
            si.GenerateRandom();
            Debug.Log(si.text + " " + si.ColourText());
            
            //Make sure the user isn't presented with the same answer twice.
            while(si.text == prevStroopItem.text && si.ColourText() == prevStroopItem.ColourText())
            {
                si.GenerateRandom();
            }

            testElements.Add(si);
            prevStroopItem = si;
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
                //Tracks and displays the time each word is on the screen.
                wordTimer += Time.deltaTime;
                tmpTimer.text = wordTimer.ToString("0.00");
            }
        } else if(!finalScore.calculated) //Check if the final score has already being calculated to prevent doing it more than once. 
        {
            //calculate scores.
            int correct = 0;
            float totalTime = 0;
            float bestTime = 300; //if the player exceeds 300 seconds (5 minutes) it will be capped at 300.
            float worstTime = 0;

            foreach(StroopItem si in testElements)
            {
                //checks if each answer was correct.
                if (si.correct)
                {
                    correct += 1; //add to total correct answers.

                    //only includes times from correct answers. 
                    if (si.viewTime < bestTime)
                        bestTime = si.viewTime; 
                }

                //Check if this time exceeds the current tracked longest time. 
                if (si.viewTime > worstTime)
                    worstTime = si.viewTime;

                //Calculates total time viewed based off the sum of the view time of each item.
                totalTime += si.viewTime;
            }

            Debug.Log("Correct Answers: " + correct);

            finalScore.numCorrect = correct;
            finalScore.totalTime = totalTime;
            finalScore.bestTime = bestTime; //Chance of there being no best time if the player gets all answers wrong. 
            finalScore.worstTime = worstTime;

            //fairly arbitrary method of calculating the score. Cannot exceed 100% but has a chance of being negative. Needs better design. 
            finalScore.scorePercent = (100f * ((float)correct / (float)num)) - totalTime; //works as a place holder. 
            finalScore.calculated = true;
        }
    }
}
