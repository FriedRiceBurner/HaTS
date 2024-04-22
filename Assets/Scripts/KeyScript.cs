using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO.Ports;

public class KeyScript : MonoBehaviour
{
    [SerializeField] GameObject lefthand;
    [SerializeField] GameObject righthand;
    [SerializeField] GameObject HATS;

    public string keyID;

    // reference to the text mesh pro object
    public TMP_Text KeyText;
    // reference to the sound to play on key press
    public AudioSource sound;

    private GameObject correctLayer;

    // Start is called before the first frame update
    void Start()
    {
        KeyText.SetText(keyID);
        GameObject correctLayer = GameObject.Find("Typing Test");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pressed()
    {
        // GameObject chosenfinger;
        // for now just log the key pressed
        // LiveDebugConsole.Instance.Log("Key pressed: " + KeyText.text);
        // play the sound
        // float closestDistance = float.PositiveInfinity;
        // foreach (GameObject bone in lefthand.transform)
        // {
        //     float distance = Vector3.Distance(transform.position, bone.transform.position);

        //     if (distance < closestDistance)
        //     {
        //         closestDistance = distance;
        //         chosenfinger = bone;
        //     }
        // }
        // foreach (GameObject bone in righthand.transform)
        // {
        //     float distance = Vector3.Distance(transform.position, bone.transform.position);

        //     if (distance < closestDistance)
        //     {
        //         closestDistance = distance;
        //         chosenfinger = bone;
        //     }
        // }

        // if (Input.GetKeyDown("1"))
        // {
        //     StartCoroutine(Buzz(leftPinkey));
        // }
        // if (Input.GetKeyDown("2"))
        // {
        //     StartCoroutine(Buzz(leftRing));
        // }

        // if (Input.GetKeyDown("3"))
        // {
        //     StartCoroutine(Buzz(leftMiddle));
        // }
        // if (Input.GetKeyDown("4"))
        // {
        //     StartCoroutine(Buzz(leftIndex));
        // }

        // if (Input.GetKeyDown("5"))
        // {
        //     StartCoroutine(Buzz(leftThumb));
        // }


        //HATS.GetComponent<HaTS>().Buzz(1);
        sound.Play();
        // LiveDebugConsole.Instance.Log("Key pressed: " + KeyText.text);

        // correctLayer.GetComponent<CorrectLayer>().AddChar(KeyText.text);
        CorrectLayer.Instance.AddChar(KeyText.text);

    }
}
