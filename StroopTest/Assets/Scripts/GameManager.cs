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

    private void Awake()
    {
        gameScreen = GameObject.Find("GameScreen");
        resultsScreen = GameObject.Find("ResultsScreen");
    }

    // Start is called before the first frame update
    void Start()
    {
        resultsScreen.SetActive(false);
        gameScreen.SetActive(true);
        if (test == null)
        {
            test = GameObject.FindObjectOfType<StroopTest>();
            tmp = GameObject.FindGameObjectWithTag("Display").GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!endGame)
        {
            if (startCountdown > 0)
            {
                startCountdown -= Time.deltaTime;
                int second = (int)startCountdown;
                tmp.text = second.ToString();
            }
            else if (answered)
            {
                if (!CheckFinished())
                {
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
