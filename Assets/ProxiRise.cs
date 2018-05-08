using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxiRise : MonoBehaviour {

	public float detectDistance = 10f;
	public float minHeight = 0f;
	public float maxHeight = 10f;
	public Transform playerTransform;

	public float currentDistance;
	// Use this for initialization
	void Start () {
		playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
	}
	
	// Update is called once per frame
	void Update () {

		float dist = Vector3.Distance(transform.position, playerTransform.position);
		currentDistance = dist;
		//check if player is within my vicinity
		if (dist <= detectDistance)
		{
			//move to height according to proximity of player
			float percentage = 1 - dist/detectDistance;
			float height = Mathf.Lerp(minHeight, maxHeight, percentage);
			Debug.Log(percentage);
			transform.position = new Vector3(transform.position.x, height, transform.position.z);

		}

	}
}
