using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlannelManager : MonoBehaviour {
	public GameObject flannelPrefab;
	public Lives lives;
	public float timeBetweenFlannels;

	private float timeSinceLastFlannel = 0;

	void Start() {
		InstantiateFlannel();
	}
	
	void FixedUpdate() {
		timeSinceLastFlannel += Time.deltaTime;

		if (timeBetweenFlannels <= timeSinceLastFlannel) {
			InstantiateFlannel();
			timeSinceLastFlannel = 0;
		}
	}

	void InstantiateFlannel() {
		/*
		   Flannels are automatically, randomly placed by `Flannel` when 
		   created.
		 */
		GameObject flannelObject = Instantiate(
			flannelPrefab,
			new Vector3(0, 0, 1),
			Quaternion.identity
		) as GameObject;

		Flannel flannel = flannelObject.GetComponent<Flannel>();
		flannel.lives = lives;
	}
}
