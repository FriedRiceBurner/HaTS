using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTimer : MonoBehaviour
{
    private float timeStart;
    private bool timerActive = false;
    [SerializeField] private TMP_Text stopwatchText;
    [SerializeField] private TMP_Text avgText;
    // Start is called before the first frame update
    void Start()
    {
        stopwatchText.text = "0:00";
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            float time = Time.time - timeStart;
            string minutes = ((int)time / 60).ToString();
            string seconds = (time % 60).ToString("f2");

            stopwatchText.text = minutes + ":" + seconds;
        }
    }

    public void startTimer()
    {
        // start timer
        timeStart = Time.time;
        timerActive = true;
    }

    public void stopTimer()
    {
        // stop timer
        timerActive = false;
    }

    public void resetTimer()
    {
        // reset timer
        stopwatchText.text = "0:00";
        timeStart = Time.time;
    }

    public float getTime()
    {
        return Time.time - timeStart;
    }

    public void updateAvg(float avg)
    {
        avgText.text = "Avg WPM: " + avg.ToString("f2");
    }
}
