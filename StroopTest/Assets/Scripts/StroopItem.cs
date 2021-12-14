using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StroopItem : MonoBehaviour
{
    public enum VisualColour {red, blue, yellow, pink};
    public VisualColour visCol;

    public string text;
    private string[] namesColour = {"red", "blue", "yellow", "pink"};

    public float viewTime;
    public bool correct = false;

    /// <summary>
    /// 
    /// </summary>
    public void GenerateRandom()
    {
        //pick random visual colour to display
        visCol = (VisualColour)UnityEngine.Random.Range(0, Enum.GetNames(typeof(VisualColour)).Length);

        //pick random colour name
        text = namesColour[UnityEngine.Random.Range(0, namesColour.Length)];
    }

    public Color GetColour()
    {
        switch (visCol)
        {
            case VisualColour.red:
                return Color.red;
            case VisualColour.blue:
                return Color.blue;
            case VisualColour.yellow:
                return Color.yellow;
            case VisualColour.pink:
                return new Color(232, 0, 254);
            default:
                return Color.black; //if text is black need to debug
        }
    }
}