using UnityEngine;
using System.Collections;
using Uduino;
//using Unity.VisualScripting;
//using Unity.Android.Types; // adding Uduino NameSpace 

public class HaTS : MonoBehaviour
{
    UduinoManager u; // The instance of Uduino is initialized here
    public int leftPinkey;
    public int leftRing;
    public int leftMiddle;
    public int leftIndex;
    public int leftThumb;
    public int rightThumb;
    public int rightIndex;
    public int rightMiddle;
    public int rightRing;
    public int rightPinkey;
    //[Range(0, 5)]
    public float blinkSpeed = 1;
    void Start()
    {

        UduinoManager.Instance.pinMode(leftPinkey, PinMode.Output);
        UduinoManager.Instance.pinMode(leftRing, PinMode.Output);
        UduinoManager.Instance.pinMode(leftMiddle, PinMode.Output);
        UduinoManager.Instance.pinMode(leftIndex, PinMode.Output);
        UduinoManager.Instance.pinMode(leftThumb, PinMode.Output);
        UduinoManager.Instance.pinMode(rightThumb, PinMode.Output);
        UduinoManager.Instance.pinMode(rightIndex, PinMode.Output);
        UduinoManager.Instance.pinMode(rightMiddle, PinMode.Output);
        UduinoManager.Instance.pinMode(rightRing, PinMode.Output);
        UduinoManager.Instance.pinMode(rightPinkey, PinMode.Output);
        StartCoroutine(Buzz(leftPinkey));
        //StartCoroutine(BlinkLoop());
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            StartCoroutine(Buzz(leftPinkey));
        }
        if (Input.GetKeyDown("2"))
        {
            StartCoroutine(Buzz(leftRing));
        }

        if (Input.GetKeyDown("3"))
        {
            StartCoroutine(Buzz(leftMiddle));
        }
        if (Input.GetKeyDown("4"))
        {
            StartCoroutine(Buzz(leftIndex));
        }

        if (Input.GetKeyDown("5"))
        {
            StartCoroutine(Buzz(leftThumb));
        }



    }
    public IEnumerator Buzz(int finger)
    {
        //Debug.Log("made it here");
        UduinoManager.Instance.digitalWrite(finger, Uduino.State.HIGH);
        yield return new WaitForSeconds(blinkSpeed);
        UduinoManager.Instance.digitalWrite(finger, Uduino.State.LOW);
        yield return new WaitForSeconds(blinkSpeed * 2);
    }
}