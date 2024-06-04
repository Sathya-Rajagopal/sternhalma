using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60; 
    private bool timeIsRunning = true;
    public TMP_Text timeText;
    public TMP_Text gameEndText; 

    void Start()
    {
    }

    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeIsRunning = false;
            }
            
        }
        DisplayTime(timeRemaining);
        CheckGameStatus();
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(timeToDisplay, 0);
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void CheckGameStatus()
    {
        
        
        int piecesLeft  = GameObject.FindGameObjectsWithTag("Pieces").Length; 

        if (piecesLeft == 1) 
        {
            gameEndText.text = "You Won!";
            gameEndText.gameObject.SetActive(true); 
            timeIsRunning = false;
        }
        else if(!timeIsRunning)
        {
            gameEndText.text = "You Lost!";
            timeRemaining = 0;
            timeIsRunning = false;
            gameEndText.gameObject.SetActive(true); 
        }

    }
}
