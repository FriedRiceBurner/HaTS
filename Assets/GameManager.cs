using System.Collections;
using TMPro;
using UnityEngine;
using static OVRHand;

public class GameManager : MonoBehaviour
{
	private float averageConfifdence = 0;
	//hand object
	[SerializeField] private GameObject leftHandGameObject;
	[SerializeField] private GameObject rightHandGameObject;

	[SerializeField] private TextMeshProUGUI HandDebugConsole;
	private OVRHand leftHand;
	private OVRHand rightHand;

	private OVRSkeleton leftSkel;
	private OVRSkeleton rightSkel;

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
		leftHand = leftHandGameObject.GetComponentInChildren<OVRHand>();
		rightHand = rightHandGameObject.GetComponentInChildren<OVRHand>();
		leftSkel = leftHandGameObject.GetComponentInChildren<OVRSkeleton>();
		rightSkel = rightHandGameObject.GetComponentInChildren<OVRSkeleton>();

	}

	// Update is called once per frame
	void Update()
	{
		//Debug.Log(leftMiddleFingerConfidence);
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
		if (isRightMiddleFingerPinching && !isLeftMiddleFingerPinching && functionInProgress)
		{
			leftKeyLock = true;
			Debug.Log("right middle");
		}

		if (!isRightMiddleFingerPinching && isLeftMiddleFingerPinching && functionInProgress)
		{
			rightKeyLock = true;
			Debug.Log("left middle");

		}

		//HandDebugConsole.SetText($"left pinch: {isLeftIndexFingerPinching}, and right pinch: {isRightIndexFingerPinching}\n" +
		//	$"left confidence: {leftIndexFingerConfidence}, and right confidence: {rightIndexFingerConfidence}");

		if (isLeftPinkyFingerPinching && isRightPinkyFingerPinching && !functionInProgress)
		{
			functionInProgress = true;

			Debug.Log("pinched pinky");

			if (virtualKeyboard.activeSelf)
			{
				virtualKeyboard.SetActive(false);
				functionInProgress = false;

				return;
			}
			if (physicalKeyboard.activeSelf)
			{
				physicalKeyboard.SetActive(false);
				functionInProgress = false;

				return;
			}
			functionInProgress = false;

		}

		if (isLeftMiddleFingerPinching && isRightMiddleFingerPinching && !functionInProgress)
		{
			if (!virtualKeyboard.activeSelf)
			{
				// HandDebugConsole.SetText($"left wrist: {leftSkel.Bones[0].Id}, rotation: {leftSkel.Bones[0].Transform.rotation}");
				PlaceKeyboard(0);
			}
		}

		if (isLeftRingFingerPinching && isRightRingFingerPinching && !functionInProgress)
		{
			functionInProgress = true;

			StartCoroutine(TrackLeftKeys());


		}

	}

	IEnumerator TrackLeftKeys()
	{
		HandDebugConsole.SetText("Place left hand on ASDF and pinch right middle finger and thumb");
		while (!leftKeyLock)
		{
			yield return null;
		}
		Debug.Log("done checking left");
		leftFKey = leftHandGameObject.transform.FindChildRecursive(OVRSkeleton.BoneId.Body_LeftHandIndexTip.ToString().Insert(4 + 9, "_").Substring(9)).Find("Bone(Clone)").transform.position;
		leftDKey = leftHandGameObject.transform.FindChildRecursive(OVRSkeleton.BoneId.Body_LeftHandMiddleTip.ToString().Insert(4 + 13, "_").Substring(13)).Find("Bone(Clone)").transform.position;
		Debug.Log(leftFKey);
		Debug.Log(leftDKey);

		leftKeyLock = false;
		StartCoroutine(TrackRightKeys());

	}

	IEnumerator TrackRightKeys()
	{
		HandDebugConsole.SetText("Place right hand on JKL; and pinch left middle finger and thumb");

		while (!rightKeyLock)
		{
			yield return null;
		}
		Debug.Log("done checking right");
		Debug.Log(OVRSkeleton.BoneId.Body_RightHandIndexTip.ToString().Insert(18, "_").Substring(14));
		Debug.Log(OVRSkeleton.BoneId.Body_RightHandMiddleTip.ToString().Insert(14, "_").Substring(10));

		rightJKey = rightHandGameObject.transform.FindChildRecursive(OVRSkeleton.BoneId.Body_RightHandIndexTip.ToString().Insert(18, "_").Substring(14)).Find("Bone(Clone)").position;
		rightKKey = rightHandGameObject.transform.FindChildRecursive(OVRSkeleton.BoneId.Body_RightHandMiddleTip.ToString().Insert(14, "_").Substring(10)).Find("Bone(Clone)").position;
		Debug.Log(rightJKey);
		Debug.Log(rightKKey);

		rightKeyLock = false;
		PlaceKeyboard(1);
		HandDebugConsole.SetText("1. Turn your palms towards you.\r\n\r\n2. Rest hands where you want your virtual keyboard, " +
			"\r\n     then pinch your middle finger and thumb.\r\n\r\n3. Pinch your ring finger and thumb together for physical " +
			"\r\n     keyboard.");

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

			// virtualKeyboard.transform.position = (leftHand.transform.position + rightHand.transform.position) / 2
			// 										+ new Vector3(leftHand.transform.position.x, -0.03f, leftHand.transform.position.z / 2);
			virtualKeyboard.transform.position = (leftHand.transform.position + rightHand.transform.position) / 2;

			// virtualKeyboard.transform.position = (leftHand.transform.position + rightHand.transform.position) / 2;

			// Calculate the direction vector from target1 to target2
			Vector3 direction = rightHand.transform.position - leftHand.transform.position;

			// Calculate the rotation angle based on the direction vector
			float HorizontalAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			float TiltedAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
			//float VerticalAngle = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;

			// Set the rotation of the object based on the calculated angle
			virtualKeyboard.transform.rotation = Quaternion.Euler(0, -TiltedAngle, HorizontalAngle);
			//virtualKeyboard.transform.SetPositionAndRotation();

			Debug.Log(leftHand.transform.position);
			Debug.Log(rightHand.transform.position);
			Debug.Log(virtualKeyboard.transform.position);
		}

		if (KeyboardType == 1)
		{
			if (!physicalKeyboard.activeSelf)
			{
				physicalKeyboard.SetActive(true);

				float leftPointsDistance = Mathf.Abs(Mathf.Sqrt(Mathf.Pow(leftFKey.x - leftDKey.x, 2) + Mathf.Pow(leftFKey.y - leftDKey.y, 2) + Mathf.Pow(leftFKey.z - leftDKey.z, 2)));
				float rightPointsDistance = Mathf.Abs(Mathf.Sqrt(Mathf.Pow(rightJKey.x - rightKKey.x, 2) + Mathf.Pow(rightJKey.y - rightKKey.y, 2) + Mathf.Pow(rightJKey.z - rightKKey.z, 2)));

				float averageDistance = (leftPointsDistance + rightPointsDistance) / 2;

				Debug.Log("averageDistance");
				Debug.Log(averageDistance);

				// put this back in later
				physicalKeyboard.GetComponent<CreateKeys>().PlaceKeys(KeyboardType, new Vector3(averageDistance, 0, 0), new Vector3(0, 0, -averageDistance), new Vector3((averageDistance / 2.4f), 0, 0));
			}

			// physicalKeyboard.transform.position = (leftHand.transform.position + rightHand.transform.position) / 2
			// 											+ new Vector3(leftHand.transform.position.x, -0.03f, leftHand.transform.position.z / 2);

			physicalKeyboard.transform.position = (leftFKey + rightJKey) / 2;

			// Calculate the direction vector from target1 to target2
			Vector3 direction = rightJKey - leftFKey;
			// Calculate the rotation angle based on the direction vector
			float HorizontalAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			float TiltedAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
			// Set the rotation of the object based on the calculated angle
			physicalKeyboard.transform.rotation = Quaternion.Euler(0, -TiltedAngle, HorizontalAngle);
			//virtualKeyboard.transform.SetPositionAndRotation();


		}
		functionInProgress = false;
	}
}
