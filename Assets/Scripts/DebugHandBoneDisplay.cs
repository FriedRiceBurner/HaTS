using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHandBoneDisplay : MonoBehaviour
{

    [SerializeField] private OVRHand hand;

    [SerializeField] private OVRSkeleton handSkel;

    [SerializeField] private GameObject bonePrefab;
    private bool boneAdded;

    // Start is called before the first frame update
    void Start()
    {
        if(!hand) hand = GetComponent<OVRHand>();
        if(!handSkel) handSkel = GetComponent<OVRSkeleton>();

    }
    private void CreateBones()
    {

        foreach (var bone in handSkel.Bones)
        {
            Instantiate(bonePrefab, bone.Transform).GetComponent<BoneDebug>().AddBone(bone);
        }
		boneAdded = true;
    }


    // Update is called once per frame
    void Update()
    {

        if (hand.IsTracked)
        {
            if (!this.boneAdded) CreateBones();
        }

    }
}

//https://developer.oculus.com/reference/unity/v63/class_o_v_r_skeleton/#a28012298dc7f3ecc0de0988fa341461b
// https://www.youtube.com/watch?v=DR60_TCkmAY