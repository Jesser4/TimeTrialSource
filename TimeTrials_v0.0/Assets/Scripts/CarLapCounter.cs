using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class CarLapCounter : MonoBehaviour
{

    int passedCheckPointNumber = 0;
    float timeAtLastPassedCheckPoint = 0;
    float splitTime = 0;
    private float splitHelper = 0;
    private float totalTime = 0;
    private float startTime = 0;
    private float record = 100f;
    
    int numberOfPassedCheckpoints = 0;
    
    public GameObject targetHighscore;
    public GameObject targetStopwatch;
    public GameObject targetPastTime;
    private TextChanger pastTime;
    private StopwatchCounter stopwatch;
    private TextChanger highscore;
    private TextChanger invalidLap;
    public GameObject targetInvalid;

    

    public event Action<CarLapCounter> OnPassCheckpoint;

    private void Start()
    {

        stopwatch = targetStopwatch.GetComponent<StopwatchCounter>();
        highscore = targetHighscore.GetComponent<TextChanger>();
        invalidLap = targetInvalid.GetComponent<TextChanger>();

        pastTime = targetPastTime.GetComponent<TextChanger>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Checkpoint"))
        {

            CheckpointScript checkPoint = collider2D.GetComponent<CheckpointScript>();

            //Make sure that the car is passing the checkpoints in the correct order. The correct checkpoint must have exactly 1 higher value than the passed checkpoint
            if (passedCheckPointNumber + 1 == checkPoint.checkPointNumber)
            {
                
                passedCheckPointNumber = checkPoint.checkPointNumber;
                
                numberOfPassedCheckpoints++;

                //Store the time at the checkpoint
                
                splitTime = timeAtLastPassedCheckPoint - splitHelper;
                splitHelper = timeAtLastPassedCheckPoint;
                timeAtLastPassedCheckPoint = Time.time;
                
                
                
                // OnPassCheckpoint?.Invoke(this);
                
            }
            
            else if (passedCheckPointNumber + 1 != checkPoint.checkPointNumber)
            {
                
                invalidLap.ChangeDisplayText("INVALID LAP");
            }

            if (checkPoint.isFinishLine && passedCheckPointNumber == 16)
            {
                totalTime = timeAtLastPassedCheckPoint - startTime;
                passedCheckPointNumber = 0;
                pastTime.ChangeDisplayText(totalTime.ToString("F3"));
                if (totalTime < record)
                {
                    record = totalTime;
                    highscore.ChangeDisplayText(record.ToString("F3"));
                    Debug.Log($"NEW RECORD OF: {record}");
                    
                }
            }
            else if (checkPoint.isFinishLine && passedCheckPointNumber != 16)
            {
                passedCheckPointNumber = 0;
                Debug.Log("INVALID LAP");
            }

            if (checkPoint.isStartLine)
            {
                startTime = Time.time;
                splitHelper = startTime;
                stopwatch.ResetCountUp();
                invalidLap.ChangeDisplayText("");
            }
        }
    }

    public float GetTotalTime()
    {
        return totalTime;
    }

    public int GetGateNumber()
    {
        return passedCheckPointNumber;
    }
}
