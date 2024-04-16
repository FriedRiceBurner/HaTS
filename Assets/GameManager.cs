using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static OVRHand;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
	//hand object
	[SerializeField] private GameObject leftHandGameObject;
	[SerializeField] private GameObject rightHandGameObject;

	[SerializeField] private TextMeshProUGUI HandDebugConsole;
	[SerializeField] private OVRHand leftHand;
	[SerializeField] private OVRHand rightHand;

	[SerializeField] private OVRSkeleton leftSkel;
	[SerializeField] private OVRSkeleton rightSkel;

	[SerializeField] private GameObject virtualKeyboard;
	[SerializeField] private GameObject physicalKeyboard;

	//middle finger
	private bool isLeftMiddleFingerPinching;
	private TrackingConfidence leftMiddleFingerConfidence;
	private float leftMiddleFingerPinchStrength;

	private bool isRightMiddleFingerPinching;
	private TrackingConfidence rightMiddleFingerConfidence;
	private float rightMiddleFingerPinchStrength;

	//ring finger
	private bool isLeftRingFingerPinching;
	private TrackingConfidence leftRingFingerConfidence;
	private float leftRingFingerPinchStrength;

	private bool isRightRingFingerPinching;
	private TrackingConfidence rightRingFingerConfidence;
	private float rightRingFingerPinchStrength;

	//pinky
	private bool isLeftPinkyFingerPinching;
	private TrackingConfidence leftPinkyFingerConfidence;
	private float leftPinkyFingerPinchStrength;

	private bool isRightPinkyFingerPinching;
	private TrackingConfidence rightPinkyFingerConfidence;
	private float rightPinkyFingerPinchStrength;

	//[SerializeField] private string instruction = "Place hand";
	private bool functionInProgress = false;

	private bool leftKeyLock = false;
	private bool rightKeyLock = false;

	private Vector3 leftFKey;
	private Vector3 leftDKey;
	private Vector3 rightJKey;
	private Vector3 rightKKey;


	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//middle finger tracking
		isLeftMiddleFingerPinching = leftHand.GetFingerIsPinching(HandFinger.Middle);
		leftMiddleFingerConfidence = leftHand.GetFingerConfidence(HandFinger.Middle);
		leftMiddleFingerPinchStrength = leftHand.GetFingerPinchStrength(HandFinger.Middle);

		isRightMiddleFingerPinching = rightHand.GetFingerIsPinching(HandFinger.Middle);
		rightMiddleFingerConfidence = rightHand.GetFingerConfidence(HandFinger.Middle);
		rightMiddleFingerPinchStrength = rightHand.GetFingerPinchStrength(HandFinger.Middle);

		//ring finger tracking
		isLeftRingFingerPinching = leftHand.GetFingerIsPinching(HandFinger.Ring);
		leftRingFingerConfidence = leftHand.GetFingerConfidence(HandFinger.Ring);
		leftRingFingerPinchStrength = leftHand.GetFingerPinchStrength(HandFinger.Ring);

		isRightRingFingerPinching = rightHand.GetFingerIsPinching(HandFinger.Ring);
		rightRingFingerConfidence = rightHand.GetFingerConfidence(HandFinger.Ring);
		rightRingFingerPinchStrength = rightHand.GetFingerPinchStrength(HandFinger.Ring);

		//pinky finger tracking
		isLeftPinkyFingerPinching = leftHand.GetFingerIsPinching(HandFinger.Pinky);

		isRightPinkyFingerPinching = rightHand.GetFingerIsPinching(HandFinger.Pinky);

		//used for physical key confirmation
		if (isRightRingFingerPinching && !isLeftRingFingerPinching)
		{
			leftKeyLock = true;
			leftKeyLock = false;
		}

		if (!isRightRingFingerPinching && isLeftRingFingerPinching)
		{
			rightKeyLock = true;
			rightKeyLock = false;
		}

		//HandDebugConsole.SetText($"left pinch: {isLeftIndexFingerPinching}, and right pinch: {isRightIndexFingerPinching}\n" +
		//	$"left confidence: {leftIndexFingerConfidence}, and right confidence: {rightIndexFingerConfidence}");

		if (isLeftPinkyFingerPinching && isRightPinkyFingerPinching)
		{
			Debug.Log("pinched pinky");

			if (virtualKeyboard.activeSelf)
			{
				virtualKeyboard.SetActive(false);
				return;
			}
			if (physicalKeyboard.activeSelf)
			{
				physicalKeyboard.SetActive(false);
				return;
			}
		}

		if (isLeftMiddleFingerPinching && isRightMiddleFingerPinching)
		{
			HandDebugConsole.SetText("pinched");
			Debug.Log("pinched middle");
			if (!functionInProgress)
			{
				Debug.Log("success middle pinch");
				functionInProgress = true;
				// HandDebugConsole.SetText($"left wrist: {leftSkel.Bones[0].Id}, rotation: {leftSkel.Bones[0].Transform.rotation}");
				PlaceKeyboard(0);
				functionInProgress = false;
			}
		}

		if (isLeftRingFingerPinching && isRightRingFingerPinching)
		{
			Debug.Log("pinched ring");

			if (!functionInProgress)
			{
				functionInProgress = true;
				HandDebugConsole.SetText("Place left hand on ASDF and pinch right ring finger and thumb");
				StartCoroutine(TrackLeftKeys());
				HandDebugConsole.SetText("Place right hand on JKL; and pinch left ring finger and thumb");
				StartCoroutine(TrackRightKeys());
				PlaceKeyboard(1);
				HandDebugConsole.SetText("1. Turn your palms towards you.\r\n\r\n2. Rest hands where you want your virtual keyboard, " +
					"\r\n     then pinch your middle finger and thumb.\r\n\r\n3. Pinch your ring finger and thumb together for physical " +
					"\r\n     keyboard.");
				functionInProgress = false;

			}
		}


	}

	IEnumerator TrackLeftKeys()
	{

		while (!leftKeyLock)
		{
			yield return null;
		}

		leftFKey = leftHandGameObject.transform.Find(OVRSkeleton.BoneId.Body_LeftHandIndexTip.ToString()).position;
		leftDKey = leftHandGameObject.transform.Find(OVRSkeleton.BoneId.Body_LeftHandMiddleTip.ToString()).position;


		leftKeyLock = false;

	}

	IEnumerator TrackRightKeys()
	{

		while (!rightKeyLock)
		{
			yield return null;
		}

		rightJKey = rightHandGameObject.transform.Find(OVRSkeleton.BoneId.Body_RightHandIndexTip.ToString()).position;
		rightKKey = rightHandGameObject.transform.Find(OVRSkeleton.BoneId.Body_RightHandIndexTip.ToString()).position;

		rightKeyLock = false;

	}

	private void PlaceKeyboard(int KeyboardType)
	{
		if (KeyboardType == 0)
		{
			if (!virtualKeyboard.activeSelf)
			{
				virtualKeyboard.SetActive(true);
				virtualKeyboard.GetComponent<CreateKeys>().PlaceKeys(KeyboardType, new Vector3(0.01905f, 0, 0), new Vector3(0, 0, -0.01905f), new Vector3(0.0079375f, 0, 0));

			}

			//float placementMagnitude = virtualKeyboard.GetComponent<CreateKeys>().topRow.Count() * 0.01905f;

			//virtualKeyboard.transform.position = (leftHand.transform.position + rightHand.transform.position) / 2;
			virtualKeyboard.transform.position = leftHand.transform.position;

			// Calculate the direction vector from target1 to target2
			Vector3 direction = rightHand.transform.position - leftHand.transform.position;

			// Calculate the rotation angle based on the direction vector
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

			// Set the rotation of the object based on the calculated angle
			virtualKeyboard.transform.rotation = Quaternion.Euler(0f, 0f, angle);

			Debug.Log(leftHand.transform.position);
			Debug.Log(rightHand.transform.position);
			Debug.Log((virtualKeyboard.transform.position));


		}
		if (KeyboardType == 1)
		{
			if (!physicalKeyboard.activeSelf)
			{
				physicalKeyboard.SetActive(true);
				float rightjroot = Mathf.Sqrt(Mathf.Pow(rightJKey.x, 2) + Mathf.Pow(rightJKey.y, 2) + Mathf.Pow(rightJKey.z, 2));
				float rightkroot = Mathf.Sqrt(Mathf.Pow(rightKKey.x, 2) + Mathf.Pow(rightKKey.y, 2) + Mathf.Pow(rightKKey.z, 2));

				float leftfroot = Mathf.Sqrt(Mathf.Pow(leftFKey.x, 2) + Mathf.Pow(leftFKey.y, 2) + Mathf.Pow(leftFKey.z, 2));
				float leftdroot = Mathf.Sqrt(Mathf.Pow(leftDKey.x, 2) + Mathf.Pow(leftDKey.y, 2) + Mathf.Pow(leftDKey.z, 2));

				float rightDistance = Mathf.Abs(rightjroot - rightkroot);
				float leftDistance = Mathf.Abs(leftfroot - leftdroot);

				float averageDistance = (rightDistance + leftDistance) / 2;

				Debug.Log("averageDistance");
				Debug.Log(averageDistance);

				// put this back in later
				physicalKeyboard.GetComponent<CreateKeys>().PlaceKeys(KeyboardType, new Vector3(averageDistance, 0, 0), new Vector3(0, 0, -averageDistance), new Vector3((averageDistance / 2.4f), 0, 0));

			}

			physicalKeyboard.transform.position = (leftHand.transform.position + rightHand.transform.position) / 2;

		}

	}
}
