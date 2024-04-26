using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO.Ports;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction.Collections;
using System.Linq;

public class KeyScript : MonoBehaviour
{
    [SerializeField] GameObject lefthand;
    [SerializeField] GameObject righthand;
    public string keyID;

    // reference to the text mesh pro object
    public TMP_Text KeyText;
    // reference to the sound to play on key press
    public AudioSource sound;

    private GameObject correctLayer;
    PokeInteractor poker;
    private HaTS hats;

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

    //check the passed in string to see which finger was used, hand that the finger is attached to
    private void CheckFinger(string input, string hand)
    {
        input = input.ToLower();
        // Debug.Log(input);
        switch (input)
        {
            case string s when s.Contains("pinky"):
                if (hand.Contains("left"))
                {
                    StartCoroutine(hats.GetComponent<HaTS>().Buzz(hats.leftPinkey));
                }
                else
                {
                    StartCoroutine(hats.GetComponent<HaTS>().Buzz(hats.rightPinkey));
                }

                return;
            case string s when s.Contains("ring"):
                if (hand.Contains("left"))
                {
                    StartCoroutine(hats.GetComponent<HaTS>().Buzz(hats.leftRing));
                }
                else
                {
                    StartCoroutine(hats.GetComponent<HaTS>().Buzz(hats.rightRing));
                }

                return;
            case string s when s.Contains("middle"):
                if (hand.Contains("left"))
                {
                    StartCoroutine(hats.GetComponent<HaTS>().Buzz(hats.leftMiddle));
                }
                else
                {
                    StartCoroutine(hats.GetComponent<HaTS>().Buzz(hats.rightMiddle));
                }

                return;
            case string s when s.Contains("index"):
                if (hand.Contains("left"))
                {
                    StartCoroutine(hats.GetComponent<HaTS>().Buzz(hats.leftIndex));
                }
                else
                {
                    StartCoroutine(hats.GetComponent<HaTS>().Buzz(hats.rightIndex));

                }

                return;
            case string s when s.Contains("thumb"):
                if (hand.Contains("left"))
                {
                    Debug.Log("left hand");
                    StartCoroutine(hats.GetComponent<HaTS>().Buzz(hats.leftThumb));
                }
                else
                {
                    Debug.Log("right hand");

                    StartCoroutine(hats.GetComponent<HaTS>().Buzz(hats.rightThumb));
                }

                return;
        }
    }
    public void pressed()
    {
        hats = transform.parent.GetComponent<HandDebugManager>().hatsHolder;
        // Debug.Log("transform.parent.name");
        // Debug.Log(transform.parent.name);
        // Debug.Log(hats.blinkSpeed);

        // Debug.Log("poke stuff");
        // Debug.Log(keyID);

        //get the object that interacted with the key 
        poker = GetComponent<PokeInteractable>().Interactors.First();
        // Debug.Log(poker.transform.parent.parent.parent.parent.parent.parent.name.ToLower());
        // Debug.Log(poker.transform.parent.parent.parent.parent.parent.parent.parent.name.ToLower());
        // Debug.Log(poker.transform.parent.parent.parent.parent.parent.parent.parent.parent.name.ToLower());

        if (poker.transform.parent.parent.parent.parent.parent.parent.name.ToLower().Contains("left") ||
            poker.transform.parent.parent.parent.parent.parent.parent.parent.name.ToLower().Contains("left") ||
            poker.transform.parent.parent.parent.parent.parent.parent.parent.parent.name.ToLower().Contains("left"))
        {
            CheckFinger(poker.transform.parent.name.ToLower(), "left");
            // Debug.Log("left hand");
        }
        else
        {
            CheckFinger(poker.transform.parent.name.ToLower(), "right");
            // Debug.Log("right hand");

        }

        sound.Play();
        // LiveDebugConsole.Instance.Log("Key pressed: " + KeyText.text);

        // correctLayer.GetComponent<CorrectLayer>().AddChar(KeyText.text);
        CorrectLayer.Instance.AddChar(KeyText.text);

    }
}
