using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CorrectLayer : MonoBehaviour
{
    public static CorrectLayer Instance; // Singleton instance

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


    void Start()
    {
        LiveDebugConsole.Instance.Log("CorrectLayer Start");
        // LiveDebugConsole.Instance.Log("HI".ToLower());
        // CorrectLayer. 
        Instance = this;
        tags.Add(redTag);
        tags.Add(greyTag);
        tags.Add(whiteTag);

        int randomIndex = Random.Range(0, 5);
        text.text = stringStorage.GetString(randomIndex);

        // seperate the string into individual characters, this aids in inline color changes later
        for (int i = 0; i < stringStorage.GetString(randomIndex).Length; i++)
        {
            // Debug.Log(stringStorage.GetString(0).Substring(i, 1));
            listOfChars.Add(stringStorage.GetString(randomIndex).Substring(i, 1));
            // Debug.Log(">>>>>>>>>>>>>" + listOfChars.Count);
        }
        // Debug.Log(">>>>>>>>>>>>>" + string.Join("", listOfChars));
    }

    private bool isDone = false;
    // Update is called once per frame
    void Update()
    {
        // if (!isDone)
        // {
        //     // logic to construct the string with inline color changes
        //     int typedIndex = 0;
        //     for (int trueIndex = 0; trueIndex < listOfChars.Count; trueIndex++)
        //     {
        //         if (typingListOfChars[typedIndex] == "s" && listOfChars[trueIndex] == "s")
        //         {
        //             isDone = true;
        //             break;
        //         }
        //         if (typingListOfChars[typedIndex] == listOfChars[trueIndex])
        //         {
        //             listOfChars.Insert(trueIndex, whiteTag);
        //             listOfChars.Insert(trueIndex + 2, greyTag);
        //             trueIndex += 2;
        //         }
        //         else
        //         {
        //             listOfChars.Insert(trueIndex, redTag);
        //             listOfChars.Insert(trueIndex + 2, greyTag);
        //             trueIndex += 2;
        //         }

        //         typedIndex++;

        //         if (typedIndex >= typingListOfChars.Count)
        //         {
        //             isDone = true;
        //             // break;
        //         }
        //     }

        //     text.text = string.Join("", listOfChars);
        // }
    }

    private int typedIndex = 0;
    private int trueIndex = 0;
    public void AddChar(string c)
    {
        // if (typingListOfChars[typedIndex] == "s" && listOfChars[trueIndex] == "s")
        // {
        //     isDone = true;
        //     break;
        // }
        LiveDebugConsole.Instance.Log("typedIndex: " + typedIndex + " trueIndex: " + trueIndex);
        // LiveDebugConsole.Instance.Log("in addChar: " + c + " " + typingListOfChars[typedIndex] + " " + listOfChars[trueIndex]);
        // LiveDebugConsole.Instance.Log(typingListOfChars[typedIndex]);
        // LiveDebugConsole.Instance.Log(listOfChars[trueIndex]);

        typingListOfChars.Add(c);

        LiveDebugConsole.Instance.Log(typingListOfChars[typedIndex]);
        LiveDebugConsole.Instance.Log(listOfChars[trueIndex]);

        if (typingListOfChars[typedIndex].ToLower() == listOfChars[trueIndex].ToLower())
        {
            LiveDebugConsole.Instance.Log("match");
            listOfChars.Insert(trueIndex, whiteTag);
            listOfChars.Insert(trueIndex + 2, greyTag);
            trueIndex += 2;
            // 01  2  3  4     5    6
            // ju     s             t
            // ju     d             t
            // ju<red>d<white><grey>t
        }
        else
        {
            LiveDebugConsole.Instance.Log("no match");
            listOfChars.Insert(trueIndex, redTag);
            listOfChars.Insert(trueIndex + 2, greyTag);
            trueIndex += 2;
        }

        LiveDebugConsole.Instance.Log("before, typedIndex: " + typedIndex + " trueIndex: " + trueIndex);
        typedIndex++;
        trueIndex++;
        LiveDebugConsole.Instance.Log("after, typedIndex: " + typedIndex + " trueIndex: " + trueIndex);

        if (typedIndex >= typingListOfChars.Count)
        {
            LiveDebugConsole.Instance.Log("isDone sety to true");
            isDone = true;
        }

        // LiveDebugConsole.Instance.Log("text: " + string.Join("", listOfChars));
        text.text = string.Join("", listOfChars);
    }
}

// few      k             ust
// few <red>j<white><grey>ust fine