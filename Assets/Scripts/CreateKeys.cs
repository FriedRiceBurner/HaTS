using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateKeys : MonoBehaviour
{
<<<<<<< HEAD
	[SerializeField] private GameObject keyPrefab;
=======
    [SerializeField] private GameObject keyPrefab;
>>>>>>> b9095dea90f1d9e52c439786d4c543100491a039

	private Vector3 previousPosition = new Vector3(0, 0, 0);
	//private Vector3 startingHome = new Vector3(0, 0, 0);
	//private Vector3 startingBottom = new Vector3(0, 0, 0);

	private List<string> topRow = new List<string>()
	{ "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "[{", "]}"};

	private List<string> homeRow = new List<string>()
	{ "A", "S", "D", "F", "G", "H", "J", "K", "L", ";:", "\'\""};

	private List<string> bottomRow = new List<string>()
	{ "Z", "X", "C", "V", "B", "N", "M", ",<", ".>", "/?"};


	//distance from center of each key to next
<<<<<<< HEAD
	Vector3 keyDistanceX = new Vector3(0.015f, 0, 0);
	Vector3 keyDistanceY = new Vector3(0, 0, -0.015f);
	Vector3 shiftedRow = new Vector3(0.004f, 0, 0);

=======
	Vector3 keyDistanceX = new Vector3(0.01905f, 0, 0);
	Vector3 keyDistanceY = new Vector3(0, 0, -0.01905f);
	Vector3 shiftedRow = new Vector3(0.0079375f, 0, 0);

	//key dimensions based on meter scaling
	public Vector3 KeyScale = new Vector3(0.0174625f, 0.0174625f, 0.0174625f);
>>>>>>> b9095dea90f1d9e52c439786d4c543100491a039


	// Start is called before the first frame update
	void Start()
<<<<<<< HEAD
	{
		//previousPosition = new Vector3(0, 0, 0);

=======
    {
		//previousPosition = new Vector3(0, 0, 0);
		
>>>>>>> b9095dea90f1d9e52c439786d4c543100491a039
		foreach (string key in topRow)
		{
			GameObject newKey = Instantiate(keyPrefab);
			newKey.transform.parent = transform;
			newKey.transform.localPosition = previousPosition + keyDistanceX;
			Debug.Log(newKey.transform.position);
<<<<<<< HEAD
			newKey.transform.localScale = new Vector3(1, 1, 1);
=======
			newKey.transform.localScale = KeyScale;
>>>>>>> b9095dea90f1d9e52c439786d4c543100491a039
			previousPosition = newKey.transform.localPosition;
			newKey.GetComponent<KeyScript>().keyID = key;
			newKey.name = key + "-key";
		}

		previousPosition = Vector3.zero + shiftedRow + keyDistanceY;

		foreach (string key in homeRow)
<<<<<<< HEAD
		{
			GameObject newKey = Instantiate(keyPrefab);
			newKey.transform.parent = transform;
			newKey.transform.localPosition = previousPosition + keyDistanceX;
			Debug.Log(newKey.transform.position);
			newKey.transform.localScale = new Vector3(1, 1, 1);
			previousPosition = newKey.transform.localPosition;
			newKey.GetComponent<KeyScript>().keyID = key;
			newKey.name = key + "-key";
		}

		previousPosition = Vector3.zero + shiftedRow * 3 + keyDistanceY * 2;
=======
        {
            GameObject newKey = Instantiate(keyPrefab);
            newKey.transform.parent = transform;
            newKey.transform.localPosition = previousPosition + keyDistanceX;
            Debug.Log(newKey.transform.position);
			newKey.transform.localScale = KeyScale;
			previousPosition = newKey.transform.localPosition;
            newKey.GetComponent<KeyScript>().keyID = key;
            newKey.name = key + "-key";
		}

		previousPosition = Vector3.zero + shiftedRow * 2.5f + keyDistanceY * 2;
>>>>>>> b9095dea90f1d9e52c439786d4c543100491a039

		foreach (string key in bottomRow)
		{
			GameObject newKey = Instantiate(keyPrefab);
			newKey.transform.parent = transform;
			newKey.transform.localPosition = previousPosition + keyDistanceX;
			Debug.Log(newKey.transform.position);
<<<<<<< HEAD
			newKey.transform.localScale = new Vector3(1, 1, 1);
=======
			newKey.transform.localScale = KeyScale;
>>>>>>> b9095dea90f1d9e52c439786d4c543100491a039
			previousPosition = newKey.transform.localPosition;
			newKey.GetComponent<KeyScript>().keyID = key;
			newKey.name = key + "-key";
		}

<<<<<<< HEAD


	}

	// Update is called once per frame
	void Update()
	{

	}
}
=======
		

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
>>>>>>> b9095dea90f1d9e52c439786d4c543100491a039
