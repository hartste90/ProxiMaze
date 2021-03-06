﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProxiRise : MonoBehaviour {

	private float detectDistance = 7f;
	private float startHeight = -3f;
	private float minHeight = -2f;
	private float maxHeight = -1f;
	private Transform playerTransform;

	bool discovered = false;
	bool isActive = true;

	public float currentDistance;
	// Use this for initialization
	void Start () {
		playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        SetHeight(startHeight);
        //TweenToHidden();
        //TweenDance();
    }

	//// Update is called once per frame
	//void Update () {

	//	float dist = Vector3.Distance(transform.position, playerTransform.position);
	//	currentDistance = dist;
	//	//check if player is within my vicinity
	//	if (dist <= detectDistance)
	//	{
	//		//move to height according to proximity of player
	//		float percentage = 1 - dist/detectDistance;
	//		float height = Mathf.Lerp(minHeight, maxHeight, percentage);
	//		// Debug.Log(percentage);
	//		SetHeight(height);

	//	}


	//}

	// Update is called once per frame
	void Update()
	{
		if (isActive)
        {
			float dist = Vector3.Distance(new Vector3(transform.position.x, playerTransform.position.y, transform.position.z), playerTransform.position);
			currentDistance = dist;
			//check if player is within my vicinity
			if (dist <= detectDistance)
			{
				discovered = true;
				TweenToTall();
			}
			else if (discovered)
			{
				TweenToShort();
			}
		}
		


	}

	void SetHeight(float heightSet)
	{
		transform.localPosition = new Vector3(transform.localPosition.x, heightSet, transform.localPosition.z);
	}

	public void TweenToHidden()
	{
		KillMyTweens();
		transform.DOLocalMoveY(startHeight, 1f).SetId("TweenToHidden" + gameObject.GetInstanceID()).OnComplete(
			() => {
				Destroy(gameObject);
				});
	}

	void TweenToTall()
    {
		KillMyTweens();
		transform.DOLocalMoveY(maxHeight, .2f).SetId("TweenToTall" + gameObject.GetInstanceID());
	}

	void TweenToShort()
    {
		KillMyTweens();
		transform.DOLocalMoveY(minHeight, .2f).SetId("TweenToShort" + gameObject.GetInstanceID());

	}

	public void TweenDance()
    {
		isActive = false;
        KillMyTweens();
		SetHeight(minHeight);
        Sequence s = DOTween.Sequence();
		s.AppendInterval(Random.Range(0, 1f));
		s.Append(transform.DOLocalMoveY(maxHeight, 1f));
		s.AppendInterval(Random.Range(0, 1f));
		s.Append(transform.DOLocalMoveY(minHeight, 1f));
		s.SetLoops(-1);//, LoopType.Yoyo);
		s.Play();
	}

	void KillMyTweens()
    {
		DOTween.Kill("TweenToTall" + gameObject.GetInstanceID());
		DOTween.Kill("TweenToShort" + gameObject.GetInstanceID());
		DOTween.Kill("TweenToHidden" + gameObject.GetInstanceID());
	}
}
