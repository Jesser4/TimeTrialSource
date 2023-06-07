using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopwatchCounter : MonoBehaviour
{
    float contup=0;
    public GameObject targetStopwatch;
    private TextChanger stopwatch;
    
    void Start()
    {
        targetStopwatch = GameObject.FindWithTag("Stopwatch");
        stopwatch = targetStopwatch.GetComponent<TextChanger>();
    }

    void Update()
    {
 
        contup += Time.deltaTime;     
        
        stopwatch.ChangeDisplayText(Math.Round(contup, 3).ToString("F3"));
     
    }

    public void ResetCountUp()
    {
        contup = 0;
    }
}
