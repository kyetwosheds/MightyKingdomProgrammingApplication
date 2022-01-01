using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StroopItem : MonoBehaviour
{
    public enum VisualColour {red, blue, yellow, pink};
    public VisualColour visCol;

    public string text;
    private string[] namesColour = {"RED", "BLUE", "YELLOW", "PINK"};

    public float viewTime;
    public bool correct = false;

    public StroopItem()
    {
        GenerateRandom();
    }

    /// <summary>
    /// Creates a Stroop Item with random colour name and display.
    /// </summary>
    public void GenerateRandom()
    {
        //pick random visual colour to display
        visCol = (VisualColour)UnityEngine.Random.Range(0, Enum.GetNames(typeof(VisualColour)).Length);

        //pick random colour name
        text = namesColour[UnityEngine.Random.Range(0, namesColour.Length)];
    }

    /// <summary>
    /// Converts the Visual Colour enum into colours unity can use.
    /// </summary>
    /// <returns>colour to display in text mesh pro</returns>
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

    /// <summary>
    /// Gets the display colour as a string for comparion with answers.
    /// </summary>
    /// <returns>colour name as a string</returns>
    public string ColourText()
    {
        switch (visCol)
        {
            case VisualColour.red:
                return "RED";
            case VisualColour.blue:
                return "BLUE";
            case VisualColour.yellow:
                return "YELLOW";
            case VisualColour.pink:
                return "PINK";
        }
        return "";
    }
}