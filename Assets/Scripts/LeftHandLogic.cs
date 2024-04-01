using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandLogic : MonoBehaviour
{
    // reference to the plane to be shown
    public GameObject plane;

    // reference to the threshold for the hand to be considered facing up
    public float xUp = 330;
    public float xPadding = 30;

    public float zUp = 4;
    public float zPadding = 30;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // LiveDebugConsole.Instance.Log("transform.rotation.eulerAngles: " + transform.rotation.eulerAngles.ToString());
        // LiveDebugConsole.Instance.Log("bool logic: " + (isNearDegree(transform.rotation.eulerAngles.x, xUp, xPadding) && isNearDegree(transform.rotation.eulerAngles.z, zUp, zPadding)).ToString());
        // if the hand is rough facing up display the plane
        if (isNearDegree(transform.rotation.eulerAngles.x, xUp, xPadding) && isNearDegree(transform.rotation.eulerAngles.z, zUp, zPadding))
        {
            plane.SetActive(true);
        }
        else
        {
            plane.SetActive(false);
        }
    }

    private bool isNearDegree(float degree, float target, float padding)
    {
        // this but mod by 360 to get the degree
        // whats -26 mod 360
        // 334
        // LiveDebugConsole.Instance.Log("degree: " + degree.ToString());
        // LiveDebugConsole.Instance.Log("target: " + target.ToString());
        // LiveDebugConsole.Instance.Log("padding: " + padding.ToString());
        // LiveDebugConsole.Instance.Log("((target - padding) % 360): " + ((target - padding) % 360).ToString());
        // LiveDebugConsole.Instance.Log("((target + padding) % 360): " + ((target + padding) % 360).ToString());

        return degree >= ((target - padding) % 360) && degree <= ((target + padding) % 360);
    }
}



