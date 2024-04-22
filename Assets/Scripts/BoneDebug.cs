using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
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
        Debug.Log($"{debugBone.Id}");
        boneIdText.text = $"{debugBone.Id}";
        boneIdText.transform.rotation = Quaternion.LookRotation(
            boneIdText.transform.position - Camera.main.transform.position);

        // if (debugBone.Id.ToString().Contains("Tip"))
        // {
        //     debugBone.Transform.GetComponent<PokeInteractor>().Enable();
        // }


        transform.position = debugBone.Transform.position;
        transform.rotation = debugBone.Transform.rotation;

    }
}
