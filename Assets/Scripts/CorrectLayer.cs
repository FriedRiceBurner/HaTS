using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CorrectLayer : MonoBehaviour
{
    public static CorrectLayer Instance; // Singleton instance

    [SerializeField] private TextTimer textTimer;

    // reference to the string storage object
    public StringStorage stringStorage;
    // reference to the text mesh pro object
    public TMP_Text text;
    // Start is called before the first frame update
    private List<string> listOfChars = new List<string>();
    private List<string> typingListOfChars = new List<string>();
    private List<int> wordCounts = new List<int>();
    private List<float> testTimes = new List<float>();


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

    void Start()
    {
        LiveDebugConsole.Instance.Log("CorrectLayer Start");
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

    }

    private void reset()
    {
        LiveDebugConsole.Instance.Log("new test started");
        listOfChars.Clear();
        typingListOfChars.Clear();
        started = false;
        typedIndex = 0;
        trueIndex = 0;
        isDone = false;
        wordCounts.Clear();
        testTimes.Clear();

        int randomIndex = Random.Range(0, 5);
        text.text = stringStorage.GetString(randomIndex);

        // get the amount of words in the string by splitting it by spaces
        int wordCount = stringStorage.GetString(randomIndex).Split(' ').Length;
        LiveDebugConsole.Instance.Log("wordCount: " + wordCount);
        tmpWordCount = wordCount;

        for (int i = 0; i < stringStorage.GetString(randomIndex).Length; i++)
        {
            listOfChars.Add(stringStorage.GetString(randomIndex).Substring(i, 1));
        }
        testLength = listOfChars.Count;
        LiveDebugConsole.Instance.Log("testLength: " + testLength);

        textTimer.resetTimer();
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
        }
        else
        {
            listOfChars.Insert(trueIndex, redTag);
            listOfChars.Insert(trueIndex + 2, greyTag);
            trueIndex += 2;
        }

        typedIndex++;
        trueIndex++;

        if (typedIndex >= testLength)
        {
            isDone = true;

            wordCounts.Add(tmpWordCount);

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