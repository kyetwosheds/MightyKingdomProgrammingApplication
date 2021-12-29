using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update
    void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    public void ColourPick()
    {
        if(gm.currentItem >= gm.test.num)
        {
            gm.endGame = true;
        }
        string name = EventSystem.current.currentSelectedGameObject.name;
        if(name == gm.test.testElements[gm.currentItem - 1].text && gm.currentItem - 1 < gm.test.num)
        {
            gm.test.testElements[gm.currentItem - 1].correct = true;
        }
        gm.test.testElements[gm.currentItem - 1].viewTime = gm.test.wordTimer;
        gm.test.wordTimer = 0;

        if (gm.currentItem >= gm.test.num)
            gm.endGame = true;
        else 
            gm.answered = true;
    }
}
