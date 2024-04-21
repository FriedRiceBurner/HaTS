using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyScript : MonoBehaviour
{

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
        // for now just log the key pressed
        // LiveDebugConsole.Instance.Log("Key pressed: " + KeyText.text);
        // play the sound
        sound.Play();
        // LiveDebugConsole.Instance.Log("Key pressed: " + KeyText.text);

        // correctLayer.GetComponent<CorrectLayer>().AddChar(KeyText.text);
        CorrectLayer.Instance.AddChar(KeyText.text);

    }
}
