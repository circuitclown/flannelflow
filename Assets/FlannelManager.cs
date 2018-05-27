using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlannelManager : MonoBehaviour {
	public Transform flannelPrefab;
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
		Instantiate(flannelPrefab, new Vector3(0, 0, -1), Quaternion.identity);
	}
}
