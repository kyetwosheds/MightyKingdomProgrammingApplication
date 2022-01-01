using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public StroopTest test;
    public int currentItem = 0;

    public float startCountdown = 4;

    public bool answered = true;
    public bool endGame = false;

    public GameObject gameScreen;
    public GameObject resultsScreen;
    public GameObject buttonDisplay;

    private void Awake()
    {
        //Setup canvas objects to use later.
        gameScreen = GameObject.Find("GameScreen");
        resultsScreen = GameObject.Find("ResultsScreen");
        buttonDisplay = GameObject.FindGameObjectWithTag("Input");
    }

    void Start()
    {
        //Display game screen
        resultsScreen.SetActive(false);
        gameScreen.SetActive(true);

        //Create test with finie number of stroop items
        if (test == null)
        {
            test = GameObject.FindObjectOfType<StroopTest>();
            tmp = GameObject.FindGameObjectWithTag("Display").GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        if(!endGame) 
        {
            //displays the countdown before the test starts.
            if (startCountdown > 0)
            {
                buttonDisplay.SetActive(false); //remove buttons to prevent answering before the test starts.
                startCountdown -= Time.deltaTime;
                int second = (int)startCountdown;
                tmp.text = second.ToString();
            }  
            else if (answered) // checks if we are waiting for input from the user.
            {
                buttonDisplay.SetActive(true);
                if (!CheckFinished()) //Checks if we have finished the test
                {
                    //setup and display next stroop item.
                    test.timing = true;
                    answered = false;
                    StroopItem si = test.testElements[currentItem];
                    tmp.text = si.text;
                    tmp.color = si.GetColour();
                    currentItem += 1;
                }
            }
        } else
        {
            CheckFinished();
            //Throw Game Over Screen.
            if(test.finalScore.calculated)
            {
                resultsScreen.SetActive(true);
                gameScreen.SetActive(false);

                //Display Statistics
                var scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = "SCORE: " + test.finalScore.scorePercent.ToString("0.00") + "%";
                var totalTimeText = GameObject.Find("timeNumber").GetComponent<TextMeshProUGUI>().text = test.finalScore.totalTime.ToString("0.00") + "s";
                var successText = GameObject.Find("successText").GetComponent<TextMeshProUGUI>().text = test.finalScore.numCorrect + "/" + test.num;
                var bestText = GameObject.Find("bestTime").GetComponent<TextMeshProUGUI>().text = test.finalScore.bestTime.ToString("0.00") + "s";
                var worstText = GameObject.Find("worstTime").GetComponent<TextMeshProUGUI>().text = test.finalScore.worstTime.ToString("0.00") + "s";
            }
        } 
    }

    bool CheckFinished()
    {
        if(currentItem >= test.num)
        {
            foreach(StroopItem si in test.testElements)
            {
                Debug.Log("Word: " + si.text + ", Time: " + si.viewTime.ToString() + ", Correct: " + si.correct);
            }
            endGame = true;
            return true;
        }
        return false;
    }
}
