using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    private Test test;

    private void Awake()
    {
        test = FindObjectOfType<Test>();

        if (test == null)
        {
            test = new Test();
        }

        tmp = GameObject.FindGameObjectWithTag("Display").GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StroopItem si = new StroopItem();
        si.GenerateRandom();
        tmp.text = si.text;
        tmp.color = si.GetColour();
        //UpdateText();
    }

    void UpdateText()
    {
        tmp.text = test.testElements[0].text;
        tmp.color = test.testElements[0].GetColour();
    }
}
