using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoneDebug : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI boneIdText;
    private OVRBone debugBone;
    
    // function to add bone to hand
    public void AddBone(OVRBone bone) => this.debugBone = bone;


    // Update is called once per frame
    void Update()
    {
        if (debugBone == null) return;

		boneIdText.text = $"{debugBone.Id}";
		boneIdText.transform.rotation = Quaternion.LookRotation(
            boneIdText.transform.position - Camera.main.transform.position);


		transform.position = debugBone.Transform.position;
		transform.rotation = debugBone.Transform.rotation;

	}
}