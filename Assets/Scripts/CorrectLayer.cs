using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class CorrectLayer : MonoBehaviour
{
    public static CorrectLayer Instance; // Singleton instance
    [SerializeField] private TextTimer textTimer;
    public StringStorage stringStorage;
    public TMP_Text text;
    [SerializeField] private TMP_Text accuracyText;
    private List<string> listOfChars = new List<string>();
    private List<string> typingListOfChars = new List<string>();
    private List<int> wordCounts = new List<int>();
    private List<float> testTimes = new List<float>();
    public string User;


    string redTag = "<color=" + "red" + ">";
    string greyTag = "<color=" + "grey" + ">";
    string whiteTag = "<color=" + "white" + ">";

    List<string> tags = new List<string>();

    private bool started = false;
    private int testLength = 1;
    private int typedIndex = 0;
    private int trueIndex = 0;

    private int tmpWordCount = 0;
    private float tmpTime = 0.0f;


    private int correctCount = 0;
    private int totalCount = 0;

    // String that will be the name of the file that will be written to
    private string filePath;
    TextWriter tw;
    private int testCount = 4;

    void Start()
    {
        //  set file name as the current date and time
        filePath = "./" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "_User" + User + ".txt";

        // print current working directory
        // LiveDebugConsole.Instance.Log(System.IO.Directory.GetCurrentDirectory());

        // create the file
        tw = new StreamWriter(filePath);
        // LiveDebugConsole.Instance.Log("CorrectLayer Start");
        Instance = this;
        tags.Add(redTag);
        tags.Add(greyTag);
        tags.Add(whiteTag);

        reset();
    }

    private bool isDone = false;
    // Update is called once per frame
    void Update()
    {
        float acc = 0.0f;
        if (totalCount == 0)
        {
            acc = 0.0f;
        }
        else
        {
            acc = (correctCount / (float)totalCount * 100);
        }
        accuracyText.text = "Accuracy: " + (acc).ToString("F2") + "%";
    }

    private void reset()
    {
        if (testCount <= 0)
        {
            // LiveDebugConsole.Instance.Log("Test Done");

            // save the file
            tw.Close();

        }
        else
        {
            // tw.WriteLine("Test text: " + stringStorage.GetString(randomIndex) + "\n");

            // LiveDebugConsole.Instance.Log("new test started");
            listOfChars.Clear();
            typingListOfChars.Clear();
            started = false;
            typedIndex = 0;
            trueIndex = 0;
            isDone = false;
            wordCounts.Clear();
            testTimes.Clear();

            // correctCount = 0;
            // totalCount = 0;

            int randomIndex = Random.Range(0, 29);
            text.text = stringStorage.GetString(randomIndex);

            tw.WriteLine("Test text: " + stringStorage.GetString(randomIndex));

            // get the amount of words in the string by splitting it by spaces
            int wordCount = stringStorage.GetString(randomIndex).Split(' ').Length;
            // LiveDebugConsole.Instance.Log("wordCount: " + wordCount);
            tmpWordCount = wordCount;

            tw.WriteLine("Word count: " + wordCount);

            for (int i = 0; i < stringStorage.GetString(randomIndex).Length; i++)
            {
                listOfChars.Add(stringStorage.GetString(randomIndex).Substring(i, 1));
            }
            testLength = listOfChars.Count;
            //LiveDebugConsole.Instance.Log("testLength: " + testLength);

            textTimer.resetTimer();
            testCount--;
        }
    }

    public void AddChar(string c)
    {
        if (started == false)
        {
            started = true;
            textTimer.startTimer();
        }

        typingListOfChars.Add(c);

        if (typingListOfChars[typedIndex].ToLower() == listOfChars[trueIndex].ToLower())
        {
            listOfChars.Insert(trueIndex, whiteTag);
            listOfChars.Insert(trueIndex + 2, greyTag);
            trueIndex += 2;
            correctCount++;
            totalCount++;
        }
        else
        {
            listOfChars.Insert(trueIndex, redTag);
            listOfChars.Insert(trueIndex + 2, greyTag);
            trueIndex += 2;
            totalCount++;
        }

        typedIndex++;
        trueIndex++;

        if (typedIndex >= testLength)
        {
            isDone = true;

            wordCounts.Add(tmpWordCount);

            tw.WriteLine("Test time: " + textTimer.getTime());

            float acc = 0.0f;
            if (totalCount == 0)
            {
                acc = 0.0f;
            }
            else
            {
                acc = (correctCount / (float)totalCount * 100);
            }
            tw.WriteLine("Accuracy: " + acc);
            tw.WriteLine("WPM: " + (tmpWordCount / textTimer.getTime() * 60));
            tw.WriteLine("Correct count: " + correctCount);
            tw.WriteLine("Total count: " + totalCount + "\n");


            testTimes.Add(textTimer.getTime());
            textTimer.stopTimer();

            updateAvg();
            reset();
        }

        text.text = string.Join("", listOfChars);
    }

    private void updateAvg()
    {
        // sum up all the float values in the testTimes list
        float timeSum = 0.0f;
        int wordSum = 0;
        for (int i = 0; i < wordCounts.Count; i++)
        {
            timeSum += testTimes[i];
            wordSum += wordCounts[i];
        }

        float timeAvg = timeSum / testTimes.Count;
        float wordAvg = wordSum / wordCounts.Count;
        float WPM = wordAvg / timeAvg * 60;
        textTimer.updateAvg(WPM);
    }
}