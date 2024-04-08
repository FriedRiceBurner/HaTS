using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{

    public string keyID;

    // reference to the text mesh pro object
    public TMPro.TextMeshPro KeyText;
    // reference to the sound to play on key press
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        KeyText.SetText(keyID);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pressed()
    {
        // for now just log the key pressed
        LiveDebugConsole.Instance.Log("Key pressed: " + KeyText.text);
        // play the sound
        sound.Play();
    }
}
