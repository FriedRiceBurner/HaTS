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


    string redTag = "<color=" + "red" + ">";
    string greyTag = "<color=" + "grey" + ">";
    string whiteTag = "<color=" + "white" + ">";

    List<string> tags = new List<string>();

    private bool started = false;
    private int testLength = 1;
    private int typedIndex = 0;
    private int trueIndex = 0;

    void Start()
    {
        LiveDebugConsole.Instance.Log("CorrectLayer Start");
        Instance = this;
        tags.Add(redTag);
        tags.Add(greyTag);
        tags.Add(whiteTag);

        int randomIndex = Random.Range(0, 5);
        text.text = stringStorage.GetString(randomIndex);

        for (int i = 0; i < stringStorage.GetString(randomIndex).Length; i++)
        {
            listOfChars.Add(stringStorage.GetString(randomIndex).Substring(i, 1));
        }
        testLength = listOfChars.Count;
    }

    private bool isDone = false;
    // Update is called once per frame
    void Update()
    {

    }

    private void reset()
    {
        listOfChars.Clear();
        typingListOfChars.Clear();
        started = false;
        typedIndex = 0;
        trueIndex = 0;
        isDone = false;

        int randomIndex = Random.Range(0, 5);
        text.text = stringStorage.GetString(randomIndex);

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
            LiveDebugConsole.Instance.Log(">>> started");
            started = true;
            textTimer.startTimer();
        }

        // LiveDebugConsole.Instance.Log("typedIndex: " + typedIndex + " trueIndex: " + trueIndex);

        typingListOfChars.Add(c);

        // LiveDebugConsole.Instance.Log(typingListOfChars[typedIndex]);
        // LiveDebugConsole.Instance.Log(listOfChars[trueIndex]);

        if (typingListOfChars[typedIndex].ToLower() == listOfChars[trueIndex].ToLower())
        {
            // LiveDebugConsole.Instance.Log("match");
            listOfChars.Insert(trueIndex, whiteTag);
            listOfChars.Insert(trueIndex + 2, greyTag);
            trueIndex += 2;
        }
        else
        {
            // LiveDebugConsole.Instance.Log("no match");
            listOfChars.Insert(trueIndex, redTag);
            listOfChars.Insert(trueIndex + 2, greyTag);
            trueIndex += 2;
        }

        // LiveDebugConsole.Instance.Log("before, typedIndex: " + typedIndex + " trueIndex: " + trueIndex);
        typedIndex++;
        trueIndex++;
        // LiveDebugConsole.Instance.Log("after, typedIndex: " + typedIndex + " trueIndex: " + trueIndex);

        LiveDebugConsole.Instance.Log("typedIndex: " + typedIndex + " testLength: " + testLength);
        if (typedIndex >= testLength)
        {
            LiveDebugConsole.Instance.Log("typedIndex >= typingListOfChars.Count");
            // LiveDebugConsole.Instance.Log("isDone set to true");
            isDone = true;
            textTimer.stopTimer();
            reset();
        }

        // LiveDebugConsole.Instance.Log("text: " + string.Join("", listOfChars));
        text.text = string.Join("", listOfChars);
    }
}