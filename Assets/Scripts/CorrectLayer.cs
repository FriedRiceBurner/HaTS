using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CorrectLayer : MonoBehaviour
{
    // reference to the string storage object
    public StringStorage stringStorage;
    // reference to the text mesh pro object
    public TMP_Text text;
    // Start is called before the first frame update
    List<string> listOfChars = new List<string>();
    [SerializeField] private List<string> typingListOfChars = new List<string>();


    string redTag = "<color=" + "red" + ">";
    string greyTag = "<color=" + "grey" + ">";
    string whiteTag = "<color=" + "white" + ">";

    List<string> tags = new List<string>();


    void Start()
    {
        tags.Add(redTag);
        tags.Add(greyTag);
        tags.Add(whiteTag);

        text.text = stringStorage.GetString(0);

        // seperate the string into individual characters, this aids in inline color changes later
        for (int i = 0; i < stringStorage.GetString(0).Length; i++)
        {
            // Debug.Log(stringStorage.GetString(0).Substring(i, 1));
            listOfChars.Add(stringStorage.GetString(0).Substring(i, 1));
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
        typingListOfChars.Add(c);

        if (typingListOfChars[typedIndex] == listOfChars[trueIndex])
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

        if (typedIndex >= typingListOfChars.Count)
        {
            isDone = true;
        }

        text.text = string.Join("", listOfChars);
    }
}

// few      k             ust
// few <red>j<white><grey>ust fine