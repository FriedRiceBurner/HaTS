using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static OVRHand;

public class GameManager : MonoBehaviour
{

	[SerializeField] private TextMeshProUGUI HandDebugConsole;
	[SerializeField] private OVRHand leftHand;
	[SerializeField] private OVRHand rightHand;

	[SerializeField] private OVRSkeleton leftSkel;
	[SerializeField] private OVRSkeleton rightSkel;

	[SerializeField] private GameObject virtualKeyboard;
	[SerializeField] private GameObject physicalKeyboard;

	private bool isLeftMiddleFingerPinching;
	private TrackingConfidence leftMiddleFingerConfidence;
	private float leftRingFingerPinchStrength;

	private bool isRightMiddleFingerPinching;
	private TrackingConfidence rightMiddleFingerConfidence;
	private float rightMiddleFingerPinchStrength;

	private bool operating;

	[SerializeField] private string instruction = "Place hand";
	private bool functionInProgress;

	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		isLeftMiddleFingerPinching = leftHand.GetFingerIsPinching(HandFinger.Middle);
		leftMiddleFingerConfidence = leftHand.GetFingerConfidence(HandFinger.Middle);
		leftRingFingerPinchStrength = leftHand.GetFingerPinchStrength(HandFinger.Middle);

		isRightMiddleFingerPinching = rightHand.GetFingerIsPinching(HandFinger.Middle);
		rightMiddleFingerConfidence = rightHand.GetFingerConfidence(HandFinger.Middle);
		rightMiddleFingerPinchStrength = rightHand.GetFingerPinchStrength(HandFinger.Middle);

		//HandDebugConsole.SetText($"left pinch: {isLeftIndexFingerPinching}, and right pinch: {isRightIndexFingerPinching}\n" +
		//	$"left confidence: {leftIndexFingerConfidence}, and right confidence: {rightIndexFingerConfidence}");

		if (isLeftMiddleFingerPinching && isRightMiddleFingerPinching)
		{
			functionInProgress = true;

			if (virtualKeyboard.activeSelf)
			{
				virtualKeyboard.SetActive(false);
				return;
			}
			if (functionInProgress)
			{
				// HandDebugConsole.SetText($"left wrist: {leftSkel.Bones[0].Id}, rotation: {leftSkel.Bones[0].Transform.rotation}");
				virtualKeyboard.SetActive(true);
				virtualKeyboard.transform.position = (leftHand.transform.position + rightHand.transform.position) / 2 + leftHand.transform.position;
				functionInProgress = false;
			}

		}


	}

	private void Placement()
	{
		virtualKeyboard.GetComponent<CreateKeys>().PlaceKeys();

	}
}
