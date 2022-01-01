using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update
    void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// sends user input to the game manager
    /// including values to store in stroop items such as time and if the answer is correct.
    /// </summary>
    public void ColourPick()
    {
        //End the game if this is the last stroop item
        if(gm.currentItem >= gm.test.num)
        {
            gm.endGame = true;
        }

        string name = EventSystem.current.currentSelectedGameObject.name; //Get name of button (names are the colours)

        //Check if the button pressed is the correct answer and store this in the stroop item.
        if(name == gm.test.testElements[gm.currentItem - 1].ColourText() && gm.currentItem - 1 < gm.test.num)
        {
            gm.test.testElements[gm.currentItem - 1].correct = true;
        }

        gm.test.testElements[gm.currentItem - 1].viewTime = gm.test.wordTimer;//Store the time the stroop item was viewed before answering
        gm.test.wordTimer = 0; //reset timer for next word. 

        //End the game if this is the last stroop item
        if (gm.currentItem >= gm.test.num)
            gm.endGame = true;
        else 
            gm.answered = true; //set up game manager ready to accept the next user input.
    }

    public void Retry()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
