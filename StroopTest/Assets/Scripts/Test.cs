using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Header("Number of elements in test")]
    public int num = 10;
    
    [Header("Generated test elements")]
    public List<StroopItem> testElements;

    private void Awake()
    {
        for (int i = 0; i < num; i++)
        {
            StroopItem si = new StroopItem();
            si.GenerateRandom();
            testElements.Add(si);
        }
            
    }
}
