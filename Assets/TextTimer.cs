using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTimer : MonoBehaviour
{
    private float timeStart;
    private bool timerActive = false;
    [SerializeField] private TMP_Text stopwatchText;
    // Start is called before the first frame update
    void Start()
    {
        LiveDebugConsole.Instance.Log("TextTimer Start");
        stopwatchText.text = "0:00";
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            LiveDebugConsole.Instance.Log("Timer is active");
            float time = Time.time - timeStart;
            string minutes = ((int)time / 60).ToString();
            string seconds = (time % 60).ToString("f2");

            stopwatchText.text = minutes + ":" + seconds;
        }
    }

    public void startTimer()
    {
        LiveDebugConsole.Instance.Log("Timer started");
        // start timer
        timeStart = Time.time;
        timerActive = true;
    }

    public void stopTimer()
    {
        LiveDebugConsole.Instance.Log("Timer stopped");
        // stop timer
        timerActive = false;
    }

    public void resetTimer()
    {
        LiveDebugConsole.Instance.Log("Timer reset");
        // reset timer
        stopwatchText.text = "0:00";
        timeStart = Time.time;
    }
}
