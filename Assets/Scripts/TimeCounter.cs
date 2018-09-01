using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCounter : MonoBehaviour
{
    Text timeUI;
    
    public int totalTime;
    public bool timeUp;

    float startTime;
    float ellapsedTime;
    float timeLeft;

    bool startCounter;

    int minutes;
    int seconds;

    // Use this for initialization
    void Start()
    {
       // startCounter = false;

        timeUI = GetComponent<Text>();
    }

    public void StartTimeCounter()
    {
        startTime = Time.time;
        startCounter = true;
    }


    public void StopTimeCounter()
    {
        startCounter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startCounter)
        {
            ellapsedTime = Time.time - startTime;
            timeLeft = totalTime - ellapsedTime;

            minutes = (int)timeLeft / 60;
            seconds = (int)timeLeft % 60;


            timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (timeLeft <= 0)
                timeUp = true;
        }
    }
}
