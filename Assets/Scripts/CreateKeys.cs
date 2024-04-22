using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateKeys : MonoBehaviour
{
	[SerializeField] private GameObject keyPrefab;
	[SerializeField] private GameObject SpacebarPrefab;

	public int KeyboardType;
	private bool keysCreated = false;
	private Vector3 previousPosition = new Vector3(0, 0, 0);
	//private Vector3 startingHome = new Vector3(0, 0, 0);
	//private Vector3 startingBottom = new Vector3(0, 0, 0);

	[HideInInspector]
	public List<string> topRow = new List<string>()
	{ "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "[ {", "] }"};

	private List<string> homeRow = new List<string>()
	{ "A", "S", "D", "F", "G", "H", "J", "K", "L", "; :", "\' \""};

	private List<string> bottomRow = new List<string>()
	{ "Z", "X", "C", "V", "B", "N", "M", ", <", ". >", "/ ?"};

	private Vector3 centerMarker;

	//distance from center of each key to next
	// Vector3 keyDistanceX = new Vector3(0.01905f, 0, 0);
	// Vector3 keyDistanceY = new Vector3(0, 0, -0.01905f);
	// Vector3 shiftedRow = new Vector3(0.0079375f, 0, 0);

	// virtual vs physical keyboard overlay
	private Vector3[] KeyScales = {
		new Vector3(1, 1, 1),
		new Vector3(1, 1, 1),
	};

	// Start is called before the first frame update
	public void PlaceKeys(int KeyboardType, Vector3 keyDistanceX, Vector3 keyDistanceY, Vector3 shiftedRow)
	{
		if (keysCreated) return;
		previousPosition = -keyDistanceX * 5 - (keyDistanceX / 3) - shiftedRow - keyDistanceY;
		//previousPosition = new Vector3(0, 0, 0);

		foreach (string key in topRow)
		{
			GameObject newKey = Instantiate(keyPrefab);
			newKey.transform.parent = transform;
			newKey.transform.localPosition = previousPosition + keyDistanceX;
			newKey.transform.localScale = KeyScales[KeyboardType];
			previousPosition = newKey.transform.localPosition;
			newKey.GetComponent<KeyScript>().keyID = key;
			newKey.name = key + "-key";
		}

		previousPosition = -keyDistanceX * 5 - (keyDistanceX / 3) - shiftedRow - keyDistanceY + shiftedRow + keyDistanceY;

		foreach (string key in homeRow)
		{
			GameObject newKey = Instantiate(keyPrefab);
			newKey.transform.parent = transform;
			newKey.transform.localPosition = previousPosition + keyDistanceX;
			//Debug.Log(newKey.transform.position);
			newKey.transform.localScale = KeyScales[KeyboardType];
			previousPosition = newKey.transform.localPosition;
			newKey.GetComponent<KeyScript>().keyID = key;
			newKey.name = key + "-key";
		}

		previousPosition = -keyDistanceX * 5 - (keyDistanceX / 3) - shiftedRow - keyDistanceY + shiftedRow * 2.5f + keyDistanceY * 2;

		foreach (string key in bottomRow)
		{
			GameObject newKey = Instantiate(keyPrefab);
			newKey.transform.parent = transform;
			newKey.transform.localPosition = previousPosition + keyDistanceX;
			//Debug.Log(newKey.transform.position);
			newKey.transform.localScale = KeyScales[KeyboardType];
			previousPosition = newKey.transform.localPosition;
			newKey.GetComponent<KeyScript>().keyID = key;
			newKey.name = key + "-key";
			if (key.Equals("B") || key.Equals("b")) centerMarker = newKey.transform.localPosition;

		}

		GameObject spacebarkey = Instantiate(SpacebarPrefab);
		spacebarkey.transform.parent = transform;
		spacebarkey.transform.localPosition = centerMarker + keyDistanceY;
		spacebarkey.transform.localScale = KeyScales[KeyboardType];
		spacebarkey.GetComponent<KeyScript>().keyID = " ";
		spacebarkey.name = "Spacebar";

		keysCreated = true;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
