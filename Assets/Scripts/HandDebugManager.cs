using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandDebugManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI HandDebugConsole;
	[SerializeField] private OVRHand leftHand;
	[SerializeField] private OVRHand rightHand;

	[SerializeField] private OVRSkeleton leftSkel;
	[SerializeField] private OVRSkeleton rightSkel;


	// Start is called before the first frame update
	void Start()
    {

	}

	// Update is called once per frame
	void Update()
    {
#if UNITY_EDITOR

		//foreach (var bone in leftSkel.Bones)
		//{
		//	string lefthandstring = $"{leftSkel.GetSkeletonType()}: num of bones -> {leftSkel.GetCurrentNumBones()} \n" +
		//		$"{leftSkel.GetSkeletonType()}: num of skinnable bones -> {leftSkel.GetCurrentNumSkinnableBones()} \n" +
		//		$"{leftSkel.GetSkeletonType()}: start bone id -> {leftSkel.GetCurrentStartBoneId()} \n" +
		//		$"{leftSkel.GetSkeletonType()}: end bones id -> {leftSkel.GetCurrentEndBoneId()}\n";
		//	string righthandstring = $"{rightSkel.GetSkeletonType()}: num of bones -> {rightSkel.GetCurrentNumBones()} \n" +
		//		$"{rightSkel.GetSkeletonType()}: num of skinnable bones -> {rightSkel.GetCurrentNumSkinnableBones()} \n" +
		//		$"{rightSkel.GetSkeletonType()}: start bone id -> {rightSkel.GetCurrentStartBoneId()} \n" +
		//		$"{rightSkel.GetSkeletonType()}: end bones id -> {rightSkel.GetCurrentEndBoneId()}\n";
			
		//	//HandDebugConsole.SetText(lefthandstring + righthandstring);
		//	//Body_LeftHandThumbTip
		//	//Debug.Log(lefthandstring + righthandstring);
		//}

#endif
	}
}

//https://www.youtube.com/watch?v=DR60_TCkmAY