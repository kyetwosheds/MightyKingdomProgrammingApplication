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

    // Start is called before the first frame update
    void Start()
    {
        if(test == null)
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
